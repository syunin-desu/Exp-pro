using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// battleデータ
/// 出現する敵情報を保管し、battleシーン表示時に呼び出す
/// </summary>
public class BattleData : SerializedMonoBehaviour
{
    /// <summary>
    /// インスタンス
    /// </summary>
    public static BattleData instance;

    // 表示する敵リスト
    [SerializeField]
    private CharData targetEnemy;

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

    public void UpdateBattleTargetEnemy(CharData targetEnemys)
    {
        this.targetEnemy = targetEnemys;
    }

    public CharData GetTargetEnemy()
    {
        return this.targetEnemy;
    }
}
