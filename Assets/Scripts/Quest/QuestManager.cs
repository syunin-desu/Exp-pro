using CONST;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using NUnit.Framework;

public class QuestManager : MonoBehaviour
{

    // 現在の階層
    public StageUIManager stageUI;
    public SceneTransitionManager sceneTransitionManager;
    public PartyMember w_PartyMember;
    public HadItem hadItem;
    public CardUIManager cardUIManager;
    public CardManager cardManager;
    public PlayerMenuManager playerMenuManager;

    public PlayerMenuUIManager playerMenuUIManager;
    public DeviceInputController deviceInputController;
    List<BaseCardProperty> cardList = new List<BaseCardProperty>();

    private int currentFloor = 0; //現在の階層

    // メニュー画面状態
    private CONST.QUEST_MENU_STATUS.MenuStatus currentMenuStatus;

    // ダンジョンに最初に張ったとき、ロードした時
    private void Start()
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

            // 選択カードの削除と表示位置のリセット
            this.ResetCard();

        }

        currentMenuStatus = QUEST_MENU_STATUS.MenuStatus.Main;

        stageUI.UpdateUI(currentFloor);
    }

    // 一階層進み、次回層の情報を初期化する時  
    private void InitializeNextFloor()
    {
    }

    // ロード実行された際にUIを初期化、ロード状態に更新する
    public void OnDataLoaded()
    {

    }

    public void GetItems(BaseCardProperty baseCardProperty)
    {
        // アイテムを追加
        baseCardProperty.item.ForEach(item =>
        {
            hadItem.AddItem(new HavingItem()
            {
                id = item.id,
                Name = item.name,
                category = item.category,
                count = 1, // 複数を同タイミングで着脱する予定はないため一つとする
            });
        });

        // TODO: アイテム取得メッセージを表示

        // カードUpdate処理
        this.ResetCard();
    }

    public void NextFloor()
    {
        // フェードアウト

        // 次の階に進む
        currentFloor++;

        // カード状態をリセット
        cardList.Clear();
        cardManager.ClearCardList();


        // 次の階のカードをセット
    }

    /// <summary>
    /// 町へ帰る
    /// </summary>
    public void ReturnTown()
    {
        sceneTransitionManager.LoadTo(CONST.SCENE.Scene.Town);
    }

    void EncountEnemy(BaseCardProperty baseCardProperty)
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

        // 敵データを設定する
        BattleData.instance.UpdateBattleTargetEnemy(baseCardProperty.enemyData);

        // バトルシーンをロードする
        SceneManager.LoadScene(CONST.SCENE.Scene.Battle.ToString());
    }

    /// <summary>
    /// 選択されたカードイベントを実施
    /// </summary>
    /// <param name="selectedCardType"></param>
    public async void executeCardEvent(CONST.QUEST.CardType selectedCardType,
        int canSelectCardNumber,
        int selectedCardIndex,
        BaseCardProperty baseCardProperty)
    {
        // Drop対象外のカードのIndexを取得 
        List<int> excludeCardIndeies = new List<int>();
        foreach (var item in cardList.Select((value, index) => new { value, index }))
        {
            if (item.value.cartType == CONST.QUEST.CardType.NextFloor
                || item.value.cartType == CONST.QUEST.CardType.LockedNextFloor)
            {
                excludeCardIndeies.Add(item.index);
            }
        }

        // 選択範囲内の非選択カードを削除するアニメーションを実行する
        await cardManager.DropUnselectedCard(selectedCardIndex, excludeCardIndeies);

        // 選択範囲内の非選択カードを削除
        foreach (var item in cardList.Select((value, index) => new { value, index }))
        {
            if (item.index == selectedCardIndex)
            {
                item.value.cartType = CONST.QUEST.CardType.Selected;
                cardManager.UpdateCardListStatus(item.index, CONST.QUEST.CardType.Selected);
            }
            else if (item.index < canSelectCardNumber && item.value.cartType != CONST.QUEST.CardType.NextFloor)
            {
                item.value.cartType = CONST.QUEST.CardType.Deleted;
                cardManager.UpdateCardListStatus(item.index, CONST.QUEST.CardType.Deleted);
            }
        }

        switch (selectedCardType)
        {
            case CONST.QUEST.CardType.EncountEnemy:
                EncountEnemy(baseCardProperty);
                break;
            case CONST.QUEST.CardType.GetItem:
                this.GetItems(baseCardProperty);
                break;
            case CONST.QUEST.CardType.NextFloor:
                this.NextFloor();
                break;


            default:
                break;
        }
    }

    private async void ResetCard()
    {
        // 選択済みのカードを削除する
        await this.DropSelectedCard();

        // 並び替えアニメーション
        cardManager.ReMoveCardPosition();
    }

    private async Task DropSelectedCard()
    {
        // 選択済みのカードを削除するアニメーション
        int targetIndex = cardList.FindIndex(c => c.cartType == CONST.QUEST.CardType.Selected);
        await cardManager.DropSelectedCard(targetIndex);
        cardList[targetIndex].cartType = CONST.QUEST.CardType.Deleted;

        // 削除ステータスのカードを削除
        cardList.RemoveAll(c => c.cartType == CONST.QUEST.CardType.Deleted);
    }

    /// <summary>
    /// プレイヤーメニューの表示する
    /// </summary>
    public void ShowPlayerMenu()
    {
        this.currentMenuStatus = CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu;
        // メニュー側初期化処理
        playerMenuManager.InitializePlayerMenuSelectButtons();

        this.playerMenuUIManager.ShowPlayerMenu();
    }

    /// <summary>
    /// プレイヤーメニューを閉じる
    /// </summary>
    public void ClosePlayerMenu()
    {
        this.currentMenuStatus = CONST.QUEST_MENU_STATUS.MenuStatus.Main;
        this.playerMenuManager.ClosePlayerMenu();
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
