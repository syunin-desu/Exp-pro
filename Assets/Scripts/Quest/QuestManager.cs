using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{

    // 現在の階層
    public StageUIManager stageUI;
    public GameObject enemyPrefab;
    public SceneTransitionManager sceneTransitionManager;
    public PartyMember w_PartyMember;

    //敵に遭遇するテーブル:-1なら遭遇しない 0なら遭遇する
    int[] encountTable = { -1, -1, 0, -1, 0, -1 };

    private int currentFloor = 0; //現在の階層

    private void Start()
    {
        // QuestDataから現在の階層を読み込む
        currentFloor = QuestData.instance.currentFloor;

        stageUI.UpdateUI(currentFloor);
    }

    public void OnNextButton()
    {
        currentFloor++;
        //進行度をUIに反映
        stageUI.UpdateUI(currentFloor);

        if (encountTable.Length <= currentFloor)
        {
            Debug.Log("クエストクリア");
            stageUI.ShowClearText();

        }
        else if (encountTable[currentFloor] == 0)
        {
            EncountEnemy();
        }
    }

    /// <summary>
    /// 町へ帰る
    /// </summary>
    public void ReturnTown()
    {
        sceneTransitionManager.LoadTo(CONST.SCENE.Scene.Town);
    }

    void EncountEnemy()
    {
        // シーン遷移するため
        // ダンジョンの進捗状態、パーティー状態を保存する(Autoセーブ的な)
        QuestData.instance.currentFloor = currentFloor;

        // マスターに現在のプレイヤーデータを保存する
        // プレイヤーサイドのステータスを更新する
        PlayerData.instance.UpdatePlayerData(this.w_PartyMember.GetCharParameters());

        // バトルシーンをロードする
        SceneManager.LoadScene(CONST.SCENE.Scene.Battle.ToString());
    }
}
