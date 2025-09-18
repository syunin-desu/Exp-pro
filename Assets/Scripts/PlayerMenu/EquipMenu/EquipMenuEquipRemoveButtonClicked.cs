using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipMenuEquipRemoveButtonClicked : MonoBehaviour, IButtonClicked
{
    public EquipMenuUIManager equipMenuUIManager;
    public EquipMenuScrollManager scrollManager;
    public EquipMenuManager equipMenuManager;
    public EquipMenuDescriptionUI equipMenuDescriptionUI;
    public EquipMenuStatusUiManager equipMenuStatusUIManager;
    public ItemManager itemManager;
    public CONST.ITEM.CATEGORY partsCategory;
    public string itemID;
    public SelectedStatus selectedStatus;

    public void OnClicked()
    {

        if (!selectedStatus.GetIsSelected())
        {
            equipMenuManager.UpdateEquipSelected(this.itemID, this.partsCategory, this.gameObject);
            return;
        }

        // ‘•”õXV
        equipMenuManager.UpdateEquip(this.itemID, this.partsCategory);
    }
}
