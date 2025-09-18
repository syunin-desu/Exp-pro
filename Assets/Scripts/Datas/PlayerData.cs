using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// ゲームデータ
/// シーン遷移の際にはこのデータを最新にすること
/// ゲーム開始時に生成される
/// </summary>
public class PlayerData : SerializedMonoBehaviour
{
    /// <summary>
    /// ゲームデータシングルトン
    /// </summary>
    public static PlayerData instance;

    // パーティーデータ
    // TODO:現状はひとりとする
    public List<CharParameter> PartyMember = new List<CharParameter>();

    // 所持アイテムデータと所持数
    public Dictionary<BaseItemData, int> HaveItemList = new Dictionary<BaseItemData, int>();

    // 所持金
    public Money HasMoney = new Money();

    // Mockデータ
    // TODO: セーブデータが作成され次第削除
    public List<CharData> MockCharData;

    public Dictionary<string, int> MockItemData;

    // Start is called before the first frame update
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

    private void Start()
    {
        // Mockデータでパラメータを更新する
        // TODO: セーブデータが作成されたときには削除
        this.CreateMockData();
    }

    // マスタープレイヤーデータを更新する
    // マスターデータへの更新はシーン変更等のタイミングで実施させる
    // 基本はシーンごとにマスターデータをコピーしたオブジェクトで更新する
    public void UpdatePlayerData(CharParameter charParameter)
    {
        this.PartyMember[0] = charParameter;
    }


    /// <summary>
    /// Mockデータでパラメータを更新する
    /// </summary>
    private void CreateMockData()
    {
        PartyMember = ConvertMockToCharParameter(this.MockCharData);
        HaveItemList = this.ConvertMockItemData(this.MockItemData);
        this.HasMoney.InitializeMoney(123456789);
    }

    /// <summary>
    /// Mockデータをparamerクラスに変換する
    /// </summary>
    private List<CharParameter> ConvertMockToCharParameter(List<CharData> mockCharData)
    {
        var list = new List<CharParameter>();
        list = mockCharData
            .Select(c => CharData.Instantiate(c))
            .Select(tempMock => new CharParameter
            {
                Name = tempMock.Name,
                currentHP = tempMock.currentHP,
                currentMP = tempMock.currentMP,
                maxHp = tempMock.maxHp,
                maxMp = tempMock.maxMp,
                STR = tempMock.STR,
                DEF = tempMock.DEF,
                SPEED = tempMock.SPEED,
                MGC = tempMock.MagicPower,
                INT = tempMock.INT,
                KID = tempMock.Kindness,
                ROLE = tempMock.ROLE,
                countOfActions = tempMock.countOfActions,
                HavingAbility = tempMock.HavingAbility,
                WeakElement = tempMock.WeakElement,
                StrongElement = tempMock.StrongElement,
                equipDatas = new PlayerEquipData()
                {
                    weaponData = tempMock.weaponData,
                    armedHead = tempMock.armedHead,
                    armedBody = tempMock.armedBody,
                    armedAccessory_1 = tempMock.armedAccessory_1,
                    armedAccessory_2 = tempMock.armedAccessory_2,
                },
                charClass = tempMock.charClass,
            })
            .ToList();

        return list;
    }

    public Dictionary<BaseItemData, int> GetItems()
    {
        return this.HaveItemList;
    }

    public void UpdateLoadedData(SaveData loadData)
    {
        this.PartyMember = loadData.PartyMember;
        this.HaveItemList = loadData.HaveItemList;
        this.HasMoney.InitializeMoney(loadData.HasMoney);
    }

    public void UpdatePartyMemberParam(CharParameter charParameter)
    {
        // 現状、一人のため
        this.PartyMember[0] = charParameter;
    }

    private Dictionary<BaseItemData, int> ConvertMockItemData(Dictionary<string, int> itemMocks)
    {
        Dictionary<BaseItemData, int> res = new Dictionary<BaseItemData, int>();
        foreach (var item in itemMocks)
        {
            var targetItemData = MasterData.instance.masterItemList.FirstOrDefault(m => m.Name == item.Key);
            res.Add(targetItemData, item.Value);
        }
        return res;
    }
}