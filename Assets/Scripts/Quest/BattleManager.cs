using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Const;

public class BattleAction
{
    public string action;
    public CharBase character;

    public BattleAction(string action, CharBase character)
    {
        this.action = action;
        this.character = character;
    }
}

//対戦を管理
public class BattleManager : MonoBehaviour
{
    public PlayerUIManager playerUI;
    public PlayerManager player;
    EnemyManager enemy;
    public EnemyUIManager enemyUI;
    public BattleUIManager battleUI;
    public QuestManager questManager;
    public BattleUIWindow battleWindow;



    //経過ターン
    private int turned;


    //プレイヤー行動リスト
    [SerializeField]
    List<BattleAction> actionList;
    //全体行動リスト
    [SerializeField]
    public List<BattleAction> allActionList;

    //Playerパーティーの人数
    // TODO あとでパーティ情報はクラスなどにまとめる
    private int char_Num = 1;


    void Start()
    {
        //リストのセット
        actionList = this.setActionList<BattleAction>();
        allActionList = this.setActionList<BattleAction>();
        // バトルUIを非表示にする
        this.switchBattleUI(false);
    }

    public void SetUp(EnemyManager enemyManager)
    {
        //行動リストの初期化
        this.clear_actionList();
        //バトルUIの表示
        this.switchBattleUI(true);

        enemy = enemyManager;
        enemyUI.SetUpUI(enemy);
        playerUI.SetUpUI(player);

        //経過ターンをリセット
        turned = 0;
    }


    void Update()
    {

        //バトルシーンの場合更新処理を行う
        if (questManager.getSceneSwitcher() == Const.CO.SCENE_BATTLE)
        {

            //アクション選択フェーズ
            if (this.actionSelect() == true)
            {

                //アクション実行順などの処理
                this.actionOrderSorting();


                if (this.doAction() == true)
                {
                    //バトル終了処理
                    this.EndBattle();
                    Destroy(enemy.gameObject);
                }
                else
                {
                    //ターン終了処理
                    this.turnFinalize();
                }
            }
        }
    }

    //アクション選択フェーズ
    bool actionSelect()
    {
        //人数分アクションが選択されたらバトルさせる
        if (actionList.Count == char_Num)
        {
            //プレイヤーの動きを全体行動リストに登録
            allActionList.AddRange(actionList);

            //敵アクションの登録
            this.setAction_Enemy((int)Const.CO.COMMAND.Defence, this.enemy);

            //選択UIを削除し
            this.switchActionSelectUI(false);
            return true;
        }

        return false;
    }


    //リストに登録されているアクションを実行する
    bool doAction()
    {
        bool endBattle = false;


        foreach (BattleAction allAction in allActionList)
        {
            //キャラ識別
            int role = allAction.character.char_role;

            //アクションによって識別
            switch (allAction.action)
            {
                //攻撃
                case Const.CO.ATTACK:
                    //プレイヤーの場合
                    if (role == Const.CO.PLAYER)
                    {
                        this.PlayerAttack(player);
                    }
                    //敵の場合
                    else
                    {
                        this.EnemyAttack(enemy);
                    }
                    break;

                // アビリティ
                case Const.CO.ABILITY:
                    break;

                // 防御
                case Const.CO.DEFENCE:
                    this.Defence(allAction.character);
                    break;
                // アイテム
                case Const.CO.ITEM:
                    break;
                default:
                    break;
            }

            if (this.updateBattleState(enemy) == true)
            {
                endBattle = true;
                break;
            }

        }
        return endBattle;
    }

    //攻撃処理
    public void PlayerAttack(PlayerManager player)
    {
        //Playerが攻撃
        player.Attack(enemy);
        enemyUI.UpdateUI(enemy);

    }
    //敵の攻撃
    void EnemyAttack(EnemyManager enemy)
    {
        enemy.Attack(player);
        playerUI.UpdateUI(player);
    }

    public void PlayerAttack(CharBase character)
    {
        //Playerが攻撃
        player.Attack(enemy);
        enemyUI.UpdateUI(enemy);

    }

    //防御
    public void Defence(CharBase character)
    {
        //指定したキャラクタの防御フラグをtrueにする
        character.Defence();

    }



    //1ターンの終了処理
    void turnFinalize()
    {
        //パラメータの初期化
        this.resetCharBuffANDunbuff(this.allActionList);

        //行動リストの初期化
        this.clear_actionList();

        //アクション選択UIを表示
        this.switchActionSelectUI(true);


        //ターンを追加
        this.turned++;
    }

    //バトル終了処理
    void EndBattle()
    {
        this.switchBattleUI(false);
        questManager.EndBattle();

        questManager.setSceneSwitcher(Const.CO.SCENE_QUEST);
    }


    //================
    // アクション登録
    //================
    //バトルの状態の処理
    private bool updateBattleState(EnemyManager enemy)
    {
        //HP処理
        if (enemy.HP <= 0)
        {
            return true;

        }
        return false;
    }



    //攻撃アクションを登録
    public void setAction_Attack(CharBase character)
    {
        BattleAction act = new BattleAction(Const.CO.ATTACK, character);
        setActionList_FOR_Role(character.char_role, act);
    }

    //防御アクションを登録
    public void setAction_Defence(CharBase character)
    {
        BattleAction act = new BattleAction(Const.CO.DEFENCE, character);
        setActionList_FOR_Role(character.char_role, act);

    }

    //敵、プレイヤーを判別して対応したアクションリストにaddする
    private void setActionList_FOR_Role(int character_role, BattleAction act)
    {
        if (character_role == Const.CO.PLAYER)
        {
            this.actionList.Add(act);
        }
        else if (character_role == Const.CO.ENEMY)
        {
            this.allActionList.Add(act);
        }
    }

    //================
    // Private メソッド
    //================

    private List<BattleAction> setActionList<BattleAction>()
    {
        return new List<BattleAction>();
    }

    //行動リストの初期化
    private void clear_actionList()
    {
        actionList.Clear();
        allActionList.Clear();
    }

    // 敵の行動を登録する
    private void setAction_Enemy(int command, CharBase enemy)
    {
        switch (command)
        {
            case (int)Const.CO.COMMAND.Attack:
                this.setAction_Attack(enemy);
                break;
            case (int)Const.CO.COMMAND.Ability:
                break;
            case (int)Const.CO.COMMAND.Defence:
                this.setAction_Defence(enemy);
                break;
            case (int)Const.CO.COMMAND.Item:
                break;
            default:
                break;
        }
    }

    // バトルUIを表示または非表示
    private void switchBattleUI(bool switcher)
    {
        enemyUI.gameObject.SetActive(switcher);
        battleUI.gameObject.SetActive(switcher);
        playerUI.gameObject.SetActive(switcher);
        battleWindow.gameObject.SetActive(switcher);
    }

    // アクション選択UIの表示,非表示
    private void switchActionSelectUI(bool switcher)
    {
        battleUI.gameObject.SetActive(switcher);
    }


    //素早さでソート
    private void actionOrderSorting()
    {
        List<BattleAction> actList = new List<BattleAction>();

        // //アビリティ優先順に並び変える
        actList.AddRange(this.sortActionListForAbilityPriority(this.allActionList));
        this.allActionList.Clear();
        this.allActionList.AddRange(actList);
        foreach (BattleAction item in this.allActionList)
        {
            Debug.Log(item.character.NAME);

        }

    }

    //行動順の並び替え
    private List<BattleAction> sortActionListForAbilityPriority(List<BattleAction> allAction)
    {
        List<BattleAction> resultList = new List<BattleAction>();
        List<BattleAction> FAlist = new List<BattleAction>();
        List<BattleAction> NAlist = new List<BattleAction>();


        //三種類の優先度に分けリストに入れる
        foreach (BattleAction charAction in allAction)
        {
            //防御、またはFAだった時は先頭に持ってくる(同値はSPD比較)
            // TODO FA条件式は必要になったときに追加
            if (charAction.action == Const.CO.DEFENCE)
            {
                FAlist.Add(charAction);
            }
            else if (charAction.action == Const.CO.ATTACK)
            {
                NAlist.Add(charAction);
            }
        }

        //それぞれのリストをSPDの速い順位ソート
        FAlist.Sort((obj, target) => obj.character.SPEED.CompareTo(target.character.SPEED));
        FAlist.Reverse();

        NAlist.Sort((obj, target) => obj.character.SPEED.CompareTo(target.character.SPEED));
        NAlist.Reverse();

        //リストに追加
        resultList.AddRange(FAlist);
        resultList.AddRange(NAlist);
        return resultList;
    }


    //クリックを待つ
    private IEnumerator waitClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }

    //キャラクタのバフ、デバフをリセットする
    private void resetCharBuffANDunbuff(List<BattleAction> allActionList)
    {
        foreach (BattleAction allAction in allActionList)
        {
            //バフのリセット
            allAction.character.resetBuff();
        }

    }


}
