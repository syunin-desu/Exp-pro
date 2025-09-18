using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuManager : MonoBehaviour
{
    public ItemMenuUIManager itemMenuUIManager;
    public ItemMenuScrollManager itemMenuScrollManager;
    public ItemManager itemManager;
    public ItemMenuDescriptionUI itemMenuDescriptionUI;
    public HadItem hadItem;
    [SerializeField]
    private MenuSelectionUtility gameObjectUtility = new MenuSelectionUtility();

    private CONST.ITEM_MENU_STATUS.MenuStatus currentStatus;

    public List<RectTransform> ItemButtonList = new List<RectTransform>();
    public List<GameObject> HowButtons = new List<GameObject>();

    public PartyMember playerParam;

    public void SetUpItemMenu()
    {
        this.currentStatus = CONST.ITEM_MENU_STATUS.MenuStatus.HowItem;
        itemMenuUIManager.ShowItemMenu();
        var hasItemList = hadItem.GetHavingItem();
        this.ItemButtonList = itemMenuScrollManager.SetupItemUI(hasItemList);

        // ボタン選択状態を初期化
        this.ClearHowItemButtonSelected(HowButtons);
        itemMenuUIManager.AllSelectedHowButtonUnSelected(HowButtons);
        this.HowButtons[0].GetComponent<SelectedStatus>().UpdateIsSelected(true);
        this.itemMenuUIManager.UpdateHowButtonColor(this.HowButtons[0]);


        this.UpdateItemSelected(ItemButtonList[0].gameObject);

    }

    public void SetUpItemSelectedMenu()
    {
        this.currentStatus = CONST.ITEM_MENU_STATUS.MenuStatus.SelectItem;
        this.UpdateCanSelectHowItemButtons(false);
        this.UpdateCanSelectItemButton(true);

        // アイテム選択状態を初期化
        //全アイテムの選択中ステータスをFalseにする
        itemMenuScrollManager.ClearItemButtonSelected();
        itemMenuUIManager.AllSelectedIconDisable();
        // 選択されたアイテムを選択状態にする
        ItemButtonList[0].GetComponentInChildren<SelectedStatus>().UpdateIsSelected(true);
        TextMeshProUGUI target_obj = ItemButtonList[0].transform.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
        itemMenuUIManager.UpdateItemSelectedIcon(target_obj, true);

    }

    public void CloseItemMenu()
    {
        itemMenuScrollManager.RemoveAllItem();
        itemMenuUIManager.CloseItemMenu();

        // PlayerDataを更新
        PlayerData.instance.UpdatePartyMemberParam(playerParam.GetCharParameters());
    }

    public void CloseItemSelectedMenu()
    {
        this.currentStatus = CONST.ITEM_MENU_STATUS.MenuStatus.HowItem;
        itemMenuUIManager.AllSelectedIconDisable();
        itemMenuScrollManager.ClearItemButtonSelected();
        this.UpdateDiscription("");
        this.UpdateCanSelectHowItemButtons(true);
        this.UpdateCanSelectItemButton(false);

        // PlayerDataを更新
        PlayerData.instance.UpdatePartyMemberParam(playerParam.GetCharParameters());
    }

    public void ItemButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE selectedType)
    {
        switch (this.currentStatus)
        {
            case CONST.ITEM_MENU_STATUS.MenuStatus.HowItem:
                gameObjectUtility.ClickedButtonsInALow(HowButtons,
                    selectedType
                    );
                break;
            case CONST.ITEM_MENU_STATUS.MenuStatus.SelectItem:
                gameObjectUtility.ClickedButtonsInTwoLow(ItemButtonList.Select(i => i.gameObject).ToList(),
                    selectedType);
                break;
        }
    }

    public void ItemButtonFromKeyInputShiftColumn(CONST.MENU.SELECTEDTYPE selectedType)
    {
        switch (this.currentStatus)
        {
            case CONST.ITEM_MENU_STATUS.MenuStatus.SelectItem:
                gameObjectUtility.ClickedButtonsInTwoLow(ItemButtonList.Select(i => i.gameObject).ToList(),
                    selectedType);
                break;
        }
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

    public void UpdateItemSelected(GameObject gameObject)
    {
        //全アイテムの選択中ステータスをFalseにする
        itemMenuScrollManager.ClearItemButtonSelected();
        itemMenuUIManager.AllSelectedIconDisable();

        // 選択されたアイテムを選択状態にする
        gameObject.GetComponentInChildren<SelectedStatus>().UpdateIsSelected(true);
        TextMeshProUGUI target_obj = gameObject.transform.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
        itemMenuUIManager.UpdateItemSelectedIcon(target_obj, true);
        itemMenuUIManager.resetListScroll(gameObject.GetComponent<RectTransform>());

        // Descriptionを更新
        this.UpdateDiscription(itemManager.GetItemDiscriptionfromMaster(gameObject.GetComponent<ItemButtonClicked>().item_id) ?? "");
    }

    public void ResetHowButtonSelected(GameObject gameObject)
    {
        this.ClearHowItemButtonSelected(this.HowButtons);
        itemMenuUIManager.AllSelectedHowButtonUnSelected(this.HowButtons);

        gameObject.GetComponent<SelectedStatus>().UpdateIsSelected(true);
        itemMenuUIManager.UpdateHowButtonColor(gameObject);
    }

    private void ClearHowItemButtonSelected(List<GameObject> targets)
    {
        foreach (var item in targets)
        {
            item.GetComponent<SelectedStatus>().UpdateIsSelected(false);
        }
    }
}
