using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// ゲームデータ
/// シーン遷移の際にはこのデータを最新にすること
/// ゲーム開始時に生成される
/// </summary>
public class GameData : SerializedMonoBehaviour
{
    /// <summary>
    /// ゲームデータシングルトン
    /// </summary>
    public static GameData instance;

    // パーティーデータ
    // TODO:現状はひとりとする
    public List<CharData> PartyMember = new List<CharData>();

    // 所持アイテムデータと所持数
    public Dictionary<ItemData, int> HaveItemList = new Dictionary<ItemData, int>();

    // 所持金
    public Money HasMoney = new Money();

    // Mockデータ
    // TODO: セーブデータが作成され次第削除
    public List<CharData> MockCharData;

    public Dictionary<ItemData, int> MockItemData;

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


    /// <summary>
    /// Mockデータでパラメータを更新する
    /// </summary>
    private void CreateMockData()
    {
        PartyMember = this.MockCharData;
        HaveItemList = MockItemData;
        this.HasMoney.InitializeMoney(99999);
    }
}