using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    private List<createItemData> _itemList = new List<createItemData>();
    public PlayerUIManager playerUI;


    // Start is called before the first frame update
    void Start()
    {
        Object[] itemObjects = Resources.LoadAll("Data/Item/", typeof(createItemData));

        foreach (createItemData item in itemObjects)
        {
            _itemList.Add(item);
        }

    }

#nullable enable
    public void ExecItem(CharBase performChar, CharBase? targetChar, string execItemName)
    {
        switch (execItemName)
        {
            case "BluePotion":
                this.BluePotion(performChar, this.GetItemData(execItemName));
                break;
            default:
                Debug.Log("アイテムデータに登録されていないアイテムが指定されました");
                break;
        }
    }
#nullable disable

    /// <summary>
    ///  ブルーポーション
    /// </summary>
    /// <param name="performChar">対象キャラ</param>
    /// <param name="execItemData">実行するアイテムデータ</param>
    public void BluePotion(CharBase performChar, createItemData execItemData)
    {
        int healValue = execItemData.power;

        performChar.Heal(healValue);

        //UI修正
        playerUI.UpdateUI(performChar);


    }

    /// <summary>
    /// 表示アイテム名を返す
    /// </summary>
    /// <param name="itemName">アイテム名</param>
    /// <returns>表示アイテム名</returns>
    public string getItemDisplayName(string itemName)
    {
        createItemData selectedItem = this._itemList.Find(ability => ability.Name == itemName);
        return selectedItem.displayName;
    }

    /// <summary>
    /// 表示アイテム名からアイテム名を取得
    /// </summary>
    /// <param name="itemDisplayName">表示アイテム名/param>
    /// <returns>アイテム名</returns>
    public string getItemNameForDisplayName(string itemDisplayName)
    {
        createItemData item = this._itemList.Find(ability => ability.displayName == itemDisplayName);
        return item.Name;
    }

    /// <summary>
    ///  アイテムデータを取得
    /// </summary>
    /// <param name="itemName">アイテム名</param>
    /// <returns>アイテムデータ</returns>
    private createItemData GetItemData(string itemName)
    {
        return this._itemList.Find(item => item.Name == itemName);

    }
}
