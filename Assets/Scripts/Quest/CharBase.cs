using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵や、プレイヤーなどのキャラの派生元
public class CharBase : MonoBehaviour
{
    public string NAME;
    public int HP;
    public int STRANGE;
    public int SPEED;
    bool action_defence;
    //攻撃する
    public void Attack(CharBase character)
    {

        character.Damage(this.STRANGE);

    }

    //ダメージを受ける
    public void Damage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
        }
    }
}
