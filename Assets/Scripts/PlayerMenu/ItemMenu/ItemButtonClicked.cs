using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class ItemButtonClicked : MonoBehaviour
{
    // eventSystemを取得するための変数宣言
    [SerializeField] EventSystem eventSystem;
    public ItemManager _itemManager;
    public ItemMenuManager itemMenuManager;
    public ItemMenuUIManager itemMenuUIManager;
    public ItemMenuScrollManager itemMenuScrollManager;
    public PartyMember _playerManager;
    public ItemStatus _itemStatus;
    public HadItem haditem;
    public string item_id;

    /// <summary>
    /// 表示されているアイテムがクリックした時の処理
    /// </summary>
    /// <param name="charactor">キャラ</param>
    public void OnClickItem()
    {
        GameObject selectedObj = eventSystem.currentSelectedGameObject.gameObject;

        // 選択されたOBJと一致していた場合はアイテム使用を実施
        string selectedItemID = selectedObj.GetComponent<ItemButtonClicked>().item_id;

        // managerクラスに選択中オブジェクトとして更新
        if (selectedObj.GetComponentInChildren<ItemStatus>().GetIsSelected() == false)
        {
            //全アイテムの選択中ステータスをFalseにする
            itemMenuScrollManager.ClearItemButtonSelected();
            itemMenuUIManager.AllSelectedIconDisable();

            // 選択されたアイテムを選択状態にする
            selectedObj.GetComponentInChildren<ItemStatus>().UpdateIsSelected(true);
            TextMeshProUGUI target_obj = selectedObj.transform.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
            itemMenuUIManager.UpdateItemSelectedIcon(target_obj, true);

            // Description を更新
            var targetItemID = selectedObj.GetComponent<ItemButtonClicked>().item_id;
            itemMenuManager.UpdateDiscription(_itemManager.GetItemDiscriptionfromMaster(selectedItemID) ?? "");
            return;
        }

        // アイテムの効果を実施
        itemMenuManager.ExecuteItem(_playerManager, selectedItemID);

        // アイテムの個数がなくなった場合はremove
        var updatedTargetItemCount = haditem.GetItemCount(selectedItemID);
        if (updatedTargetItemCount is null)
        {
            Destroy(selectedObj);
        }

        // アイテム個数のUIを更新
        var texts = selectedObj.GetComponentsInChildren<TextMeshProUGUI>();
        texts.First(t => t.name == "Item_Number").text = updatedTargetItemCount.ToString();


    }
}
