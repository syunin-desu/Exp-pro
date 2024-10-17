using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
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
}
