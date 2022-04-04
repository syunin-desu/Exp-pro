using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScrollController : MonoBehaviour
{

    [SerializeField]
    RectTransform ItemContents = null;
    private RectTransform _contentArea;

    [SerializeField]
    private ItemManager _itemManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Item_button").SetActive(false);

    }

    /// <summary>
    /// アイテムUIをセットアップ
    /// </summary>
    /// <param name="havingItemList">所持アイテムリスト</param>
    public void SetupItemUI(List<HavingItem> havingItemList)
    {
        var content = GameObject.Find("Item_UI_Content");
        foreach (HavingItem item in havingItemList)
        {
            var itemContent = GameObject.Instantiate(ItemContents) as RectTransform;
            itemContent.SetParent(transform, false);

            var text = itemContent.GetComponentInChildren<Text>();

            text.text = this._itemManager.getItemDisplayName(item.ItemName);

            itemContent.gameObject.SetActive(true);
            var count = GameObject.Find("Number").GetComponent<Text>();
            count.text = item.ItemCount.ToString();
        }
    }
}
