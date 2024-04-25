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
        this.SetParameter(ConvertCharDataToParameter(enemyData));
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

    /// <summary>
    /// マスタデータを内部パラメータクラスに変換する
    /// </summary>
    /// <param name="charData"></param>
    /// <returns></returns>
    private CharParameter ConvertCharDataToParameter(CharData charData)
    {
        return new CharParameter()
        {
            Name = charData.Name,
            currentHP = charData.currentHP,
            currentMP = charData.currentMP,
            maxHp = charData.maxHp,
            maxMp = charData.maxMp,
            STR = charData.STR,
            DEF = charData.DEF,
            SPEED = charData.SPEED,
            INT = charData.INT,
            ROLE = charData.ROLE,
            countOfActions = charData.countOfActions,
            WeakElement = charData.WeakElement,
            StrongElement = charData.StrongElement,
            HavingAbility = charData.HavingAbility,

        };
    }
}
