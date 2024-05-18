using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// TODO しかるべきファイルにあとで格納する
public class HavingItem
{
    private string _itemName;
    private int _count;

    public string ItemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }

    public int ItemCount
    {
        get { return _count; }
        set { _count = value; }
    }

}

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

    // 所持アビリティ
    public List<string> HavingAbility = new List<string>();


    //所持アイテム
    // TODO 現状モックで実装する
    public List<HavingItem> HavingItem = new List<HavingItem>();


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
    public virtual void Heal(int healValue)
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

        // TODO Itemに関してはmockで実装
        // アイテム所持データの周りが実装されたら消す
        HavingItem mockHavingItem = new HavingItem();
        mockHavingItem.ItemName = "BluePotion";
        mockHavingItem.ItemCount = 4;
        HavingItem.Add(mockHavingItem);

    }

    /// <summary>
    /// アイテム個数を減少させる
    /// </summary>
    /// <param name="itemName">対象アイテム名</param>
    /// <param name="reduceItemCount">減少させる個数</param>
    public void reduceItemCount(string itemName, int reduceItemCount)
    {
        var targetItem = GetHavingItem().Find(item => item.ItemName == itemName);

        //アイテム数を減少させる
        targetItem.ItemCount -= reduceItemCount;

        // アイテムがなくなったら削除
        if (targetItem.ItemCount <= 0)
        {
            this.HavingItem.Remove(targetItem);
        }

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

    // INT
    public int GetInteli()
    {
        return this.charParameters.INT;
    }

    //strange
    public int GetStrange()
    {
        return this.charParameters.STR;
    }

    //所持アビリティ
    public List<string> GetHavingAbilities()
    {
        return this.charParameters.HavingAbility;
    }

    //所持アイテム
    public List<HavingItem> GetHavingItem()
    {
        return this.HavingItem;
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
