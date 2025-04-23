using UnityEngine;
using UnityEngine.UI;

public class ItemMenuManager : MonoBehaviour
{
    public ItemMenuUIManager itemMenuUIManager;
    public ItemMenuScrollManager itemMenuScrollManager;
    public ItemManager itemManager;
    public ItemMenuDescriptionUI itemMenuDescriptionUI;
    public HadItem hadItem;

    private CONST.ITEM_MENU_STATUS.MenuStatus currentStatus;

    public void SetUpItemMenu()
    {
        this.currentStatus = CONST.ITEM_MENU_STATUS.MenuStatus.HowItem;
        itemMenuUIManager.ShowItemMenu();
        var hasItemList = hadItem.GetHavingItem();
        itemMenuScrollManager.SetupItemUI(hasItemList);
    }

    public void SetUpItemSelectedMenu()
    {
        this.currentStatus = CONST.ITEM_MENU_STATUS.MenuStatus.SelectItem;
        this.UpdateCanSelectHowItemButtons(false);
        this.UpdateCanSelectItemButton(true);
    }

    public void CloseItemMenu()
    {
        itemMenuScrollManager.RemoveAllItem();
        itemMenuUIManager.CloseItemMenu();
    }

    public void CloseItemSelectedMenu()
    {
        this.currentStatus = CONST.ITEM_MENU_STATUS.MenuStatus.HowItem;
        itemMenuUIManager.AllSelectedIconDisable();
        itemMenuScrollManager.ClearItemButtonSelected();
        this.UpdateDiscription("");
        this.UpdateCanSelectHowItemButtons(true);
        this.UpdateCanSelectItemButton(false);
    }

    public void UpdateCanSelectHowItemButtons(bool canSelect)
    {
        var buttons = GameObject.FindGameObjectsWithTag("HowItemButton");
        //表示しているボタンの削除 
        foreach (var button in buttons)
        {
            button.GetComponentInChildren<Button>().enabled = canSelect;

        }

    }

    public void UpdateCanSelectItemButton(bool canSelect)
    {
        var buttons = GameObject.FindGameObjectsWithTag("PlayerMenuItembutton");
        //表示しているボタンの削除 
        foreach (var button in buttons)
        {
            button.GetComponentInChildren<Button>().enabled = canSelect;

        }
    }

    public async void ExecuteItem(CharBase performChar, string execItemID)
    {
        await itemManager.ExecItem(performChar, null, execItemID, false);
    }

    public void SetCurrentItemMenuStatus(CONST.ITEM_MENU_STATUS.MenuStatus currentStatus)
    {
        this.currentStatus = currentStatus;
    }

    public CONST.ITEM_MENU_STATUS.MenuStatus GetCurrentItemMenuStatus()
    {
        return this.currentStatus;
    }

    public void UpdateDiscription(string discription)
    {
        itemMenuDescriptionUI.SetDiscription(discription);
    }
}
