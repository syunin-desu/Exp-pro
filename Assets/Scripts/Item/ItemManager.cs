using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<ItemData> _itemList = new List<ItemData>();

    // Start is called before the first frame update
    void Start()
    {
        _itemList = MasterData.instance.masterItemList;
    }

#nullable enable
    public async Task ExecItem(CharBase performChar, CharBase? targetChar, string execItemName, bool canEffect = true)
    {
        switch (execItemName)
        {
            case "BluePotion":
            case "BluePotionEx":
            case "BluePotionNeo":
            case "EnagyDrink":
            case "EnagyDrinkEx":
            case "EnagyDrinkNeo":
                await this.DoHealItem(performChar, this.GetItemData(execItemName), canEffect);
                break;

            default:
                Debug.Log("アイテムデータに登録されていないアイテムが指定されました");
                break;
        }
    }
#nullable disable

    /// <summary>
    ///  回復アイテムを使用
    /// </summary>
    /// <param name="performChar">対象キャラ</param>
    /// <param name="execItemData">実行するアイテムデータ</param>
    public async Task DoHealItem(CharBase performChar, ItemData execItemData, bool canEffect)
    {

        int healValue = execItemData.value;

        if (execItemData.Target_status == CONST.ACTION.TARGET_STATUS.HP)
        {
            performChar.HealHP(healValue);
        }
        else if (execItemData.Target_status == CONST.ACTION.TARGET_STATUS.MP)
        {
            performChar.HealMP(healValue);
        }

        //アイテム数を減少させる
        performChar.reduceItemCount(execItemData.Name, 1);

        if (canEffect)
        {
            await Task.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));
        }

    }

    /// <summary>
    /// 表示アイテム名を返す
    /// </summary>
    /// <param name="itemName">アイテム名</param>
    /// <returns>表示アイテム名</returns>
    public string getItemDisplayName(string itemName)
    {
        ItemData selectedItem = this._itemList.Find(item => item.Name == itemName);
        return selectedItem.displayName;
    }

    /// <summary>
    /// 表示アイテム名からアイテム名を取得
    /// </summary>
    /// <param name="itemDisplayName">表示アイテム名/param>
    /// <returns>アイテム名</returns>
    public string getItemNameForDisplayName(string itemDisplayName)
    {
        ItemData item = this._itemList.Find(item => item.displayName == itemDisplayName);
        return item.Name;
    }

    /// <summary>
    /// アイテム名から実行優先度を取得
    /// </summary>
    /// <param name="itemDisplayName">表示アイテム名/param>
    /// <returns>アイテム名</returns>
    public int getItemSpeedRankForItemName(string itemName)
    {
        ItemData item = this._itemList.Find(item => item.name == itemName);
        return item.speed_rank;
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
