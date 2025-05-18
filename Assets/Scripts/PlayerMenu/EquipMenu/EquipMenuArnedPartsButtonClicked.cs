using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipMenuArnedPartsButtonClicked : MonoBehaviour
{
    // eventSystemを取得するための変数宣言
    [SerializeField] EventSystem eventSystem;
    public EquipMenuUIManager equipMenuUIManager;
    public CONST.EQUIP.PARTS_CATEGORY partsCategory;
    public EquipMenuManager equipMenuManager;
    public EquipMenuScrollManager scrollManager;
    public TextMeshProUGUI isSelectedIcon;
    public bool isSelected;

    public void OnClickArmedParts()
    {
        GameObject selectedObj = eventSystem.currentSelectedGameObject.gameObject;
        // 非選択状態でクリックされた場合
        if (!isSelected)
        {
            // 全部位の選択状態をクリアし、クリックされたOBJのみ選択状態にする
            equipMenuManager.ClearEquipPartsButtonSelected();
            equipMenuUIManager.ClearEquipPartsSelected();
            scrollManager.ClearHadEquipList();
            this.isSelected = true;
            equipMenuUIManager.UpdateEquipPartsSelectedIcon(isSelectedIcon, true);

            CONST.ITEM.CATEGORY itemCategory = equipMenuManager.ConvertEquipPartsCategoryToItemCategory(partsCategory);
            // 外すボタンの設定
            if (this.partsCategory != CONST.EQUIP.PARTS_CATEGORY.WEPON)
            {
                scrollManager.SetUpRemoveButton(itemCategory);
            }
            // 装備リストを更新
            List<HavingItem> hadItem = equipMenuManager.hadItem.GetEquipItem(itemCategory);
            scrollManager.SetUpHadEquipList(hadItem);
            return;
        }

        // 部位選択ボタンを無効化する
        equipMenuUIManager.UpdateEnableEquipParts(false);
        // 装備ボタンを有効化する
        equipMenuUIManager.UpdateEnableEquip(true);

    }
}
