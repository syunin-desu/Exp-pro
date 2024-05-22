using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ���s�A�N�V�������N���X
public class BattleAction
{
    /// <summary>
    /// ���s�A�N�V����
    /// </summary>
    public CONST.BATTLE_ACTION.COMMAND action;

    /// <summary>
    /// �A�N�V������
    /// </summary>
    public string abilityName = "";

    /// <summary>
    ///  �A�C�e����
    /// </summary>
    public string itemName = "";

    /// <summary>
    /// ���s�L�����N�^
    /// </summary>
    public CharBase character;

    /// <summary>
    /// �A�r���e�B�������Ɏ��s����A�N�V����
    /// </summary>
    public List<CONST.ACTION.Ability_Action_Cell> abilityActions = new List<CONST.ACTION.Ability_Action_Cell>();

    /// <summary>
    /// ���s�D��x
    /// </summary>
    public int speed_rank;

    public BattleAction(CONST.BATTLE_ACTION.COMMAND action, CharBase character)
    {
        this.action = action;
        this.character = character;
    }

    /// <summary>
    /// �A�r���e�B�����i�[
    /// </summary>
    /// <param name="abilityName"></param>
    public void setAbilityName(string abilityName)
    {
        this.abilityName = abilityName;
    }

    /// <summary>
    /// �A�r���e�B�A�N�V�������i�[
    /// </summary>
    /// <param name="abilityName"></param>
    public void setAbilityAction(List<CONST.ACTION.Ability_Action_Cell> abilityActions)
    {
        this.abilityActions = abilityActions;
    }

    /// <summary>
    /// �A�C�e������ݒ�
    /// </summary>
    /// <param name="itemName"></param>
    public void setItemName(string itemName)
    {
        this.itemName = itemName;

    }

    /// <summary>
    /// ���s�D��x��ݒ�
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
    /// �v���C���[�s�����X�g
    /// </summary>
    public List<BattleAction> p_actionList = new List<BattleAction>();

    /// <summary>
    /// �퓬�Q���ґS���̍s�����X�g
    /// </summary>
    public List<BattleAction> allActionList = new List<BattleAction>();

    /// <summary>
    /// �s���I�����X�gUI
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

    // �v���C���[�A�N�V������o�^����
    public void SetActionToPlayer(BattleAction p_act)
    {
        this.p_actionList.Add(p_act);
        // �s�����X�gUI���X�V
        this.actionRowNumberListScrollController.Setup_ActionRowNumberListUI(this.p_actionList);
    }

    // �S�s�����X�g�ɓG�A�N�V������o�^����
    public void SetAllActionToEnemy(BattleAction e_act)
    {
        this.allActionList.Add(e_act);
    }


    //�S�̍s�����X�g�ɓo�^
    public void AddRangeToAllActionList(List<BattleAction> p_ActionList)
    {
        this.allActionList.AddRange(p_ActionList);
    }

    /// <summary>
    /// �ŐV�̃A�N�V�������폜����
    /// </summary>
    public void RemoveLatestAction()
    {
        if (this.p_actionList.Count != 0)
        {
            this.p_actionList.Remove(this.p_actionList.Last());
            // �s�����X�gUI���X�V
            this.actionRowNumberListScrollController.Setup_ActionRowNumberListUI(this.p_actionList);
        }
    }

    // �S�s�����X�g�̏�����
    public void Clear_AllActionList()
    {
        this.p_actionList.Clear();
        this.allActionList.Clear();
        // �s�����X�gUI���X�V
        this.actionRowNumberListScrollController.Setup_ActionRowNumberListUI(this.p_actionList);
    }

    public List<BattleAction> GetP_ActionList() { return this.p_actionList; }
    public List<BattleAction> GetAllActionList() { return this.allActionList; }

    public void ClearP_ActionList() { this.p_actionList.Clear(); }
    public void ClearAllActionList() { this.allActionList.Clear(); }



}
