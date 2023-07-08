using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//敵を管理するもの(ステータス/クリック検出)
//char_Baseから継承
public class EnemyManager : CharBase
{
    // TODO UIManagerもプロパティに含めないか検討
    // Attackメソッドをオーバーライドし、その中でUIの値の更新も行う
    Action tapAction; //クリックされた時に実行したい関数

    /// <summary>
    /// 敵のマスターデータ
    /// </summary>
    public CharData enemyData;

    private void Awake()
    {
        this.SetParameter(enemyData);
    }

    void Start()
    {
        this.char_role = CONST.CHARCTOR.ENEMY;
    }
    public void AddEventListenerOnTap(Action action)
    {
        tapAction += action;
    }

    public void OnTap()
    {
        Debug.Log("クリックされた");
        tapAction();
    }
}
