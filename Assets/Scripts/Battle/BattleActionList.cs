using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    /// <summary>
    /// アビリティ発動時に実行するアクション
    /// </summary>
    public List<CONST.ACTION.Ability_Action_Cell> abilityActions = new List<CONST.ACTION.Ability_Action_Cell>();

    /// <summary>
    /// 実行優先度
    /// </summary>
    public int speed_rank;

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
    /// アビリティアクションを格納
    /// </summary>
    /// <param name="abilityName"></param>
    public void setAbilityAction(List<CONST.ACTION.Ability_Action_Cell> abilityActions)
    {
        this.abilityActions = abilityActions;
    }

    /// <summary>
    /// アイテム名を設定
    /// </summary>
    /// <param name="itemName"></param>
    public void setItemName(string itemName)
    {
        this.itemName = itemName;

    }

    /// <summary>
    /// 実行優先度を設定
    /// </summary>
    /// <param name="speedRank"></param>
    public void SetSpeedRank(int speedRank)
    {
        this.speed_rank = speedRank;
    }
}


public class BattleActionList : MonoBehaviour
{
    /// <summary>
    /// プレイヤー行動リスト
    /// </summary>
    public List<BattleAction> p_actionList = new List<BattleAction>();

    /// <summary>
    /// 戦闘参加者全員の行動リスト
    /// </summary>
    public List<BattleAction> allActionList = new List<BattleAction>();

    /// <summary>
    /// 行動選択リストUI
    /// </summary>
    public ActionRowNumberListScrollController actionRowNumberListScrollController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // プレイヤーアクションを登録する
    public void SetActionToPlayer(BattleAction p_act)
    {
        this.p_actionList.Add(p_act);
        // 行動リストUIを更新
        this.actionRowNumberListScrollController.Setup_ActionRowNumberListUI(this.p_actionList);
    }

    // 全行動リストに敵アクションを登録する
    public void SetAllActionToEnemy(BattleAction e_act)
    {
        this.allActionList.Add(e_act);
    }


    //全体行動リストに登録
    public void AddRangeToAllActionList(List<BattleAction> p_ActionList)
    {
        this.allActionList.AddRange(p_ActionList);
    }

    /// <summary>
    /// 最新のアクションを削除する
    /// </summary>
    public void RemoveLatestAction()
    {
        if (this.p_actionList.Count != 0)
        {
            this.p_actionList.Remove(this.p_actionList.Last());
            // 行動リストUIを更新
            this.actionRowNumberListScrollController.Setup_ActionRowNumberListUI(this.p_actionList);
        }
    }

    // 全行動リストの初期化
    public void Clear_AllActionList()
    {
        this.p_actionList.Clear();
        this.allActionList.Clear();
        // 行動リストUIを更新
        this.actionRowNumberListScrollController.Setup_ActionRowNumberListUI(this.p_actionList);
    }

    public List<BattleAction> GetP_ActionList() { return this.p_actionList; }
    public List<BattleAction> GetAllActionList() { return this.allActionList; }

    public void ClearP_ActionList() { this.p_actionList.Clear(); }
    public void ClearAllActionList() { this.allActionList.Clear(); }



}
