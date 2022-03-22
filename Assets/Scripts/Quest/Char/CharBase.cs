using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//敵や、プレイヤーなどのキャラの派生元
public class CharBase : MonoBehaviour
{

    public string char_name;
    public int hp;
    public int max_hp;

    public int strange;
    public int speed;
    public int intelligence { get; set; }

    public List<string> weakElement { get; set; }
    public List<string> strongElement { get; set; }

    //防御フラグ
    private bool action_defence;
    //キャラロール
    public int char_role;

    // 所持アビリティ
    public List<string> HavingAbility = new List<string>();



    void Start()
    {
        this.reset_defence_flag();
    }

    //ターン終了時の処理
    public void resetTurnEnd_char_parameter()
    {
        this.reset_defence_flag();
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
        float defenceRate = this.action_defence ? CONST.BATTLE_RATE.RATE_DEFENCE : CONST.BATTLE_RATE.RATE_DEFAULT_DEFENCE;
        this.hp -= (int)Math.Ceiling(damage / defenceRate);

        if (hp <= 0)
        {
            hp = 0;
        }
    }

    //バフのリセット
    public void resetBuff()
    {
        //防御をやめる
        this.reset_defence_flag();
    }

    // 防御する
    public void Defence()
    {
        this.action_defence = true;
    }

    //防御フラグをリセット
    private void reset_defence_flag()
    {
        this.action_defence = false;

    }

    //防御フラグを取得
    public bool get_actionDefence()
    {
        return this.action_defence;
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
}
