using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipMenuEquipButtonClicked : MonoBehaviour
{
    public EquipMenuUIManager equipMenuUIManager;
    public EquipMenuScrollManager scrollManager;
    public EquipMenuManager equipMenuManager;
    public EquipMenuDescriptionUI equipMenuDescriptionUI;
    public ItemManager itemManager;
    public CONST.ITEM.CATEGORY partsCategory;
    public int AccessoryNumber = 0;
    public string itemID;

    public void OnClickedEquip()
    {
        // 選択されたOBJと一致していた場合はアイテム使用を実施
        string selectedItemID = this.itemID;
        bool isSelected = this.gameObject.GetComponentInChildren<EquipStatus>().GetIsSelected();

        if (!isSelected)
        {
            // 装備アイテム選択アイコンの初期化
            equipMenuUIManager.ClearEquipItemSelected();
            scrollManager.ClearEquipItemButtonSelectedStatus();

            // 選択されたアイテムを選択状態にする
            this.gameObject.GetComponentInChildren<EquipStatus>().UpdateIsSelected(true);
            TextMeshProUGUI target_obj = this.gameObject.transform.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
            equipMenuUIManager.UpdateEquipPartsSelectedIcon(target_obj, true);

            // Description を更新
            string targetDescription = itemManager.GetEquipDiscriptionfromMaster(this.itemID);
            equipMenuDescriptionUI.SetDiscription(targetDescription);
            return;
        }

        // 装備更新
        equipMenuManager.UpdateArmedEquip(this.itemID, this.partsCategory);

        // UI反映
        scrollManager.ClearHadEquipList();

        CONST.ITEM.CATEGORY currentSelectedCategory = equipMenuManager.ConvertEquipPartsCategoryToItemCategory(equipMenuManager.GetCurrentSelectedArmedParts());
        List<HavingItem> hadItem = equipMenuManager.hadItem.GetEquipItem(currentSelectedCategory);
        // 外すボタンの設定
        if (this.partsCategory != CONST.ITEM.CATEGORY.WEAPON_ITEM)
        {
            scrollManager.SetUpRemoveButton(equipMenuManager.ConvertEquipPartsCategoryToItemCategory(equipMenuManager.GetCurrentSelectedArmedParts()));
        }
        scrollManager.SetUpHadEquipList(hadItem);

        // 部位選択ボタンを有効化する
        equipMenuUIManager.UpdateEnableEquipParts(true);
        // 装備ボタンを無効化する
        equipMenuUIManager.UpdateEnableEquip(false);

    }
}
