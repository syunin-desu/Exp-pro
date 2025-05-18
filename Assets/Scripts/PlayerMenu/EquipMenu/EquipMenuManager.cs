using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class EquipMenuManager : MonoBehaviour
{
    [SerializeField]
    private QuestManager _questManager;
    public EquipMenuUIManager equipMenuUIManager;
    public EquipMenuScrollManager equipMenuScrollManager;

    [SerializeField]
    private CONST.EQUIP_MENU_STATUS.MenuStatus currentStatus;

    public PartyMember wpartyMember;
    public HadItem hadItem;

    public ItemManager itemManager;

    private void Update()
    {
        PlayerEquipData targetEquipList = wpartyMember.GetArmedEquip();
        equipMenuUIManager.UpdateArmedEquipName(targetEquipList);
    }

    public void ShowEquipMenu()
    {
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.AbilityMenu);
        this.currentStatus = CONST.EQUIP_MENU_STATUS.MenuStatus.SelectEquipParts;
        equipMenuUIManager.ShowEquipMenu();
        var hasItemList = hadItem.GetHavingItem();
        PlayerEquipData targetEquipList = wpartyMember.GetArmedEquip();
        equipMenuScrollManager.SetupEquipListUI(targetEquipList);
    }

    public void ClearEquipPartsButtonSelected()
    {
        // 既に表示されていれば表示内容を全削除
        var items = GameObject.FindGameObjectsWithTag("PlayerMenuEquipButton");

        foreach (var item in items)
        {
            item.GetComponent<EquipMenuArnedPartsButtonClicked>().isSelected = false;
        }
    }

    public void UpdateArmedEquip(string itemID, CONST.ITEM.CATEGORY partsCategory)
    {
        // 装備を更新
        EquipBase targetEquip = itemManager.GetEquipDataFromMaster(itemID);
        if (targetEquip is null)
        {
            return;
        }
        EquipBase equipedItem = this.wpartyMember.UpdateEquip(partsCategory,
            targetEquip,
            this.GetCurrentSelectedArmedParts());

        if (targetEquip.name != "None")
        {
            this.hadItem.reduceItemCount(itemID, 1);
        }

        // アイテムから装備更新したアイテムの削除と
        // 装備していたアイテムを追加
        if (equipedItem.name != "None")
        {
            this.hadItem.AddItem(new HavingItem()
            {
                id = equipedItem.id,
                name = equipedItem.name,
                category = equipedItem.category,
                count = 1, // 複数を同タイミングで着脱する予定はないため一つとする
            });
        }
    }

    public CONST.EQUIP.PARTS_CATEGORY GetCurrentSelectedArmedParts()
    {
        // 既に表示されていれば表示内容を全削除
        var items = GameObject.FindGameObjectsWithTag("PlayerMenuEquipButton");
        return items.FirstOrDefault(i => i.GetComponent<EquipMenuArnedPartsButtonClicked>().isSelected == true)
            .GetComponent<EquipMenuArnedPartsButtonClicked>().partsCategory;
    }

    public CONST.ITEM.CATEGORY ConvertEquipPartsCategoryToItemCategory(CONST.EQUIP.PARTS_CATEGORY partsCategpory)
    {
        switch (partsCategpory)
        {
            case CONST.EQUIP.PARTS_CATEGORY.WEPON:
                return CONST.ITEM.CATEGORY.WEAPON_ITEM;
            case CONST.EQUIP.PARTS_CATEGORY.HEAD:
                return CONST.ITEM.CATEGORY.HEAD_EQUIP_ITEM;
            case CONST.EQUIP.PARTS_CATEGORY.BODY:
                return CONST.ITEM.CATEGORY.BODY_EQUIP_ITEM;
            case CONST.EQUIP.PARTS_CATEGORY.ACCESSORY1:
            case CONST.EQUIP.PARTS_CATEGORY.ACCESSORY2:
                return CONST.ITEM.CATEGORY.ACCESSORY_ITEM;
            default:
                return CONST.ITEM.CATEGORY.NONE;
        }
    }

}
