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

    public override void KeyInput_Up()
    {
        abilityMenuManager.AbilityButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.PREV);
    }

    public override void KeyInput_Down()
    {
        abilityMenuManager.AbilityButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.NEXT);
    }

    public override void KeyInput_Enter()
    {
        abilityMenuManager.AbilityButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.ENTER);
    }

    public override void KeyInput_Left()
    {
        abilityMenuManager.AbilityButtonFromKeyInputShiftColumn(CONST.MENU.SELECTEDTYPE.PREVCOLUMN);
    }

    public override void KeyInput_Right()
    {
        abilityMenuManager.AbilityButtonFromKeyInputShiftColumn(CONST.MENU.SELECTEDTYPE.NEXTCOLUMN);
    }


    private void Close_AbilityMenu()
    {
        abilityMenuManager.CloseAbilityMenu();
    }
}
