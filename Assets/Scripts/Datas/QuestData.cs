using CONST;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
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

    // 現在のフロアのカード状況
    public List<CONST.QUEST.CardType> currentCardList;

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

        animateCardInitialize = true;
        // mock実装のため後々削除
        currentCardList = new List<CONST.QUEST.CardType>()
        {
            // Mock実装 Excel等で階層ごとのカードリストを設定しておき、
            // 初めにランダムで並び変える
            CONST.QUEST.CardType.EncountEnemy,
            CONST.QUEST.CardType.EncountEnemy,
            CONST.QUEST.CardType.EncountEnemy,
            CONST.QUEST.CardType.EncountEnemy,
            CONST.QUEST.CardType.EncountEnemy,
            CONST.QUEST.CardType.EncountEnemy,
            CONST.QUEST.CardType.EncountEnemy,
            CONST.QUEST.CardType.EncountEnemy,
            CONST.QUEST.CardType.EncountEnemy,
            CONST.QUEST.CardType.EncountEnemy,

        };
    }
}
