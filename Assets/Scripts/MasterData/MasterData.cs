using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;

/// <summary>
///  マスターデータ
/// </summary>
public class MasterData : SerializedMonoBehaviour
{
    /// <summary>
    /// マスターデータシングルトン
    /// </summary>
    public static MasterData instance;

    /// <summary>
    /// アビリティのマスターデータ
    /// </summary>
    public List<Ability_base> masterAbilityList;

    /// <summary>
    /// アイテムのマスターデータ
    /// </summary>
    public List<UsedItemData> masterItemList;

    /// <summary>
    /// 装備のマスターデータ
    /// </summary>
    public List<EquipBase> masterEquipList;

    /// <summary>
    /// 敵データリストのマスターデータ
    /// </summary>
    public List<CharData> masterEnemyDataList;

    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        this.InitializeMasterData();
    }

    /// <summary>
    /// マスターデータを初期化
    /// </summary>
    private void InitializeMasterData()
    {
        masterItemList = GetItemMasterDataFromAsset();
        masterAbilityList = GetAbilityMasterDataFromAsset();
        masterEnemyDataList = GetEnemyMasterDataFromAsset();
        masterEquipList = GetEquipMasterDataFromAsset();
    }

    /// <summary>
    /// アセットからアイテムマスターデータを読み込む
    /// </summary>
    /// <returns></returns>
    private List<UsedItemData> GetItemMasterDataFromAsset()
    {
        List<UsedItemData> masterItemList = new List<UsedItemData>();
        List<UsedItemData> itemList = Resources
        .LoadAll("Data/MasterDatas/Item/", typeof(UsedItemData))
        .Cast<UsedItemData>()
        .ToList();
        List<BaseItemData> equipList = Resources
        .LoadAll("Data/MasterDatas/Equip/", typeof(BaseItemData))
        .Cast<BaseItemData>()
        .ToList();

        masterItemList.AddRange(itemList);
        masterItemList.AddRange(this.ConvertEquipDataToUsedItemData(equipList));
        return masterItemList;
    }

    private List<EquipBase> GetEquipMasterDataFromAsset()
    {
        return Resources
        .LoadAll("Data/MasterDatas/Equip/", typeof(EquipBase))
        .Cast<EquipBase>()
        .ToList();
    }

    /// <summary>
    /// アセットからアビリティマスターデータを読み込む
    /// </summary>
    /// <returns></returns>
    private List<Ability_base> GetAbilityMasterDataFromAsset()
    {
        return Resources
        .LoadAll("Data/MasterDatas/ability/", typeof(Ability_base))
        .Cast<Ability_base>()
        .ToList();
    }

    /// <summary>
    /// アセットから敵のマスターデータを読み込む
    /// </summary>
    /// <returns></returns>
    private List<CharData> GetEnemyMasterDataFromAsset()
    {
        return Resources
        .LoadAll("Data/MasterDatas/Enemy/", typeof(CharData))
        .Cast<CharData>()
        .ToList();
    }

    private List<UsedItemData> ConvertEquipDataToUsedItemData(List<BaseItemData> equipBases)
    {
        return equipBases.Select(equip => new UsedItemData
        {
            name = equip.Name,
            displayName = equip.displayName,
            id = equip.id,
            Rarity = equip.Rarity,
            category = equip.category,
            PurchasePrice = equip.PurchasePrice,
            Sellingrice = equip.Sellingrice,
            item_description = equip.item_description,

        }).ToList();
    }
}
