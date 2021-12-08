using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//敵を管理するもの(ステータス/クリック検出)
public class EnemyManager : MonoBehaviour
{

    Action tapAction; //クリックされた時に実行したい関数
    public new string name;
    public int hp;
    public int at;

    //攻撃する
    public void Attack(PlayerManager player)
    {
        player.Damage(at);
    }

    //ダメージを受ける
    public void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
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
