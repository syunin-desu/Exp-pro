using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クエスト関係のデータ
/// クエストシーン時に生成される
/// </summary>
public class QuestData : SerializedMonoBehaviour
{

    /// <summary>
    /// インスタンス
    /// </summary>
    public static QuestData instance;

    // イベントタイルリスト

    // 現在の階層
    public int currentFloor;

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
