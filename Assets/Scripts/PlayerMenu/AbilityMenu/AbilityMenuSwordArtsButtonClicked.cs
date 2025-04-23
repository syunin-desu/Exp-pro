using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuSwordArtsButtonClicked : MonoBehaviour
{
    public AbilityMenuManager abilityMenuManager;
    public AbilityMenuUIManager abilityMenuUIManager;
    public AbilityMenuScrollManager abilityMenuScrollManager;
    public PartyMember partyMember;

    public void OnclickedSwordArtsButton()
    {
        if (abilityMenuManager.GetCurrentAbilityCategory() != CONST.ABILITY.Category.SwordArts)
        {
            // 現在のアビリティ表示をクリアする
            abilityMenuUIManager.ClearAbiltyList();
            abilityMenuScrollManager.ClearAbilityButtonSelected();

            abilityMenuManager.UpdateAbilityCategory(CONST.ABILITY.Category.SwordArts);
            abilityMenuManager.UpdateAbilityMenuStatus(CONST.ABILITY_MENU_STATUS.MenuStatus.SelectAbility);
            // AbilityListを更新
            List<Ability_base> hasAbilityList = partyMember.GetHavingAbilitiesForCategory(
                CONST.ABILITY.Category.SwordArts);
            abilityMenuScrollManager.SetupAbilityUI(hasAbilityList);
            return;
        }

        // アビリティ選択状態に移行
        abilityMenuManager.UpdateAbilityMenuStatus(CONST.ABILITY_MENU_STATUS.MenuStatus.SelectAbility);
        // アビリティボタンを活性化する
        abilityMenuUIManager.UpdateAbilityButtonEnable(true);
        // howtoボタン群を非活性にする
        abilityMenuUIManager.UpdatHowtoButtonEnable(false);
    }
}
