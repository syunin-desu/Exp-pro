using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Const;

struct BattleAction
{
    public string action;
    public int speed;
    // 1:Player 2:敵
    public int char_role;

    public BattleAction(string action, int spd, int char_role)
    {
        this.action = action;
        this.speed = spd;
        this.char_role = char_role;
    }
}

//対戦を管理
public class BattleManager : MonoBehaviour
{
    public PlayerUIManager playerUI;
    public PlayerManager player;
    public EnemyUIManager enemyUI;
    public BattleUIManager battleUI;
    public QuestManager questManager;
    public BattleUIWindow battleWindow;



    //経過ターン
    private int turned;

    EnemyManager enemy;

    //プレイヤー行動リスト
    [SerializeField]
    List<BattleAction> actionList;
    //全体行動リスト
    [SerializeField]
    List<BattleAction> allActionList;

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
            this.setAction_Enemy();

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
            int role = allAction.char_role;
            //アクションによって識別
            switch (allAction.action)
            {
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

                    アクションを追加していく
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
        // TODO メッセージ処理
        // DialogTextManager.instance.SetScenarios(new string[] { player.NAME + "の攻撃" });

        //Playerが攻撃
        player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
        // DialogTextManager.instance.SetScenarios(new string[] { player.STRANGE + "のダメージを与えた" });
        // StartCoroutine("waitClick");

    }

    //防御
    public void Defence()
    {
        //指定したキャラクタの防御フラグをtrueにする

    }

    //敵の攻撃
    void EnemyAttack(EnemyManager enemy)
    {
        //Enemyが攻撃
        // TODO メッセージ処理
        // DialogTextManager.instance.SetScenarios(new string[] { enemy.NAME + "の攻撃" });
        // StartCoroutine("waitClick");
        enemy.Attack(player);
        playerUI.UpdateUI(player);
        // DialogTextManager.instance.SetScenarios(new string[] { enemy.STRANGE + "のダメージを受けた" });
        // StartCoroutine("waitClick");

    }


    //1ターンの終了処理
    void turnFinalize()
    {
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
    public void setAction_Attack()
    {
        Debug.Log("setUp");
        BattleAction act = new BattleAction(Const.CO.ATTACK, player.SPEED, Const.CO.PLAYER);
        this.actionList.Add(act);
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

    private void setAction_Enemy()
    {
        BattleAction act = new BattleAction("Attack", enemy.SPEED, Const.CO.ENEMY);
        allActionList.Add(act);
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
        this.allActionList.Sort((obj, target) => obj.speed.CompareTo(target.speed));

    }


    //クリックを待つ
    private IEnumerator waitClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }
}
