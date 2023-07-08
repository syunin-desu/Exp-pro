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

    public string char_name;
    public int hp;
    public int max_hp;

    public int strange;
    public int speed;
    public int intelligence { get; set; }

    public List<CONST.UTILITY.Element> weakElement { get; set; }
    public List<CONST.UTILITY.Element> strongElement { get; set; }

    //防御フラグ
    private bool action_defense = false;
    //キャラロール
    public int char_role;

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

        character.Damage(this.strange);

    }

    //ダメージを受ける
    public void Damage(int damage)
    {
        //ダメージ計算
        // TODO ダメージ計算式を見直し
        //防御アクションによるダメージ減少率を設定
        float defenceRate = this.action_defense ? CONST.BATTLE_RATE.RATE_DEFENCE : CONST.BATTLE_RATE.RATE_DEFAULT_DEFENCE;
        this.hp -= (int)Math.Ceiling(damage / defenceRate);

        if (hp <= 0)
        {
            hp = 0;
        }
    }

    /// <summary>
    /// HPを回復する
    /// </summary>
    /// <param name="healValue">回復量</param>
    public void Heal(int healValue)
    {

        Debug.Log("beforeHp:" + this.hp);
        this.hp += healValue;
        if (this.hp > this.max_hp)
        {
            this.hp = this.max_hp;
        }
        Debug.Log("afterHp:" + this.hp);

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
    public void SetParameter(CharData charData)
    {
        this.char_name = charData.Name;
        this.hp = charData.maxHp;
        this.max_hp = charData.maxHp;
        this.strange = charData.STR;
        this.speed = charData.SPEED;
        this.char_role = charData.ROLE;
        this.HavingAbility = charData.HavingAbility;
        this.intelligence = charData.INT;
        this.weakElement = charData.WeakElement;
        this.strongElement = charData.StrongElement;

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
    }

    //=============
    // getter,setter
    //=============

    //name
    public string GetName()
    {
        return this.char_name;
    }

    //hp
    public int GetHp()
    {
        return this.hp;
    }

    public int GetMaxHp()
    {
        return this.max_hp;
    }

    //speed
    public int GetSpeed()
    {
        return this.speed;
    }

    //strange
    public int GetStrange()
    {
        return this.strange;
    }

    //所持アビリティ
    public List<string> GetHavingAbilities()
    {
        return this.HavingAbility;
    }

    //所持アイテム
    public List<HavingItem> GetHavingItem()
    {
        return this.HavingItem;
    }
}
