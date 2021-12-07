using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //現在の階層
    public StageUIManager stageUI;
    public GameObject enemyPrefab;
    public BattleManager battleManager;

    //敵に遭遇するテーブル:-1なら遭遇しない 0なら遭遇する
    int[] encountTable = { -1, -1, 0, -1, 0, -1 };
    int currentStage = 0; //現在の階層

    private void Start()
    {
        stageUI.UpdateUI(currentStage);
    }

    public void OnNextButton()
    {
        currentStage++;
        //進行度をUIに反映
        stageUI.UpdateUI(currentStage);

        if (encountTable.Length <= currentStage)
        {
            Debug.Log("クエストクリア");
            //シーンを止める
        }
        else if (encountTable[currentStage] == 0)
        {
            EncountEnemy();
        }
    }

    void EncountEnemy()
    {
        stageUI.HideButtons();
        GameObject enemyObj = Instantiate(enemyPrefab);
        EnemyManager enemy = enemyObj.GetComponent<EnemyManager>();
        battleManager.SetUp(enemy);

    }

    public void EndBattle()
    {
        stageUI.showButtons();
    }
}
