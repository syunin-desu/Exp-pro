using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

/// <summary>
///  マスターデータ
/// </summary>
public class MasterData : SerializedMonoBehaviour
{
    private enum Ability
    {
        burn,

    }


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
    public List<ItemData> masterItemList;

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
    }

    /// <summary>
    /// マスターデータを初期化
    /// </summary>
    private void InitializeMasterData()
    {
        masterItemList = GetItemMasterDataFromAsset();
        masterAbilityList = GetAbilityMasterDataFromAsset();
        masterEnemyDataList = GetEnemyMasterDataFromAsset();
    }

    /// <summary>
    /// アセットからアイテムマスターデータを読み込む
    /// </summary>
    /// <returns></returns>
    private List<ItemData> GetItemMasterDataFromAsset()
    {
        return Resources
        .LoadAll("Data/MasterDatas/Item/", typeof(ItemData))
        .Cast<ItemData>()
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
}
