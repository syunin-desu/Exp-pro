using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using NUnit.Framework.Internal;


//敵や、プレイヤーなどのキャラの派生元
public class CharBase : MonoBehaviour
{

    // キャラのパラメーター
    private CharParameter charParameters;

    //防御フラグ
    private bool action_defense = false;
    //キャラロール
    public int char_role;
    // 1ターン中の行動回数
    // 行動回数の増減はパラメータを直接いじらず、この変数を返して実施すること
    public int countActionATurn;


    //ターン終了時の処理
    public void resetTurnEnd_char_parameter()
    {
        this.reset_defense_flag();
    }

    //攻撃する
    public virtual void Attack(CharBase character)
    {

        character.Damage(this.charParameters.STR);

    }

    //ダメージを受ける
    public virtual void Damage(int damage)
    {
        //ダメージ計算
        // TODO ダメージ計算式を見直し
        //防御アクションによるダメージ減少率を設定
        float defenceRate = this.action_defense ? CONST.BATTLE_RATE.RATE_DEFENCE : CONST.BATTLE_RATE.RATE_DEFAULT_DEFENCE;
        int result_damage = (int)Math.Ceiling(damage / defenceRate);
        this.charParameters.currentHP -= result_damage;

        Debug.Log($"Damage ={result_damage}");

        if (this.charParameters.currentHP <= 0)
        {
            this.charParameters.currentHP = 0;
        }
    }

    /// <summary>
    /// HPを回復する
    /// </summary>
    /// <param name="healValue">回復量</param>
    public virtual void HealHP(int healValue)
    {

        Debug.Log("beforeHp:" + this.charParameters.currentHP);
        this.charParameters.currentHP += healValue;
        if (this.charParameters.currentHP > this.charParameters.maxHp)
        {
            this.charParameters.currentHP = this.charParameters.maxHp;
        }
        Debug.Log("afterHp:" + this.charParameters.currentHP);

    }

    /// <summary>
    /// MPを回復する
    /// </summary>
    /// <param name="healValue">回復量</param>
    public virtual void HealMP(int healValue)
    {

        Debug.Log("beforeMp:" + this.charParameters.currentMP);
        this.charParameters.currentMP += healValue;
        if (this.charParameters.currentMP > this.charParameters.maxMp)
        {
            this.charParameters.currentMP = this.charParameters.maxMp;
        }
        Debug.Log("afterMp:" + this.charParameters.currentMP);

    }

    /// <summary>
    /// MPが消費できれば消費、できなければfalseを返す
    /// </summary>
    /// <param name="ConsumeValue"></param>
    public bool ConsumeMP(int ConsumeValue)
    {
        Debug.Log("beforeMp:" + this.charParameters.currentMP);
        if (this.charParameters.currentMP > ConsumeValue)
        {
            this.charParameters.currentMP -= ConsumeValue;
            Debug.Log("AfterMp:" + this.charParameters.currentMP);
            return true;
        }
        else
        {
            // MPが足りていなかったためfalseを返す
            return false;
        }
    }

    //バフのリセット
    public void resetBuff()
    {
        //防御をやめる
        this.reset_defense_flag();
    }

    // 防御する
    public void Defense()
    {
        this.action_defense = true;
    }

    //防御フラグをリセット
    private void reset_defense_flag()
    {
        this.action_defense = false;

    }

    //防御フラグを取得
    public bool get_actionDefense()
    {
        return this.action_defense;
    }

    //パラメータセット
    public void SetParameter(CharParameter charParameter)
    {
        this.charParameters = charParameter;

        countActionATurn = this.charParameters.countOfActions;

    }

    //=============
    // getter,setter
    //=============

    // charParameter
    public CharParameter GetCharParameters()
    {
        return this.charParameters;
    }

    //name
    public string GetName()
    {
        return this.charParameters.Name;
    }

    //hp
    public int GetHp()
    {
        return this.charParameters.currentHP;
    }

    public int GetMaxHp()
    {
        return this.charParameters.maxHp;
    }

    public int GetMp()
    {
        return this.charParameters.currentMP;
    }

    public int GetMaxMp()
    {
        return this.charParameters.maxMp;
    }

    //speed
    public int GetSpeed()
    {
        return this.charParameters.SPEED;
    }

    public int GetMagicPoser()
    {
        return this.charParameters.MGC;
    }

    // INT
    public int GetInteli()
    {
        return this.charParameters.INT;
    }

    public int GetKindness()
    {
        return this.charParameters.KID;
    }

    //strange
    public int GetStrange()
    {
        return this.charParameters.STR;
    }

    public int GetDefence()
    {
        return this.charParameters.DEF;
    }

    //所持アビリティ
    public List<Ability_base> GetHavingAbilities()
    {
        return this.charParameters.HavingAbility;
    }

    //特定のカテゴリの所持アビリティを取得
    public List<Ability_base> GetHavingAbilitiesForCategory(CONST.ABILITY.Category targetCategory)
    {
        return this.charParameters.HavingAbility.Where(t => t.category == targetCategory).ToList();
    }

    // 弱点属性
    public List<CONST.UTILITY.Element> GetWeakElement()
    {
        return this.charParameters.WeakElement;
    }

    // 耐性属性
    public List<CONST.UTILITY.Element> GetStrongElement()
    {
        return this.charParameters.StrongElement;
    }

    // 行動回数を返す
    public int GetCharActionCount()
    {
        // パラメータから直接取得せず、別変数を返して取得する
        return this.countActionATurn;
    }

    // パラメーター一覧を返す
    public CharParameter GetCharData()
    {
        return this.charParameters;
    }
}
