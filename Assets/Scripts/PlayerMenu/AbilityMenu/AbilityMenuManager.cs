using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class AbilityMenuManager : MonoBehaviour
{
    [SerializeField]
    private QuestManager _questManager;
    public AbilityManager abilityManager;
    public AbilityMenuUIManager abilityMenuUIManager;
    public AbilityMenuScrollManager abilityMenuScrollManager;
    public AbilityDescriptionUI itemMenuDescriptionUI;
    public PartyMember partyMember;

    [SerializeField]
    private CONST.ABILITY_MENU_STATUS.MenuStatus currentStatus;
    [SerializeField]
    private CONST.ABILITY.Category selectedAbilityCategory = CONST.ABILITY.Category.Default;

    public void SetAbilityMenu()
    {
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.AbilityMenu);
        this.currentStatus = CONST.ABILITY_MENU_STATUS.MenuStatus.HowAbility;
        abilityMenuUIManager.ShowItemMenu();
        selectedAbilityCategory = CONST.ABILITY.Category.Default;
        //Magicをデフォルトにする
        List<Ability_base> hasAbilityList = partyMember.GetHavingAbilitiesForCategory(CONST.ABILITY.Category.Magic);
        abilityMenuScrollManager.SetupAbilityUI(hasAbilityList);
    }

    public async void ExecuteAbilityOnPlayerMenu(CharBase performChar, string execAbilityID)
    {
        await abilityManager.execAbility(performChar, null, execAbilityID, abilityManager.getAbilityActionsForID(execAbilityID));
    }

    public CONST.ABILITY_MENU_STATUS.MenuStatus GetCurrentAbilityMenuStatus()
    {
        return currentStatus;
    }

    public CONST.ABILITY.Category GetCurrentAbilityCategory()
    {
        return selectedAbilityCategory;
    }

    public void UpdateAbilityMenuStatus(CONST.ABILITY_MENU_STATUS.MenuStatus targetAbilityMenuStatus)
    {
        currentStatus = targetAbilityMenuStatus;
    }

    public void UpdateAbilityCategory(CONST.ABILITY.Category targetAbilityCategory)
    {
        this.selectedAbilityCategory = targetAbilityCategory;
    }

    public void CloseAbilityMenu()
    {
        // 各アビリティの選択状態、説明表示をクリア
        this.ClearAbilitySelected();
        // アビリティボタンを活性化する
        abilityMenuUIManager.UpdateAbilityButtonEnable(false);
        // howtoボタン群を非活性にする
        abilityMenuUIManager.UpdatHowtoButtonEnable(true);
        abilityMenuUIManager.ClearAbiltyList();
        abilityMenuUIManager.CloseItemMenu();
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu);
        this.UpdateAbilityCategory(CONST.ABILITY.Category.Default);
    }
    public void CloseAbilitySelectedMenu()
    {
        // 各アビリティの選択状態、説明表示をクリア
        this.ClearAbilitySelected();
        // 
        // アビリティ選択状態に移行
        this.UpdateAbilityMenuStatus(CONST.ABILITY_MENU_STATUS.MenuStatus.HowAbility);
        this.UpdateAbilityCategory(CONST.ABILITY.Category.Default);
        // howtoボタン群を活性にする
        abilityMenuUIManager.UpdatHowtoButtonEnable(true);
        abilityMenuUIManager.UpdateAbilityButtonEnable(false);
    }

    public void UpdateDiscription(string discription)
    {
        itemMenuDescriptionUI.SetDiscription(discription);
    }

    public void ClearAbilitySelected()
    {
        abilityMenuScrollManager.ClearAbilityButtonSelected();
        abilityMenuUIManager.AllSelectedIconDisable();
        //TODO: 非活性色のままになる
        abilityMenuUIManager.ClearAllAbilityTextColor();
        this.UpdateDiscription("");
    }
}
