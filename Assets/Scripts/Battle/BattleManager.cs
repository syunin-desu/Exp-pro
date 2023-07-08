using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

// 実行アクション情報クラス
public class BattleAction
{
    /// <summary>
    /// 実行アクション
    /// </summary>
    public CONST.BATTLE_ACTION.COMMAND action;

    /// <summary>
    /// アクション名
    /// </summary>
    public string abilityName = "";

    /// <summary>
    ///  アイテム名
    /// </summary>
    public string itemName = "";

    /// <summary>
    /// 実行キャラクタ
    /// </summary>
    public CharBase character;


    public BattleAction(CONST.BATTLE_ACTION.COMMAND action, CharBase character)
    {
        this.action = action;
        this.character = character;
    }

    /// <summary>
    /// アビリティ名を格納
    /// </summary>
    /// <param name="abilityName"></param>
    public void setAbilityName(string abilityName)
    {
        this.abilityName = abilityName;
    }

    /// <summary>
    /// アイテム名を設定
    /// </summary>
    /// <param name="itemName"></param>
    public void setItemName(string itemName)
    {
        this.itemName = itemName;

    }
}

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
    /// 経過ターン
    /// </summary>
    private int turned;

    /// <summary>
    /// プレイヤー行動リスト
    /// </summary>
    [SerializeField]
    List<BattleAction> actionList;

    /// <summary>
    /// 戦闘参加者全員の行動リスト
    /// </summary>
    [SerializeField]
    public List<BattleAction> allActionList;

    //Playerパーティーの人数
    // TODO あとでパーティ情報はクラスなどにまとめる
    private int char_Num = 1;

    private bool endBattle = false;


    void Start()
    {

        //リストのセット
        actionList = setActionList<BattleAction>();
        allActionList = setActionList<BattleAction>();

        // バトルUIを非表示にする
        switchBattleUI(false);

        // セットアップ
        SetUp();

    }

    public void SetUp()
    {
        //行動リストの初期化
        clear_actionList();
        //バトルUIの表示
        switchBattleUI(true);
        playerUI.SetUpUI(partyMember);

        // 敵オブジェクトを生成
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
        //アクション選択フェーズ
        // 選択されるまで待つ
        this.actionSelect();

        // アクション実行順などの処理
        this.actionOrderSorting();

        /// バトルが終わるまで待つ
        await this.doAction();
    }

    /// <summary>
    /// プレイヤーアクション選択待機およびリストへの登録
    /// </summary>
    /// <returns></returns>
    private void actionSelect()
    {
        //人数分アクションが選択されたらバトルさせる
        //プレイヤーの動きを全体行動リストに登録
        allActionList.AddRange(actionList);

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
        foreach (BattleAction allAction in allActionList)
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
                        await abilityManager.execAbility(partyMember, enemy, allAction.abilityName);
                    }
                    else
                    {
                        await abilityManager.execAbility(enemy, partyMember, allAction.abilityName);
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
        Debug.Log($"{enemy.enemyData.name}:HP={enemy.hp}");
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

    /// <summary>
    /// バトル終了処理
    /// </summary>
    void EndBattle()
    {
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
        setActionList_FOR_Role(character.char_role, act);

        if (actionList.Count == char_Num && character.char_role == CONST.CHARCTOR.PLAYER)
        {
            this.battle();
        }
    }

    /// <summary>防御アクションを登録</summary>
    public void setAction_Defence(CharBase character)
    {
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.COMMAND.Defence, character);
        setActionList_FOR_Role(character.char_role, act);

        if (actionList.Count == char_Num)
        {
            this.battle();
        }
    }

    /// <summary>アビリティコマンドを登録</summary>
    public void setAction_Ability(CharBase character, string selectedAbilityName)
    {
        BattleAction act = new BattleAction(CONST.BATTLE_ACTION.COMMAND.Ability, character);
        // アビリティ名を格納
        act.setAbilityName(selectedAbilityName);

        setActionList_FOR_Role(character.char_role, act);

        // アビリティウインドウを閉じる
        this.abilityUI.removeAbilityWindow();
        if (actionList.Count == char_Num)
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

        setActionList_FOR_Role(character.char_role, act);

        // アイテムウインドウを閉じる
        this.itemUI.removeItemWindow();

        if (actionList.Count == char_Num)
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

    // TODO: 関数自体を見直す必要あり
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
            if (charAction.action == CONST.BATTLE_ACTION.COMMAND.Defence ||
             abilityManager.getAbilityTimingType(charAction.abilityName) == CONST.ACTION.Speed.Fast)
            {
                FAlist.Add(charAction);
            }
            // TODO アイテムをNormalにするが将来的にはItemDataにTimingTypeを設定させたほうがいいと思う
            else if (charAction.action == CONST.BATTLE_ACTION.COMMAND.Attack ||
            charAction.action == CONST.BATTLE_ACTION.COMMAND.Item ||
            abilityManager.getAbilityTimingType(charAction.abilityName) == CONST.ACTION.Speed.Normal)
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

    /// <summary>
    /// 敵オブジェクトを生成する
    /// </summary>
    private void CreateEnemyObjects(string[] enemyNames)
    {
        // TODO: 現状敵は一体のみなので敵オブジェクト一つを対象にしている)
        foreach (var enemyName in enemyNames)
        {
            var targetPrefab = (GameObject)Resources.Load($"Prefabs/Enemy/{enemyName}");
            Instantiate(targetPrefab, new Vector3((float)0.05, (float)0.16, 0), Quaternion.identity);
            
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
