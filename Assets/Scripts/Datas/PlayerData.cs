using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public Dictionary<BaseItemData, int> MockItemData;

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
        HaveItemList = MockItemData ?? new Dictionary<BaseItemData, int>();
        this.HasMoney.InitializeMoney(123456789);
    }

    /// <summary>
    /// Mockデータをparamerクラスに変換する
    /// </summary>
    private List<CharParameter> ConvertMockToCharParameter(List<CharData> mockCharData)
    {
        return mockCharData
            .Select(charData => new CharParameter
            {
                Name = charData.Name,
                currentHP = charData.currentHP,
                currentMP = charData.currentMP,
                maxHp = charData.maxHp,
                maxMp = charData.maxMp,
                STR = charData.STR,
                DEF = charData.DEF,
                SPEED = charData.SPEED,
                MGC = charData.MagicPower,
                INT = charData.INT,
                KID = charData.Kindness,
                ROLE = charData.ROLE,
                countOfActions = charData.countOfActions,
                HavingAbility = charData.HavingAbility,
                WeakElement = charData.WeakElement,
                StrongElement = charData.StrongElement,
                equipDatas = new PlayerEquipData()
                {
                    weaponData = charData.weaponData,
                    armedHead = charData.armedHead,
                    armedBody = charData.armedBody,
                    armedAccessory_1 = charData.armedAccessory_1,
                    armedAccessory_2 = charData.armedAccessory_2,
                },

            }).ToList();
    }

    public Dictionary<BaseItemData, int> GetItems()
    {
        return this.HaveItemList;
    }
}