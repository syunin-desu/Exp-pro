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
                itemMenuManager.UpdateCanSelectHowItemButtons(true);
                itemMenuManager.UpdateCanSelectItemButton(false);
                itemMenuManager.SetCurrentItemMenuStatus(CONST.ITEM_MENU_STATUS.MenuStatus.HowItem);
                break;
        }
    }

    private void Close_ItemMenu()
    {
        itemMenuManager.CloseItemMenu();
        questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu);
    }
}
