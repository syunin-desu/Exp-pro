using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//対戦を管理
public class BattleManager : MonoBehaviour
{
    public PlayerUIManager playerUI;
    public PlayerManager player;
    public EnemyUIManager enemyUI;
    public EnemyManager enemy;
    void Start()
    {
        //Playerが攻撃
        player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
        //Enemyが攻撃
        enemy.Attack(player);
        playerUI.UpdateUI(player);
    }


}
