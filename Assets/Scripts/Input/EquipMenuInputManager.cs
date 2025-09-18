using UnityEngine;

public class EquipMenuInputManager : PlayerMenuInputManager
{
    public EquipMenuManager equipMenuManager;
    public EquipMenuScrollManager equipMenuScrollManager;

    public override void KeyInput_OpenMenu()
    {
        equipMenuManager.CloseEquipMenu();
        base.KeyInput_OpenMenu();
    }

    public override void KeyInput_Return()
    {
        switch (equipMenuManager.GetCurrentEquipMenuStatus())
        {
            case CONST.EQUIP_MENU_STATUS.MenuStatus.SelectEquip:
                equipMenuManager.CloseEquipSeletedMenu();
                break;
            case CONST.EQUIP_MENU_STATUS.MenuStatus.SelectEquipParts:
                equipMenuManager.CloseEquipMenu();
                break;
        }
    }

    public override void KeyInput_Up()
    {
        equipMenuManager.EquipButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.PREV);
    }

    public override void KeyInput_Down()
    {
        equipMenuManager.EquipButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.NEXT);
    }

    public override void KeyInput_Enter()
    {
        equipMenuManager.EquipButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.ENTER);
    }

    public override void KeyInput_Left()
    {

    }

    public override void KeyInput_Right()
    {

    }
}
