using UnityEngine;

public class AbilityMenuInputManager : PlayerMenuInputManager
{
    public AbilityMenuManager abilityMenuManager;

    public override void KeyInput_OpenMenu()
    {
        this.Close_AbilityMenu();
        base.KeyInput_OpenMenu();
    }

    public override void KeyInput_Return()
    {
        switch (abilityMenuManager.GetCurrentAbilityMenuStatus())
        {
            case CONST.ABILITY_MENU_STATUS.MenuStatus.HowAbility:
                this.Close_AbilityMenu();
                break;
            case CONST.ABILITY_MENU_STATUS.MenuStatus.SelectAbility:
                abilityMenuManager.CloseAbilitySelectedMenu();
                break;
        }
    }

    private void Close_AbilityMenu()
    {
        abilityMenuManager.CloseAbilityMenu();
    }
}
