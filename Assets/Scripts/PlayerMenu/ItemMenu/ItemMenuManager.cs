using UnityEngine;
using UnityEngine.UI;

public class ItemMenuManager : MonoBehaviour
{
    public ItemMenuUIManager itemMenuUIManager;
    public ItemMenuScrollManager itemMenuScrollManager;
    public ItemManager itemManager;

    private CONST.ITEM_MENU_STATUS.MenuStatus currentStatus;


    public void SetUpItemMenu()
    {
        this.currentStatus = CONST.ITEM_MENU_STATUS.MenuStatus.HowItem;
        itemMenuUIManager.ShowItemMenu();
        var hasItemList = PlayerData.instance.GetItems();
        itemMenuScrollManager.SetupItemUI(hasItemList);
    }

    public void CloseItemMenu()
    {
        itemMenuScrollManager.RemoveAllItem();
        itemMenuUIManager.CloseItemMenu();
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
        var buttons = GameObject.FindGameObjectsWithTag("ItemButton");
        //表示しているボタンの削除 
        foreach (var button in buttons)
        {
            button.GetComponentInChildren<Button>().enabled = canSelect;

        }
    }

    public async void ExecuteItem(CharBase performChar, string execItemName)
    {
        await itemManager.ExecItem(performChar, null, execItemName, false);
    }

    public void SetCurrentItemMenuStatus(CONST.ITEM_MENU_STATUS.MenuStatus currentStatus)
    {
        this.currentStatus = currentStatus;
    }

    public CONST.ITEM_MENU_STATUS.MenuStatus GetCurrentItemMenuStatus()
    {
        return this.currentStatus;
    }
}
