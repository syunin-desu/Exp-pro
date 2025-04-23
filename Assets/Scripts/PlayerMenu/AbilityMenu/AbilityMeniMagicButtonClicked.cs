using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMeniMagicButtonClicked : MonoBehaviour
{
    public AbilityMenuManager abilityMenuManager;
    public AbilityMenuScrollManager abilityMenuScrollManager;
    public AbilityMenuUIManager abilityMenuUIManager;
    public PartyMember partyMember;

    public void OnclickedMagicButton()
    {
        if (abilityMenuManager.GetCurrentAbilityCategory() != CONST.ABILITY.Category.Magic)
        {
            // 現在のアビリティ表示をクリアする
            abilityMenuUIManager.ClearAbiltyList();
            abilityMenuScrollManager.ClearAbilityButtonSelected();

            abilityMenuManager.UpdateAbilityCategory(CONST.ABILITY.Category.Magic);
            // AbilityListを更新
            List<Ability_base> hasAbilityList = partyMember.GetHavingAbilitiesForCategory(
                CONST.ABILITY.Category.Magic);
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
