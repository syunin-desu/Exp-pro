using CONST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<UsedItemData> _masteritemList = new List<UsedItemData>();
    private List<EquipBase> _masterequipList = new List<EquipBase>();
    public HadItem haditem;

    // Start is called before the first frame update
    void Start()
    {
        _masteritemList = MasterData.instance.masterItemList;
        _masterequipList = MasterData.instance.masterEquipList;
    }

#nullable enable
    public async Task ExecItem(CharBase performChar, CharBase? targetChar, string execItemID, bool canEffect = true)
    {
        var itemData = this.GetItemData(execItemID);
        switch (itemData.Type)
        {
            case CONST.ACTION.TYPE.Heal:
                await this.DoHealItem(performChar, this.GetItemData(execItemID), canEffect);
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
    public async Task DoHealItem(CharBase performChar, UsedItemData execItemData, bool canEffect)
    {

        int healValue = execItemData.value;

        if (execItemData.Target_status == CONST.ACTION.TARGET_STATUS.HP)
        {
            if (performChar.GetHp() == performChar.GetMaxHp())
            {
                //TODO: 使用できませんでした的なSEを鳴らす
                return;
            }
            performChar.HealHP(healValue);

        }
        else if (execItemData.Target_status == CONST.ACTION.TARGET_STATUS.MP)
        {
            if (performChar.GetMp() == performChar.GetMaxMp())
            {
                //TODO: 使用できませんでした的なSEを鳴らす
                return;
            }
            performChar.HealMP(healValue);
        }


        //アイテム数を減少させる
        haditem.reduceItemCount(execItemData.id, 1);

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
        UsedItemData selectedItem = this._masteritemList.Find(item => item.Name == itemName);
        return selectedItem.displayName;
    }

    public string getItemNameFromID(string itemID)
    {
        UsedItemData selectedItem = this._masteritemList.Find(item => item.id == itemID);
        return selectedItem.displayName;
    }

    public List<string> getHadEquipListFromID()
    {
        return this._masterequipList
            .FindAll(item => item.category == CONST.ITEM.CATEGORY.WEAPON_ITEM)
            .Select(item => item.id)
            .ToList();
    }

    /// <summary>
    /// 表示アイテム名からアイテム名を取得
    /// </summary>
    /// <param name="itemDisplayName">表示アイテム名/param>
    /// <returns>アイテム名</returns>
    public string getItemNameForDisplayName(string itemDisplayName)
    {
        UsedItemData item = this._masteritemList.Find(item => item.displayName == itemDisplayName);
        return item.Name;
    }

    /// <summary>
    /// アイテム名から実行優先度を取得
    /// </summary>
    /// <param name="itemDisplayName">表示アイテム名/param>
    /// <returns>アイテム名</returns>
    public int getItemSpeedRankForItemID(string itemName)
    {
        UsedItemData item = this._masteritemList.Find(item => item.id == itemName);
        return item.speed_rank;
    }

    public CONST.ITEM.CATEGORY getItemCategoryForItemID(string itemid)
    {
        UsedItemData item = this._masteritemList.Find(item => item.id == itemid);
        return item.category;
    }

    public EquipBase GetEquipDataFromMaster(string equipID)
    {
        return this._masterequipList.FirstOrDefault(e => e.id == equipID);
    }

    public EquipBase GetNoneEquip(CONST.ITEM.CATEGORY targetCategory)
    {
        return this._masterequipList.FindAll(e => e.category == targetCategory).FirstOrDefault(e => e.name == "None");
    }

    /// <summary>
    ///  アイテムデータを取得
    /// </summary>
    /// <param name="itemName">アイテム名</param>
    /// <returns>アイテムデータ</returns>
    private UsedItemData GetItemData(string itemID)
    {
        return this._masteritemList.Find(item => item.id == itemID);

    }

    public string GetItemDiscriptionfromMaster(string itemID)
    {
        return this._masteritemList.Find(item => item.id == itemID).item_description;
    }

    public string GetEquipDiscriptionfromMaster(string itemID)
    {
        return this._masterequipList.Find(item => item.id == itemID).description_equip;
    }
}
