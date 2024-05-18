using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<ItemData> _itemList = new List<ItemData>();

    public PlayerUIManager playerUI;

    // Start is called before the first frame update
    void Start()
    {
        _itemList = MasterData.instance.masterItemList;
    }

#nullable enable
    public async UniTask ExecItem(CharBase performChar, CharBase? targetChar, string execItemName)
    {
        switch (execItemName)
        {
            case "BluePotion":
                await this.BluePotion(performChar, this.GetItemData(execItemName));
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
    public async UniTask BluePotion(CharBase performChar, ItemData execItemData)
    {

        int healValue = execItemData.value;

        performChar.Heal(healValue);

        //アイテム数を減少させる
        performChar.reduceItemCount(execItemData.Name, 1);

        //UI修正
        await UniTask.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));

    }

    /// <summary>
    /// 表示アイテム名を返す
    /// </summary>
    /// <param name="itemName">アイテム名</param>
    /// <returns>表示アイテム名</returns>
    public string getItemDisplayName(string itemName)
    {
        ItemData selectedItem = this._itemList.Find(ability => ability.Name == itemName);
        return selectedItem.displayName;
    }

    /// <summary>
    /// 表示アイテム名からアイテム名を取得
    /// </summary>
    /// <param name="itemDisplayName">表示アイテム名/param>
    /// <returns>アイテム名</returns>
    public string getItemNameForDisplayName(string itemDisplayName)
    {
        ItemData item = this._itemList.Find(ability => ability.displayName == itemDisplayName);
        return item.Name;
    }

    /// <summary>
    ///  アイテムデータを取得
    /// </summary>
    /// <param name="itemName">アイテム名</param>
    /// <returns>アイテムデータ</returns>
    private ItemData GetItemData(string itemName)
    {
        return this._itemList.Find(item => item.Name == itemName);

    }
}
