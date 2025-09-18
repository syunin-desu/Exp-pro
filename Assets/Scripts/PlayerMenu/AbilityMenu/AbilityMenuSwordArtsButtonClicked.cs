using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuSwordArtsButtonClicked : MonoBehaviour, IButtonClicked
{
    public AbilityMenuManager abilityMenuManager;
    public AbilityMenuUIManager abilityMenuUIManager;
    public AbilityMenuScrollManager abilityMenuScrollManager;
    public PartyMember partyMember;

    public void OnClicked()
    {
        if (!this.gameObject.GetComponent<SelectedStatus>().GetIsSelected())
        {
            abilityMenuManager.ChangedHowButtonSelection(CONST.ABILITY.Category.SwordArts, this.gameObject);
            return;
        }

        // アビリティ選択状態に移行
        abilityMenuManager.ChangeToSelectAbility();
    }
}
