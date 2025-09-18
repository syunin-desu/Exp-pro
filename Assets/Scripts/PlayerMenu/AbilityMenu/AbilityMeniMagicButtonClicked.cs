using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMeniMagicButtonClicked : MonoBehaviour, IButtonClicked
{
    public AbilityMenuManager abilityMenuManager;
    public AbilityMenuScrollManager abilityMenuScrollManager;
    public AbilityMenuUIManager abilityMenuUIManager;
    public PartyMember partyMember;

    public void OnClicked()
    {
        if (!this.gameObject.GetComponent<SelectedStatus>().GetIsSelected())
        {
            abilityMenuManager.ChangedHowButtonSelection(CONST.ABILITY.Category.Magic, this.gameObject);
            return;
        }

        // アビリティ選択状態に移行
        abilityMenuManager.ChangeToSelectAbility();
    }
}
