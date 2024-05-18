using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//敵を管理するもの(ステータス/クリック検出)
//char_Baseから継承
public class EnemyManager : CharBase
{

    /// <summary>
    /// 敵のマスターデータ
    /// </summary>
    public CharData enemyData;

    /// <summary>
    /// 敵UI
    /// </summary>
    public EnemyUIManager enemyUI;

    private void Awake()
    {
        this.SetParameter(ConvertCharDataToParameter(enemyData));
        // UI情報をセット
        enemyUI = GameObject.Find("EnemyUI").GetComponent<EnemyUIManager>();
        enemyUI.isShowEnemyUI(false);
    }

    void Start()
    {
        this.char_role = CONST.CHARCTOR.ENEMY;

        CharParameter charParameter = this.GetCharParameters();

        // HPバーの初期化
        enemyUI.isShowEnemyUI(true);
        this.UpdateHpBar(charParameter);

        // 敵名の設定
        enemyUI.enemyText.text = charParameter.Name;

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

    public override void Damage(int healValue)
    {
        base.Damage(healValue);

        CharParameter charParameter = this.GetCharParameters();

        // HPバーの初期化
        this.UpdateHpBar(charParameter);
    }


    public override void Heal(int healValue)
    {
        base.Heal(healValue);

        CharParameter charParameter = this.GetCharParameters();

        // HPバーの初期化
        this.UpdateHpBar(charParameter);
    }

    private void UpdateHpBar(CharParameter charParameter)
    {
        float currentHpRate = (float)charParameter.currentHP / (float)charParameter.maxHp;

        // HPバーの初期化
        enemyUI.charHPBarUIManager.SetGauge(currentHpRate);
    }
}
