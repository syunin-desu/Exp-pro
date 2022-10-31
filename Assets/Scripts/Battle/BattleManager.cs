using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using CONST;

// 戦闘に参加しているキャラのアクションクラス
public class BattleAction
{
    public string action;

    public string abilityName = "";

    public string itemName = "";

    public CharBase character;


    public BattleAction(string action, CharBase character)
    {
        this.action = action;
        this.character = character;
    }
    public void setAbilityName(string abilityName)
    {
        this.abilityName = abilityName;
    }

    public void setItemName(string itemName)
    {
        this.itemName = itemName;

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
    public ItemUIManager itemUI;
    public ItemScrollController itemContents;
    public ItemManager itemManager;
    public AbilityUIManager abilityUI;
    public AbilityScrollController abilityContents;
    public AbilityManager abilityManager;



    //経過ターン
    private int turned;

    // 実行中のコルーチン
    private Coroutine _currentCoroutine;


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

    ///バトル
    public async void battle()
    {
        while (questManager.getSceneSwitcher() == CONST.SCENE.SCENE_BATTLE)
        {
            //アクション選択フェーズ
            // 選択されるまで待つ
            await this.actionSelect();

            // アクション実行順などの処理
            this.actionOrderSorting();

            /// バトルが終わるまで待つ
            await this.doAction();

        }
    }

    //アクション選択フェーズ
    private async UniTask actionSelect()
    {
        await UniTask.WaitUntil(() => actionList.Count == char_Num);
        //人数分アクションが選択されたらバトルさせる
        //プレイヤーの動きを全体行動リストに登録
        allActionList.AddRange(actionList);

        //敵アクションの登録
        this.setAction_Enemy((int)CONST.BATTLE_ACTION.COMMAND.Attack, this.enemy);

        //選択UIを削除し
        this.switchActionSelectUI(false);
    }

    //リストに登録されているアクションを実行する
    private async UniTask doAction()
    {
        bool endBattle = false;


        foreach (BattleAction allAction in allActionList)
        {
            //キャラ識別
            int role = allAction.character.char_role;

            Debug.Log(allAction.action);
            //アクションによって識別
            switch (allAction.action)
            {
                //攻撃
                case CONST.BATTLE_ACTION.ATTACK:
                    //プレイヤーの場合
                    if (role == CONST.CHARCTOR.PLAYER)
                    {
                        await this.PlayerAttack(player);

                    }
                    //敵の場合
                    else
                    {
                        await this.EnemyAttack(enemy);
                    }
                    break;

                // アビリティ
                case CONST.BATTLE_ACTION.ABILITY:
                    if (role == CONST.CHARCTOR.PLAYER)
                    {
                        // TODO 使用者と対象のキャラ情報ははAllActionListに格納できるようにしたい
                        // 引数に指定しない
                        await abilityManager.execAbility(player, enemy, allAction.abilityName);
                        enemyUI.UpdateUI(enemy);
                    }
                    else
                    {
                        await abilityManager.execAbility(enemy, player, allAction.abilityName);
                        // TODO 一時的に記述 HPなどのUIへの反映を動的に監視できるようにしたい

                    }
                    break;

                // 防御
                case CONST.BATTLE_ACTION.DEFENSE:
                    await this.Defense(allAction.character);
                    break;
                // アイテム
                case CONST.BATTLE_ACTION.ITEM:
                    if (role == CONST.CHARCTOR.PLAYER)
                    {
                        await itemManager.ExecItem(player, enemy, allAction.itemName);
                    }
                    else
                    {
                        await itemManager.ExecItem(enemy, player, allAction.itemName);
                        // TODO 一時的に記述 HPなどのUIへの反映を動的に監視できるようにしたい
                    }
                    break;
                default:
                    Debug.Log("不正なアクションが登録されました");
                    break;
            }

            if (this.updateBattleState(enemy) == true)
            {
                endBattle = true;
                break;
            }

        }

        if (endBattle)
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

    //攻撃処理
    private async UniTask PlayerAttack(PlayerManager player)
    {
        //Playerが攻撃
        player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
        await UniTask.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));


    }

    //敵の攻撃
    private async UniTask EnemyAttack(EnemyManager enemy)
    {
        enemy.Attack(player);
        await UniTask.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));

    }

    //防御
    private async UniTask Defense(CharBase character)
    {
        //指定したキャラクタの防御フラグをtrueにする
        character.Defense();

        await UniTask.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));

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
        Debug.Log("ターン:" + this.turned);
    }

    //バトル終了処理
    void EndBattle()
    {
        this.switchBattleUI(false);
        questManager.EndBattle();

        questManager.setSceneSwitcher(CONST.SCENE.SCENE_QUEST);
    }

    //バトルの状態の処理
    private bool updateBattleState(EnemyManager enemy)
    {
        //HP処理
        if (enemy.GetHp() <= 0)
        {
            return true;

        }
        return false;
    }

    //================
    // アクション登録
    //================
    //<summary>攻撃アクションを登録</summary>
    public void setAction_Attack(CharBase character)
    {
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.ATTACK, character);
        setActionList_FOR_Role(character.char_role, act);
    }

    /// <summary>防御アクションを登録</summary>
    public void setAction_Defence(CharBase character)
    {
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.DEFENSE, character);
        setActionList_FOR_Role(character.char_role, act);

    }

    /// <summary>アビリティコマンドを登録</summary>
    public void setAction_Ability(CharBase character, string selectedAbilityName)
    {
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.ABILITY, character);
        // アビリティ名を格納
        act.setAbilityName(selectedAbilityName);

        setActionList_FOR_Role(character.char_role, act);

        // アビリティウインドウを閉じる
        this.abilityUI.removeAbilityWindow();

    }

    /// <summary>
    /// アイテムコマンドを登録
    /// </summary>
    /// <param name="character">選択キャラ</param>
    /// <param name="selectedItemName">選択されたアイテム名</param>
    public void setAction_Item(CharBase character, string selectedItemName)
    {
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.ITEM, character);

        // アビリティ名を格納
        act.setItemName(selectedItemName);

        setActionList_FOR_Role(character.char_role, act);

        // アイテムウインドウを閉じる
        this.itemUI.removeItemWindow();

    }

    /// <summary>アビリティウインドウを表示</summary>
    /// アビリティボタンクリック時に呼び出される
    public void showAbility_window(CharBase charactor)
    {
        //アビリティメニューを作成
        this.abilityUI.manageShowAbilityWindow(true);

        //アビリティをコンテンツにセット
        this.abilityContents.Setup_abilityUI(charactor.GetHavingAbilities());
    }

    /// <summary>アイテムウインドウを表示</summary>
    /// アイテムボタンクリック時に呼び出される
    public void showItem_window(CharBase charactor)
    {
        //アビリティメニューを作成
        this.itemUI.manageShowItemWindow(true);

        //アビリティをコンテンツにセット
        this.itemContents.SetupItemUI(charactor.GetHavingItem());
    }

    private void setActionList_FOR_Role(int character_role, BattleAction act)
    {
        if (character_role == CONST.CHARCTOR.PLAYER)
        {
            this.actionList.Add(act);
        }
        else if (character_role == CONST.CHARCTOR.ENEMY)
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
            case (int)CONST.BATTLE_ACTION.COMMAND.Attack:
                this.setAction_Attack(enemy);
                break;
            case (int)CONST.BATTLE_ACTION.COMMAND.Ability:
                break;
            case (int)CONST.BATTLE_ACTION.COMMAND.Defence:
                this.setAction_Defence(enemy);
                break;
            case (int)CONST.BATTLE_ACTION.COMMAND.Item:
                break;
            default:
                break;
        }
    }

    // バトルUIを表示または非表示
    private void switchBattleUI(bool isShowUI)
    {
        enemyUI.gameObject.SetActive(isShowUI);
        this.switchActionSelectUI(isShowUI);
        playerUI.gameObject.SetActive(isShowUI);
        battleWindow.gameObject.SetActive(isShowUI);


        // アビリティ、アイテムリストはfalseの時の処理する
        if (isShowUI != true)
        {
            this.abilityUI.manageShowAbilityWindow(isShowUI);
            this.itemUI.manageShowItemWindow(isShowUI);
        }
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
            // TODO DA条件式は必要になったときに追加
            if (charAction.action == CONST.BATTLE_ACTION.DEFENSE ||
             abilityManager.getAbilityTimingType(charAction.abilityName) == CONST.BATTLE_ACTION.ACTION_FAST)
            {
                FAlist.Add(charAction);
            }
            // TODO アイテムをNormalにするが将来的にはItemDataにTimingTypeを設定させたほうがいいと思う
            else if (charAction.action == CONST.BATTLE_ACTION.ATTACK ||
            charAction.action == CONST.BATTLE_ACTION.ITEM ||
            abilityManager.getAbilityTimingType(charAction.abilityName) == CONST.BATTLE_ACTION.ACTION_NORMAL)
            {
                NAlist.Add(charAction);
            }
        }

        //それぞれのリストをSPDの速い順位ソート
        FAlist.Sort((obj, target) => obj.character.GetSpeed().CompareTo(target.character.GetSpeed()));
        FAlist.Reverse();

        NAlist.Sort((obj, target) => obj.character.GetSpeed().CompareTo(target.character.GetSpeed()));
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
