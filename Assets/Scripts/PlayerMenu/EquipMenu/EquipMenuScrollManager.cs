using CONST;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipMenuScrollManager : MonoBehaviour
{
    [SerializeField]
    RectTransform EquipContent = null;
    [SerializeField]
    RectTransform EquipRemoveContent = null;

    [SerializeField]
    private ItemManager _itemManager;

    public EquipMenuUIManager equipMenuUIManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("Equip_button").SetActive(false);
        GameObject.Find("RemoveEquip_button").SetActive(false);
    }

    public void SetupEquipListUI(PlayerEquipData playerEquipData)
    {
        //プレイヤーの装備UIを更新
        this.SetUpArmedEquip(playerEquipData);
    }

    public List<RectTransform> SetUpHadEquipList(List<HavingItem> havingEquipList)
    {
        var result = new List<RectTransform>();
        // リストの初期表示
        foreach (HavingItem equip in havingEquipList)
        {
            var equipContent = GameObject.Instantiate(EquipContent) as RectTransform;
            equipContent.SetParent(transform, false);

            var texts = equipContent.GetComponentsInChildren<TextMeshProUGUI>();
            equipContent.GetComponent<EquipMenuEquipButtonClicked>().itemID = equip.id;
            equipContent.GetComponent<EquipMenuEquipButtonClicked>().partsCategory = equip.category;

            // ボタンを非活性にする
            equipContent.GetComponentInChildren<Button>().enabled = false;
            texts.First(t => t.name == "EquipName").text = this._itemManager.getItemNameFromID(equip.id);
            texts.First(t => t.name == "Equip_Number").text = equip.count.ToString();
            var selected_text = equipContent.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
            selected_text.alpha = 0;

            equipContent.gameObject.SetActive(true);
            result.Add(equipContent);
        }
        return result;
    }

    public RectTransform SetUpRemoveButton(CONST.ITEM.CATEGORY currentSelectedParts)
    {
        var equipRemoveContent = GameObject.Instantiate(EquipRemoveContent) as RectTransform;
        equipRemoveContent.SetParent(transform, false);

        EquipBase NoneEquip = _itemManager.GetNoneEquip(currentSelectedParts);
        equipRemoveContent.GetComponent<EquipMenuEquipRemoveButtonClicked>().itemID = NoneEquip.id;
        equipRemoveContent.GetComponent<EquipMenuEquipRemoveButtonClicked>().partsCategory = NoneEquip.category;

        var selected_text = equipRemoveContent.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
        selected_text.alpha = 0;
        equipRemoveContent.gameObject.SetActive(true);

        return equipRemoveContent;
    }

    public void ClearHadEquipList(List<GameObject> equips)
    {

        foreach (var equip in equips)
        {
            Destroy(equip);
        }
    }

    public void ClearEquipItemButtonSelectedStatus(List<GameObject> items)
    {

        foreach (var item in items)
        {
            item.GetComponent<SelectedStatus>().UpdateIsSelected(false);
        }
    }

    public void SetUpArmedEquip(PlayerEquipData playerEquipData)
    {
        equipMenuUIManager.UpdateArmedEquipName(playerEquipData);
        equipMenuUIManager.ClearEquipPartsSelected();
    }
}
