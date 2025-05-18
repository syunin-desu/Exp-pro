using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuScrollManager : MonoBehaviour
{
    [SerializeField]
    RectTransform ItemContents = null;
    private RectTransform _contentArea;

    [SerializeField]
    private ItemManager _itemManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("Item_Button").SetActive(false);
    }

    public void SetupItemUI(List<HavingItem> havingItemList)
    {
        foreach (HavingItem item in havingItemList)
        {
            var itemContent = GameObject.Instantiate(ItemContents) as RectTransform;
            itemContent.SetParent(transform, false);
            itemContent.GetComponent<ItemButtonClicked>().item_id = item.id;

            var texts = itemContent.GetComponentsInChildren<TextMeshProUGUI>();

            // ボタンを非活性にする
            itemContent.GetComponentInChildren<Button>().enabled = false;
            texts.First(t => t.name == "ItemName").text = this._itemManager.getItemNameFromID(item.id);
            texts.First(t => t.name == "Item_Number").text = item.count.ToString();
            var selected_text = itemContent.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
            selected_text.alpha = 0;

            if (this._itemManager.getItemCategoryForItemID(item.id) != CONST.ITEM.CATEGORY.HEAL_ITEM)
            {
                texts.First(t => t.name == "ItemName").color = Color.gray;
                texts.First(t => t.name == "Item_Number").color = Color.gray;
                texts.First(t => t.name == "delimiter").color = Color.gray;
            }


            itemContent.gameObject.SetActive(true);
        }
    }

    public void RemoveAllItem()
    {
        var items = GameObject.FindGameObjectsWithTag("PlayerMenuItembutton");

        //表示しているボタンの削除 
        foreach (var button in items)
        {
            Destroy(button);

        }
    }

    /// <summary>
    /// アイテムボタンの選択状態をすべて解除する
    /// </summary>
    public void ClearItemButtonSelected()
    {
        // 既に表示されていれば表示内容を全削除
        var items = GameObject.FindGameObjectsWithTag("PlayerMenuItembutton");

        foreach (var item in items)
        {
            item.GetComponent<ItemStatus>().UpdateIsSelected(false);
        }
    }
}
