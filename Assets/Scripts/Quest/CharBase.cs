using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//敵や、プレイヤーなどのキャラの派生元
public class CharBase : MonoBehaviour
{

    public string NAME;
    public int HP;
    public int STRANGE;
    public int SPEED;

    //防御フラグ
    private bool action_defence;
    //キャラロール
    public int char_role;

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

        character.Damage(this.STRANGE);

    }

    //ダメージを受ける
    public void Damage(int damage)
    {
        //ダメージ計算
        // TODO ダメージ計算式を見直し
        //防御アクションによるダメージ減少率を設定
        Debug.Log(this.char_role + ":" + this.action_defence);
        float defenceRate = this.action_defence ? Const.CO.RATE_DEFENCE : Const.CO.RATE_DEFAULT_DEFENCE;
        this.HP -= (int)Math.Ceiling(damage / defenceRate);

        if (HP <= 0)
        {
            HP = 0;
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
}
