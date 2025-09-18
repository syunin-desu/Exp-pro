using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipMenuEquipButtonClicked : MonoBehaviour, IButtonClicked
{
    public EquipMenuUIManager equipMenuUIManager;
    public EquipMenuScrollManager scrollManager;
    public EquipMenuManager equipMenuManager;
    public EquipMenuDescriptionUI equipMenuDescriptionUI;
    public EquipMenuStatusUiManager equipMenuStatusUIManager;
    public ItemManager itemManager;
    public CONST.ITEM.CATEGORY partsCategory;
    public int AccessoryNumber = 0;
    public string itemID;
    public SelectedStatus equipStatus;

    public void OnClicked()
    {
        // 選択されたOBJと一致していた場合はアイテム使用を実施

        if (!equipStatus.GetIsSelected())
        {
            equipMenuManager.UpdateEquipSelected(this.itemID, this.partsCategory, this.gameObject);
            return;
        }

        equipMenuManager.UpdateEquip(this.itemID, this.partsCategory);
    }
}
