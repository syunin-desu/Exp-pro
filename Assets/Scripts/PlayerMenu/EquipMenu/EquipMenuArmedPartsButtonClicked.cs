using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipMenuArmedPartsButtonClicked : MonoBehaviour, IButtonClicked
{
    public EquipMenuUIManager equipMenuUIManager;
    public CONST.EQUIP.PARTS_CATEGORY partsCategory;
    public EquipMenuManager equipMenuManager;
    public EquipMenuScrollManager scrollManager;
    public TextMeshProUGUI isSelectedIcon;
    public SelectedStatus selectedStatus;

    public void OnClicked()
    {
        GameObject selectedObj = this.gameObject;
        // 非選択状態でクリックされた場合
        if (!selectedStatus.GetIsSelected())
        {
            equipMenuManager.OnClickeArmedParts(selectedObj, this.partsCategory);
            return;
        }

        equipMenuManager.OnArmedPartsSelected();
    }
}
