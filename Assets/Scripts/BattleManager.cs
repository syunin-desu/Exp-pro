using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//対戦を管理
public class BattleManager : MonoBehaviour
{
    public PlayerUIManager playerUI;
    public PlayerManager player;
    public EnemyUIManager enemyUI;
    public QuestManager questManager;
    EnemyManager enemy;

    void Start()
    {
        enemyUI.gameObject.SetActive(false);
    }

    public void SetUp(EnemyManager enemyManager)
    {
        enemyUI.gameObject.SetActive(true);
        enemy = enemyManager;
        enemyUI.SetUpUI(enemy);
        playerUI.SetUpUI(player);

        enemy.AddEventListenerOnTap(PlayerAttack);
    }

    void PlayerAttack()
    {
        //Playerが攻撃
        player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
        if (enemy.hp <= 0)
        {
            enemyUI.gameObject.SetActive(false);
            Destroy(enemy.gameObject);
            EndBattle();
        }
        else
        {
            EnemyTurn();
        }
    }

    void EnemyTurn()
    {
        //Enemyが攻撃
        enemy.Attack(player);
        playerUI.UpdateUI(player);
    }

    void EndBattle()
    {

        questManager.EndBattle();
    }


}
