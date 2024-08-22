using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// バトルシーン管理クラス
/// </summary>
public class BattleManager : MonoBehaviour
{
    /// <summary>
    /// プレイヤー情報表示UI
    /// </summary>
    public PlayerUIManager playerUI;

    /// <summary>
    /// パーティーメンバー
    /// </summary>
    public PartyMember partyMember;

    /// <summary>
    /// 敵キャラ
    /// </summary>
    private EnemyManager enemy;

    /// <summary>
    /// コマンド選択UI(名前を変える必要がある)
    /// </summary>
    public BattleUIManager battleUI;

    /// <summary>
    /// バトル時のプレイヤー側のステータス・コマンドウインドウ表示UI
    /// </summary>
    public BattleUIWindow battleWindow;

    /// <summary>
    /// アイテム選択UI管理クラス
    /// </summary>
    public ItemUIManager itemUI;

    /// <summary>
    /// アイテムウインドウの中身を管理するクラス
    /// </summary>
    public ItemScrollController itemContents;

    /// <summary>
    /// アイテム実行クラス
    /// </summary>
    public ItemManager itemManager;

    /// <summary>
    /// アビリティ選択UI管理クラス
    /// </summary>
    public AbilityUIManager abilityUI;

    /// <summary>
    /// アビリティウインドウの中身を管理するクラス
    /// </summary>
    public AbilityScrollController abilityContents;

    /// <summary>
    /// アビリティ実行クラス
    /// </summary>
    public AbilityManager abilityManager;

    /// <summary>
    /// 行動リスト
    /// </summary>
    public BattleActionList battleActionList;

    /// <summary>
    /// 経過ターン
    /// </summary>
    private int turned;


    private bool endBattle = false;
    /// <summary>
    /// バトル中かどうか
    /// </summary>
    private CONST.BATTLE.PHASES_STATUS currentBattlePhase = CONST.BATTLE.PHASES_STATUS.INITIALIZE;


    void Start()
    {

        // バトルUIを非表示にする
        switchBattleUI(false);

        // セットアップ
        SetUp();

        this.currentBattlePhase = CONST.BATTLE.PHASES_STATUS.P_ACTION_SELECTING;

    }

    public void SetUp()
    {
        //行動リストの初期化
        this.battleActionList.Clear_AllActionList();

        //バトルUIの表示
        switchBattleUI(true);
        playerUI.SetUpUI(partyMember);

        // 敵オブジェクトを生成
        // TODO: Questシーンから情報を流すことでそれに添うEnemyを生成できるようにしたい
        CreateEnemyObjects(new string[] { CONST.ENEMY_NAMES.TEST });

        //経過ターンをリセット
        turned = 0;
    }

    /// <summary>
    /// バトル実行
    /// </summary>
    /// <returns></returns>
    public async void battle()
    {
        this.currentBattlePhase = CONST.BATTLE.PHASES_STATUS.DO_BATTLE;

        // 敵のアクションを登録する
        this.actionSelect();

        // アクション実行順などの処理
        this.actionOrderSorting();

        /// バトルが終わるまで待つ
        await this.doAction();
    }

    /// <summary>
    /// 敵アクションの選択
    /// </summary>
    /// <returns></returns>
    private void actionSelect()
    {
        //プレイヤーの動きを全体行動リストに登録
        battleActionList.AddRangeToAllActionList(battleActionList.GetP_ActionList());

        //敵アクションの登録
        this.setAction_Enemy((int)CONST.BATTLE_ACTION.COMMAND.Attack, this.enemy);

        //選択UIを削除し
        this.switchActionSelectUI(false);
    }

    /// <summary>
    /// アクションリストに登録された行動を順番に実行する
    /// </summary>
    /// <returns></returns>
    private async UniTask doAction()
    {
        foreach (BattleAction allAction in battleActionList.GetAllActionList())
        {
            //キャラ識別
            int role = allAction.character.char_role;

            // TODO: デバック用なのであとで消す
            Debug.Log(allAction.action);
            //アクションによって識別
            switch (allAction.action)
            {
                //攻撃
                case CONST.BATTLE_ACTION.COMMAND.Attack:
                    //プレイヤーの場合
                    if (role == CONST.CHARCTOR.PLAYER)
                    {
                        await this.PlayerAttack(partyMember);

                    }
                    //敵の場合
                    else
                    {
                        await this.EnemyAttack(enemy);
                    }
                    break;

                // アビリティ
                case CONST.BATTLE_ACTION.COMMAND.Ability:
                    if (role == CONST.CHARCTOR.PLAYER)
                    {
                        // TODO 使用者と対象のキャラ情報ははAllActionListに格納できるようにしたい
                        // 引数に指定しない
                        await abilityManager.execAbility(partyMember, enemy, allAction.abilityName, allAction.abilityActions);
                    }
                    else
                    {
                        await abilityManager.execAbility(enemy, partyMember, allAction.abilityName, allAction.abilityActions);
                        // TODO 一時的に記述 HPなどのUIへの反映を動的に監視できるようにしたい

                    }
                    break;

                // 防御
                case CONST.BATTLE_ACTION.COMMAND.Defence:
                    await this.Defense(allAction.character);
                    break;
                // アイテム
                case CONST.BATTLE_ACTION.COMMAND.Item:
                    if (role == CONST.CHARCTOR.PLAYER)
                    {
                        await itemManager.ExecItem(partyMember, enemy, allAction.itemName);
                    }
                    else
                    {
                        await itemManager.ExecItem(enemy, partyMember, allAction.itemName);
                        // TODO 一時的に記述 HPなどのUIへの反映を動的に監視できるようにしたい
                    }
                    break;
                default:
                    Debug.Log("不正なアクションが登録されました");
                    break;
            }

            // 敵HPが尽きたとき
            // TODO: 現在は1v1を想定して作られている
            if (updateBattleState(enemy))
            {
                endBattle = true;
                break;
            }

        }

        // 戦闘終了なら
        if (endBattle)
        {
            this.currentBattlePhase = CONST.BATTLE.PHASES_STATUS.RESULT_BATTLE;
            //バトル終了処理
            this.EndBattle();

            // クエストシーンに遷移する 
            this.LoadQuestScene();
        }
        else
        {
            //ターン終了処理
            this.turnFinalize();
        }
    }

    /// <summary>
    /// プレイヤー攻撃コマンド処理
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private async UniTask PlayerAttack(PartyMember player)
    {
        //Playerが攻撃
        player.Attack(enemy);
        Debug.Log($"{enemy.GetName()}:HP={enemy.GetHp()}");
        await UniTask.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));
    }

    /// <summary>
    /// 敵の攻撃処理
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    private async UniTask EnemyAttack(EnemyManager enemy)
    {
        enemy.Attack(partyMember);
        await UniTask.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));

    }

    /// <summary>
    /// 防御コマンド処理
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    private async UniTask Defense(CharBase character)
    {
        //指定したキャラクタの防御フラグをtrueにする
        character.Defense();

        await UniTask.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));

    }

    /// <summary>
    /// ターン終了処理
    /// </summary>
    void turnFinalize()
    {
        this.currentBattlePhase = CONST.BATTLE.PHASES_STATUS.END_TURN;
        //パラメータの初期化
        this.resetCharBuffANDunbuff(this.battleActionList.GetAllActionList());

        //行動リストの初期化
        this.battleActionList.Clear_AllActionList();

        //アクション選択UIを表示
        this.switchActionSelectUI(true);


        //ターンを追加
        this.turned++;
        Debug.Log("ターン:" + this.turned);

        this.currentBattlePhase = CONST.BATTLE.PHASES_STATUS.P_ACTION_SELECTING;
    }

    /// <summary>
    /// バトル終了処理
    /// </summary>
    void EndBattle()
    {
        // プレイヤーサイドのステータスを更新する
        PlayerData.instance.UpdatePlayerData(this.partyMember.GetCharParameters());
        // UIを非表示にする
        this.switchBattleUI(false);
    }

    /// <summary>
    /// 敵HPが尽きたときの処理
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
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
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.COMMAND.Attack, character);
        act.setAbilityName("Attack");
        act.SetSpeedRank(this.abilityManager.getAbilitySpeedRank("Attack"));
        setActionList_FOR_Role(character.char_role, act);

        if (this.battleActionList.GetP_ActionList().Count == character.countActionATurn && character.char_role == CONST.CHARCTOR.PLAYER)
        {
            this.battle();
        }
    }

    /// <summary>防御アクションを登録</summary>
    public void setAction_Defence(CharBase character)
    {
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.COMMAND.Defence, character);
        act.setAbilityName("Deffence");
        act.SetSpeedRank(this.abilityManager.getAbilitySpeedRank("Deffence"));
        setActionList_FOR_Role(character.char_role, act);

        if (this.battleActionList.GetP_ActionList().Count == character.countActionATurn)
        {
            this.battle();
        }
    }

    /// <summary>アビリティコマンドを登録</summary>
    public void setAction_Ability(CharBase character, string selectedAbilityName, List<CONST.ACTION.Ability_Action_Cell> ability_Actions)
    {
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.COMMAND.Ability, character);
        // アビリティ名を格納
        act.setAbilityName(selectedAbilityName);
        act.SetSpeedRank(this.abilityManager.getAbilitySpeedRank(selectedAbilityName));
        act.setAbilityAction(ability_Actions);

        setActionList_FOR_Role(character.char_role, act);

        // アビリティウインドウを閉じる
        this.abilityUI.removeAbilityWindow();
        if (this.battleActionList.GetP_ActionList().Count == character.countActionATurn)
        {
            this.battle();
        }

    }

    /// <summary>
    /// アイテムコマンドを登録
    /// </summary>
    /// <param name="character">選択キャラ</param>
    /// <param name="selectedItemName">選択されたアイテム名</param>
    public void setAction_Item(CharBase character, string selectedItemName)
    {
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.COMMAND.Item, character);

        // アビリティ名を格納
        act.setItemName(selectedItemName);
        act.SetSpeedRank(this.itemManager.getItemSpeedRankForItemName(selectedItemName));

        setActionList_FOR_Role(character.char_role, act);

        // アイテムウインドウを閉じる
        this.itemUI.removeItemWindow();

        if (this.battleActionList.GetP_ActionList().Count == character.countActionATurn)
        {
            this.battle();
        }

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

    /// <summary>
    /// 現在のバトルシーンが何をしているのかを返す
    /// </summary>
    /// <returns></returns>
    public CONST.BATTLE.PHASES_STATUS getCurrentBattlePhase()
    {
        return this.currentBattlePhase;
    }

    // TODO: 関数自体を見直す必要あり
    private void setActionList_FOR_Role(int character_role, BattleAction act)
    {
        if (character_role == CONST.CHARCTOR.PLAYER)
        {
            this.battleActionList.SetActionToPlayer(act);
        }
        else if (character_role == CONST.CHARCTOR.ENEMY)
        {
            this.battleActionList.SetAllActionToEnemy(act);
        }
    }

    // 敵の行動を登録する
    private void setAction_Enemy(int command, CharBase enemy)
    {
        for (int i = 0; i < enemy.countActionATurn; i++)
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
    }

    // バトルUIを表示または非表示
    private void switchBattleUI(bool isShowUI)
    {
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
        actList.AddRange(this.sortActionListForAbilityPriority(this.battleActionList.GetAllActionList()));
        this.battleActionList.ClearAllActionList();
        this.battleActionList.AddRangeToAllActionList(actList);

    }

    //行動順の並び替え
    private List<BattleAction> sortActionListForAbilityPriority(List<BattleAction> allAction)
    {
        List<BattleAction> resultList = new List<BattleAction>();
        List<BattleAction> Fast_Action_List = new List<BattleAction>();
        List<BattleAction> Nomal_Action_List = new List<BattleAction>();
        List<BattleAction> Delay_Action_List = new List<BattleAction>();


        //三種類の優先度に分けリストに入れる
        foreach (BattleAction charAction in allAction)
        {
            //防御、またはFAだった時は先頭に持ってくる(同値はSPD比較)
            // TODO DA条件式は必要になったときに追加
            if (charAction.action == CONST.BATTLE_ACTION.COMMAND.Defence ||
                charAction.action == CONST.BATTLE_ACTION.COMMAND.Attack ||
                charAction.action == CONST.BATTLE_ACTION.COMMAND.Item ||
                abilityManager.getAbilityTimingType(charAction.abilityName) == CONST.ACTION.Speed.Fast)
            {
                Fast_Action_List.Add(charAction);
            }
            else if (abilityManager.getAbilityTimingType(charAction.abilityName) == CONST.ACTION.Speed.Normal)
            {
                Nomal_Action_List.Add(charAction);
            }
            else
            {
                Delay_Action_List.Add(charAction);
            }
        }

        // ソート仕様
        // 1 早 Fast < Normal< Delay 遅　の順番に実行
        // 2 それぞれの区分内で実行者のspdを参照しソート
        // 3 同値の場合それぞれのアビリティor アイテムの実行優先度を参照してソート
        Fast_Action_List.OrderByDescending(action => action.character.GetSpeed())
                        .ThenBy(action => action.speed_rank);

        Nomal_Action_List.OrderByDescending(action => action.character.GetSpeed())
                         .ThenBy(action => action.speed_rank);

        Delay_Action_List.OrderByDescending(action => action.character.GetSpeed())
                         .ThenBy(action => action.speed_rank);

        //リストに追加
        resultList.AddRange(Fast_Action_List);
        resultList.AddRange(Nomal_Action_List);

        // ログ出力
        Debug.Log("result_LIST------------");
        int count = 1;
        foreach (BattleAction action in resultList)
        {
            Debug.Log(count.ToString() + ":" + action.character.GetName());
            Debug.Log("action : " + (action.action == CONST.BATTLE_ACTION.COMMAND.Ability ? action.abilityName : action.itemName));
            count++;
        }
        Debug.Log("--------------------------");

        return resultList;
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

    /// <summary>
    /// 敵オブジェクトを生成する
    /// </summary>
    private void CreateEnemyObjects(string[] enemyNames)
    {
        // TODO: 現状敵は一体のみなので敵オブジェクト一つを対象にしている)
        foreach (var enemyName in enemyNames)
        {
            var targetPrefab = (GameObject)Resources.Load($"Prefabs/Enemy/{enemyName}");
            Instantiate(targetPrefab, new Vector3((float)-1, (float)0.16, 0), Quaternion.identity);

            // 敵情報を取得する
            // TODO: 現状敵は一体のみなので敵オブジェクト一つを対象にしている
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
        }
    }

    /// <summary>
    /// クエストシーンに遷移する
    /// </summary>
    private void LoadQuestScene()
    {
        SceneManager.LoadScene(CONST.SCENE.Scene.Quest.ToString());
    }
}
