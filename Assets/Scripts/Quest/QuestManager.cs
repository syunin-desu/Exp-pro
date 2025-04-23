using CONST;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class QuestManager : MonoBehaviour
{

    // 現在の階層
    public StageUIManager stageUI;
    public GameObject enemyPrefab;
    public SceneTransitionManager sceneTransitionManager;
    public PartyMember w_PartyMember;
    public CardUIManager cardUIManager;
    public CardManager cardManager;

    public PlayerMenuUIManager playerMenuUIManager;
    public DeviceInputController deviceInputController;
    List<CONST.QUEST.CardType> cardList = new List<CONST.QUEST.CardType>();

    private int currentFloor = 0; //現在の階層

    // メニュー画面状態
    private CONST.QUEST_MENU_STATUS.MenuStatus currentMenuStatus;

    // ダンジョンに最初に張ったとき、ロードした時
    private async void Start()
    {
        // QuestDataから現在の階層を読み込む
        currentFloor = QuestData.instance.currentFloor;

        // QuestDataからカード配置を読み込む
        cardList = QuestData.instance.currentCardList;

        cardManager.Initialize();

        if (QuestData.instance.animateCardInitialize)
        {
            cardManager.CreateCardOnPositon0(cardList);
        }
        else
        {
            // 初期表示(アニメーションなし)
            cardManager.CreateCardOnPositonEach(cardList);

            // 選択済みのカードを削除するアニメーション
            int targetIndex = cardList.FindIndex(c => c == CONST.QUEST.CardType.Selected);
            await cardManager.DropSelectedCard(targetIndex);
            cardList[targetIndex] = CONST.QUEST.CardType.Deleted;

            // 削除ステータスのカードを削除
            cardList.RemoveAll(c => c == CONST.QUEST.CardType.Deleted);

            // 並び替えアニメーション
            cardManager.ReMoveCardPosition(cardList);

        }

        currentMenuStatus = QUEST_MENU_STATUS.MenuStatus.Main;

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

        //if (encountTable.Length <= currentFloor)
        //{
        //    Debug.Log("クエストクリア");
        //    stageUI.ShowClearText();

        //}
        //else if (encountTable[currentFloor] == 0)
        //{
        //    EncountEnemy();
        //}
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
        // ダンジョンの進捗状態、パーティー状態を保存する(Autoセーブ的な)
        QuestData.instance.currentFloor = currentFloor;

        // 現在のカード状況を保存
        QuestData.instance.currentCardList = cardList;

        // マスターに現在のプレイヤーデータを保存する
        // プレイヤーサイドのステータスを更新する
        PlayerData.instance.UpdatePlayerData(this.w_PartyMember.GetCharParameters());

        // InputActionsを無効にする
        deviceInputController.DisableInputAction();
        // バトルシーンをロードする
        SceneManager.LoadScene(CONST.SCENE.Scene.Battle.ToString());
    }

    /// <summary>
    /// 選択されたカードイベントを実施
    /// </summary>
    /// <param name="selectedCardType"></param>
    public void executeCardEvent(CONST.QUEST.CardType selectedCardType, int canSelectCardNumber, int selectedCardIndex)
    {
        // 選択範囲内の非選択カードを削除
        for (var i = 0; i < canSelectCardNumber; i++)
        {
            if (i != selectedCardIndex)
            {
                this.UpdateUnSelectedCard(i, CONST.QUEST.CardType.Deleted);
            }
            else
            {
                this.UpdateUnSelectedCard(i, CONST.QUEST.CardType.Selected);
            }
        }


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

    /// <summary>
    /// プレイヤーメニューの表示する
    /// </summary>
    public void ShowPlayerMenu()
    {
        this.currentMenuStatus = CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu;
        this.playerMenuUIManager.ShowPlayerMenu();
    }

    /// <summary>
    /// プレイヤーメニューを閉じる
    /// </summary>
    public void ClosePlayerMenu()
    {
        this.currentMenuStatus = CONST.QUEST_MENU_STATUS.MenuStatus.Main;
        this.playerMenuUIManager.ClosePlayerMenu();
    }

    // 選択したカードのステータスを変更
    public void UpdateUnSelectedCard(int deletedCardIndex, CONST.QUEST.CardType updateStatus)
    {

        this.cardList[deletedCardIndex] = updateStatus;
    }

    public void UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus targetStatus)
    {
        this.currentMenuStatus = targetStatus;
    }

    public CONST.QUEST_MENU_STATUS.MenuStatus GetCurrentMenuStatus()
    {
        return currentMenuStatus;
    }
}
