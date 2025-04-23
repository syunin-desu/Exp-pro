using System.Collections.Generic;
using UnityEngine;

public class EquipMenuManager : MonoBehaviour
{
    [SerializeField]
    private QuestManager _questManager;
    public EquipMenuUIManager equipMenuUIManager;

    [SerializeField]
    private CONST.EQUIP_MENU_STATUS.MenuStatus currentStatus;

    public void ShowEquipMenu()
    {
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.AbilityMenu);
        this.currentStatus = CONST.EQUIP_MENU_STATUS.MenuStatus.SelectEquipParts;
        equipMenuUIManager.ShowEquipMenu();
        //List<Ability_base> hasAbilityList = partyMember.GetHavingAbilitiesForCategory(CONST.ABILITY.Category.Magic);
        //abilityMenuScrollManager.SetupAbilityUI(hasAbilityList);
    }
}
