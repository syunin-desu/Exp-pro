using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class ItemButtonClicked : MonoBehaviour, IButtonClicked
{
    public ItemManager _itemManager;
    public ItemMenuManager itemMenuManager;
    public ItemMenuUIManager itemMenuUIManager;
    public ItemMenuScrollManager itemMenuScrollManager;
    public PartyMember _playerManager;
    public HadItem haditem;
    public string item_id;

    /// <summary>
    /// 表示されているアイテムがクリックした時の処理
    /// </summary>
    /// <param name="charactor">キャラ</param>
    public void OnClicked()
    {
        // 選択されたOBJと一致していた場合はアイテム使用を実施
        string selectedItemID = this.gameObject.GetComponent<ItemButtonClicked>().item_id;

        // managerクラスに選択中オブジェクトとして更新
        if (!this.gameObject.GetComponentInChildren<SelectedStatus>().GetIsSelected())
        {
            this.itemMenuManager.UpdateItemSelected(this.gameObject);

            // Description を更新
            itemMenuManager.UpdateDiscription(_itemManager.GetItemDiscriptionfromMaster(selectedItemID) ?? "");
            return;
        }

        if (_itemManager.getItemCategoryForItemID(selectedItemID) != CONST.ITEM.CATEGORY.HEAL_ITEM
            && this.gameObject.GetComponentInChildren<SelectedStatus>().GetIsSelected())
        {
            // Description を更新
            itemMenuManager.UpdateDiscription(_itemManager.GetItemDiscriptionfromMaster(selectedItemID) ?? "");
            Debug.Log(selectedItemID.ToString() + "は使用できないアイテムカテゴリです");
            return;
        }

        // アイテムの効果を実施
        itemMenuManager.ExecuteItem(_playerManager, selectedItemID);

        // アイテムの個数がなくなった場合はremove
        var updatedTargetItemCount = haditem.GetItemCount(selectedItemID);
        if (updatedTargetItemCount is null)
        {
            Destroy(this.gameObject);
        }

        // アイテム個数のUIを更新
        var texts = this.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        texts.First(t => t.name == "Item_Number").text = updatedTargetItemCount.ToString();


    }
}
