using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵を管理するもの(ステータス/クリック検出)
public class EnemyManager : MonoBehaviour
{
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
        Debug.Log(name + "に" + damage + "のダメージを与えた");
    }
    public void OnTap()
    {
        Debug.Log("クリックされた");
    }
}
