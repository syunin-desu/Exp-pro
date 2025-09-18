using UnityEngine;

public class ItemMenuInputManager : PlayerMenuInputManager
{
    public ItemMenuManager itemMenuManager;

    public override void KeyInput_OpenMenu()
    {
        this.Close_ItemMenu();
        base.KeyInput_OpenMenu();
    }

    public override void KeyInput_Return()
    {
        switch (itemMenuManager.GetCurrentItemMenuStatus())
        {
            case CONST.ITEM_MENU_STATUS.MenuStatus.HowItem:
                this.Close_ItemMenu();
                break;
            case CONST.ITEM_MENU_STATUS.MenuStatus.SelectItem:
                itemMenuManager.CloseItemSelectedMenu();
                break;
        }
    }

    private void Close_ItemMenu()
    {
        itemMenuManager.CloseItemMenu();
        questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu);
    }

    public override void KeyInput_Up()
    {
        itemMenuManager.ItemButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.PREV);
    }

    public override void KeyInput_Down()
    {
        itemMenuManager.ItemButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.NEXT);
    }

    public override void KeyInput_Enter()
    {
        itemMenuManager.ItemButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.ENTER);
    }

    public override void KeyInput_Left()
    {
        itemMenuManager.ItemButtonFromKeyInputShiftColumn(CONST.MENU.SELECTEDTYPE.PREVCOLUMN);
    }

    public override void KeyInput_Right()
    {
        itemMenuManager.ItemButtonFromKeyInputShiftColumn(CONST.MENU.SELECTEDTYPE.NEXTCOLUMN);
    }
}
