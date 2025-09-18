using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;
using Sirenix.Utilities;


public class FloorCardMaster
{
    public List<CardProperty> cards;

    public int floorNumber;
}

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

    /// <summary>
    /// フロアカードマスターデータ
    /// </summary>
    public List<FloorCardMaster> masterFloorCard;

    /// <summary>
    /// 全バフリスト
    /// </summary>
    public List<BuffData> masterBuffDataList;

    public List<Class_Base> masterClassDataList;

    public List<DoWhimRateForAbilityLevel> masterDoWhimRateList;

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
        masterItemList = this.GetItemMasterDataFromAsset();
        masterAbilityList = this.GetAbilityMasterDataFromAsset();
        masterEnemyDataList = this.GetEnemyMasterDataFromAsset();
        masterEquipList = this.GetEquipMasterDataFromAsset();
        masterFloorCard = this.GetCardPropertyFromAsset();
        masterBuffDataList = this.GetBuffMasterDataFromAsset();
        masterClassDataList = this.GetClassMasterDataFromAsset();
        masterDoWhimRateList = this.GetDoWhimRateFromAsset();

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
        .Select(i => UsedItemData.Instantiate(i))
        .ToList();
        List<BaseItemData> equipList = Resources
        .LoadAll("Data/MasterDatas/Equip/", typeof(BaseItemData))
        .Select(i => BaseItemData.Instantiate(i))
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
        .Select(i => EquipBase.Instantiate(i))
        .Cast<EquipBase>()
        .ToList();
    }

    private List<BuffData> GetBuffMasterDataFromAsset()
    {
        return Resources
        .LoadAll("Data/MasterDatas/Buff/", typeof(BuffData))
        .Select(i => BuffData.Instantiate(i))
        .Cast<BuffData>()
        .ToList();
    }

    private List<FloorCardMaster> GetCardPropertyFromAsset()
    {
        List<FloorCardMaster> masterItemList = new List<FloorCardMaster>();
        int assetFloorNumber = 1;

        for (int i = 1; i <= assetFloorNumber; i++)
        {
            List<CardProperty> cards = Resources
            .LoadAll($"Data/MasterDatas/QuestCards/floor_{i.ToString()}")
            .Select(i => CardProperty.Instantiate(i))
            .Cast<CardProperty>()
            .ToList();

            masterItemList.Add(
                new FloorCardMaster()
                {
                    cards = cards,
                    floorNumber = i,
                }
            );
        }

        return masterItemList;
    }

    /// <summary>
    /// アセットからアビリティマスターデータを読み込む
    /// </summary>
    /// <returns></returns>
    private List<Ability_base> GetAbilityMasterDataFromAsset()
    {
        return Resources
        .LoadAll("Data/MasterDatas/ability/", typeof(Ability_base))
        .Select(i => Ability_base.Instantiate(i))
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
        .Select(i => CharData.Instantiate(i))
        .Cast<CharData>()
        .ToList();
    }

    private List<Class_Base> GetClassMasterDataFromAsset()
    {
        return Resources
        .LoadAll("Data/MasterDatas/BattleClass/", typeof(Class_Base))
        .Select(i => Class_Base.Instantiate(i))
        .Cast<Class_Base>()
        .ToList();
    }

    private List<DoWhimRateForAbilityLevel> GetDoWhimRateFromAsset()
    {
        return Resources
        .LoadAll("Data/MasterDatas/WhimAbilityRate/", typeof(DoWhimRateForAbilityLevel))
        .Select(i => DoWhimRateForAbilityLevel.Instantiate(i))
        .Cast<DoWhimRateForAbilityLevel>()
        .ToList();
    }

    private List<UsedItemData> ConvertEquipDataToUsedItemData(List<BaseItemData> equipBases)
    {
        List<UsedItemData> resultList = new List<UsedItemData>();

        foreach (var equip in equipBases)
        {
            UsedItemData itemData = ScriptableObject.CreateInstance("UsedItemData") as UsedItemData;
            itemData.Name = equip.Name;
            itemData.displayName = equip.displayName;
            itemData.id = equip.id;
            itemData.Rarity = equip.Rarity;
            itemData.category = equip.category;
            itemData.PurchasePrice = equip.PurchasePrice;
            itemData.Sellingrice = equip.Sellingrice;
            itemData.item_description = equip.item_description;

            resultList.Add(itemData);

        }

        return resultList;
    }

    public List<BaseCardProperty> GetCardDatasForBaseCard(int targetFloor, int targetParallelNumber = 1)
    {
        return this.masterFloorCard
            .FirstOrDefault(c => c.floorNumber == targetFloor).cards
            .FindAll(c => c.parallelNumber == targetParallelNumber)
            .Select(c => new BaseCardProperty()
            {
                cartType = c.cartType,
                creditValue = c.creditValue,
                item = this.CovertItemNamesToListItemList(c.itemName),
                enemyData = this.masterEnemyDataList.FirstOrDefault(e => e.Name == c.enemyDataName),

            })
            .ToList();

    }

    public List<BaseItemData> CovertItemNamesToListItemList(List<string> names)
    {
        var targetItemDatas = new List<BaseItemData>();
        foreach (var item in names)
        {
            targetItemDatas.Add(this.masterItemList.FirstOrDefault(i => i.Name == item));
        }
        return targetItemDatas;
    }
}
