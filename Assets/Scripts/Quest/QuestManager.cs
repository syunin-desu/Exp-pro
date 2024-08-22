using CONST;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{

    // 現在の階層
    public StageUIManager stageUI;
    public GameObject enemyPrefab;
    public SceneTransitionManager sceneTransitionManager;
    public PartyMember w_PartyMember;
    public CardUIManager cardUIManager;
    public CardManager cardManager;

    //敵に遭遇するテーブル:-1なら遭遇しない 0なら遭遇する
    int[] encountTable = { -1, -1, 0, -1, 0, -1 };
    CONST.QUEST.CardType[] cardList =
    {
        // Mock実装 Excel等で階層ごとのカードリストを設定しておき、
        // 初めにランダムで並び変える
        CONST.QUEST.CardType.EncountEnemy,
        CONST.QUEST.CardType.EncountEnemy,
        CONST.QUEST.CardType.EncountEnemy,
        CONST.QUEST.CardType.EncountEnemy,
        CONST.QUEST.CardType.EncountEnemy,
        CONST.QUEST.CardType.EncountEnemy,
        CONST.QUEST.CardType.EncountEnemy,
        CONST.QUEST.CardType.EncountEnemy,
        CONST.QUEST.CardType.EncountEnemy,
        CONST.QUEST.CardType.EncountEnemy,
    };

    private int currentFloor = 0; //現在の階層

    // ダンジョンに最初に張ったとき、ロードした時
    private void Start()
    {
        // QuestDataから現在の階層を読み込む
        currentFloor = QuestData.instance.currentFloor;

        // QuestDataからカード配置を読み込む
        // 一旦Mockで実装
        // cardList = QuestData.instance.currentCardList;

        // MOCK: 全カードをposition 0に生成し、Eventカードリストを更新
        cardManager.Initialize();
        cardManager.CreateCardOnPositon0(cardList);

        stageUI.UpdateUI(currentFloor);
    }

    // 一階層進み、次回層の情報を初期化する時  
    private void InitializeNextFloor()
    {
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

    /// <summary>
    /// 選択されたカードイベントを実施
    /// </summary>
    /// <param name="selectedCardType"></param>
    public void executeCardEvent(CONST.QUEST.CardType selectedCardType)
    {
        switch (selectedCardType)
        {
            case CONST.QUEST.CardType.EncountEnemy:
                EncountEnemy();
                break;

            default:
                break;
        }

        // イベント事後処理
        // QuestManagerの仕事

        // カードの順番を更新
        // QuestManagerから呼び出させる想定

        // カード選択可能状態に移行する
        // CardUIManagerで処理させる
    }
}
