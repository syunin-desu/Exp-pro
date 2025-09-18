using CONST;
using NUnit.Framework.Interfaces;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// </summary>
public class QuestData : SerializedMonoBehaviour
{

    /// <summary>
    /// インスタンス
    /// </summary>
    public static QuestData instance;


    // 現在のフロア
    public int currentFloor;

    // フロア名
    public string currentFloorName;

    // 現在のフロアのカード状況
    public List<BaseCardProperty> currentCardList;

    // 選択可能なカード枚数
    public int canSelectCardNumber = 3;

    // Questシーンに遷移した際のカード配置アニメーション可否
    public bool animateCardInitialize;

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
        //// mock実装のため後々削除
        //currentCardList = new List<BaseCardProperty>()
        //{
        //    // Mock実装 Excel等で階層ごとのカードリストを設定しておき、
        //    // 初めにランダムで並び変える
        //    new BaseCardProperty()
        //    {
        //        cartType = CONST.QUEST.CardType.GetItem,
        //        item = new List<BaseItemData>(){MasterData.instance.masterItemList.FirstOrDefault(e => e.Name == "BluePotion") },
        //    },
        //    new BaseCardProperty()
        //    {
        //        cartType = CONST.QUEST.CardType.EncountEnemy,
        //        enemyData = MasterData.instance.masterEnemyDataList.FirstOrDefault(e => e.Name == "test"),
        //    },
        //    new BaseCardProperty()
        //    {
        //        cartType = CONST.QUEST.CardType.NextFloor,
        //    },
        //    new BaseCardProperty()
        //    {
        //        cartType = CONST.QUEST.CardType.EncountEnemy,
        //        enemyData = MasterData.instance.masterEnemyDataList.FirstOrDefault(e => e.Name == "test"),
        //    },

        //};
    }

    private void Start()
    {
        animateCardInitialize = true;

        this.InitializeQuestStatus();
    }

    private void InitializeQuestStatus()
    {
        this.currentFloor = 1;
        this.currentCardList = MasterData.instance.GetCardDatasForBaseCard(this.currentFloor);
    }

    public void UpdataLoadedData(SaveData loadData)
    {
        this.currentFloor = loadData.currentFloor;
        this.currentFloorName = loadData.currentFloorName;
        this.currentCardList = loadData.currentCardList;
        this.canSelectCardNumber = loadData.canSelectCardNumber;
    }
}
