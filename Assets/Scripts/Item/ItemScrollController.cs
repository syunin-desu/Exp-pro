using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemScrollController : MonoBehaviour
{

    [SerializeField]
    RectTransform ItemContents = null;
    private RectTransform _contentArea;

    [SerializeField]
    private ItemManager _itemManager;

    public BattleActionList battleActionList;

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
        foreach (HavingItem item in havingItemList)
        {
            var itemContent = GameObject.Instantiate(ItemContents) as RectTransform;
            itemContent.SetParent(transform, false);

            var texts = itemContent.GetComponentsInChildren<Text>();

            texts.First(t => t.name == "Text").text = this._itemManager.getItemDisplayName(item.ItemName);

            itemContent.gameObject.SetActive(true);

            // アクションリストに該当アイテムが登録されていたらその個数分差し引く
            int lefttemCount = item.ItemCount - battleActionList.GetP_ActionList()
                                                .FindAll(a => a.itemName == item.ItemName)
                                                .Count();

            if (lefttemCount <= 0)
            {
                // ボタンを非活性にする
                itemContent.GetComponentInChildren<Button>().enabled = false;
                texts.First(t => t.name == "Text").color = Color.gray;
                texts.First(t => t.name == "Signal").color = Color.gray;
                texts.First(t => t.name == "Number").color = Color.gray;
                lefttemCount = 0;
            }

            texts.First(t => t.name == "Number").text = lefttemCount.ToString();
        }
    }
}
