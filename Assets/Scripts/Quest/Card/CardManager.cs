using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public QuestManager questManager;

    // 画面外に配置するカードのポジション
    [SerializeField]
    RectTransform position0;

    // 現在のカードのポジション
    [SerializeField]
    public RectTransform parentCardPositions;

    // 現在フロアのカードリスト(questManagerからのコピー)
    public List<RectTransform> eventCardList;

    // 配置するカードの各ポジション座標一覧
    public List<RectTransform> cardPostions;

    // 選択可能なカード枚数
    public int canSelectCardNumber;

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {

        // 選択可能枚数の確認
        canSelectCardNumber = QuestData.instance.canSelectCardNumber;

        RectTransform[] cardDisplayArea = parentCardPositions.GetComponentsInChildren<RectTransform>();
        foreach (var item in cardDisplayArea.Select((value, index) => new { value, index }))
        {
            if (item.index == 0 || item.index == 1)
                continue;

            cardPostions.Add(item.value.GetComponent<RectTransform>());
            item.value.gameObject.SetActive(false);

        }
    }

    /// <summary>
    /// 画面外にカードを作成する
    /// </summary>
    /// <param name="cardList"></param>
    public void CreateCardOnPositon0(List<CONST.QUEST.CardType> cardList)
    {
        foreach (var card in cardList.Select((value, index) => new { value, index }))
        {
            int cardNo = (int)card.value;
            var targetCardPrefab = GameObject.Instantiate(position0);
            targetCardPrefab.transform.SetParent(parentCardPositions.transform, false);
            targetCardPrefab.name = $"card_{cardNo}_{card.index}";
            targetCardPrefab.GetComponent<CardPropertyManager>().SetCardType(card.value);
            targetCardPrefab.GetComponent<CardPropertyManager>().SetCardRowID(card.index);
            if (card.value == CONST.QUEST.CardType.Deleted)
            {
                targetCardPrefab.GetComponent<CardUIManager>().SetSpriteClear();
            }

            // ?J?[?h????????

            // ?J?[?h???X?g??????

            targetCardPrefab.gameObject.SetActive(true);
            eventCardList.Add(targetCardPrefab);
        }

        this.MoveCardToEachPosition(eventCardList);

        // 選択可能範囲内にあるカードを選択可能にする
        SetCanSelected(this.canSelectCardNumber);
    }

    /// <summary>
    /// それぞれの表示位置にカードを生成
    /// </summary>
    /// <param name="cardList"></param>
    public void CreateCardOnPositonEach(List<CONST.QUEST.CardType> cardList)
    {
        foreach (var card in cardList.Select((value, index) => new { value, index }))
        {
            int cardNo = (int)card.value;
            var targetCardPrefab = GameObject.Instantiate(position0);
            targetCardPrefab.transform.SetParent(parentCardPositions.transform, false);
            targetCardPrefab.transform.position = cardPostions[card.index].transform.position;
            targetCardPrefab.name = $"card_{cardNo}_{card.index}";
            targetCardPrefab.GetComponent<CardPropertyManager>().SetCardType(card.value);
            targetCardPrefab.GetComponent<CardPropertyManager>().SetCardRowID(card.index);
            if (card.value == CONST.QUEST.CardType.Deleted)
            {
                targetCardPrefab.GetComponent<CardUIManager>().SetSpriteClear();
            }

            // ?J?[?h????????

            // ?J?[?h???X?g??????

            targetCardPrefab.gameObject.SetActive(true);
            eventCardList.Add(targetCardPrefab);
        }
    }

    /// <summary>
    /// 削除、選択済みカードをリストから削除し、順番に並び替え
    /// </summary>
    /// <param name="cardList"></param>
    public void ReMoveCardPosition(List<CONST.QUEST.CardType> cardList)
    {
        // 削除、選択済みステータスのカードを削除
        eventCardList.RemoveAll(c => c.GetComponent<CardPropertyManager>().GetCardType() == CONST.QUEST.CardType.Deleted ||
                                     c.GetComponent<CardPropertyManager>().GetCardType() == CONST.QUEST.CardType.Selected);

        // rowIDを再採番
        foreach (var card in eventCardList.Select((value, index) => new { value, index }))
        {
            card.value.GetComponent<CardPropertyManager>().SetCardRowID(card.index);
        }

        this.MoveCardToEachPosition(eventCardList);

        // 選択可能範囲内にあるカードを選択可能にする
        SetCanSelected(this.canSelectCardNumber);
    }



    /// <summary>
    /// 各位置にカードを移動させる
    /// </summary>
    public void MoveCardToEachPosition(List<RectTransform> eventCardList)
    {
        foreach (var card in eventCardList.Select((value, index) => new { value, index }))
        {
            card.value.GetComponents<CardUIManager>().Initialize();

            // 各ポジションにカードを配置する
            if (card.index <= canSelectCardNumber - 1)
            {
                card.value.GetComponent<CardUIManager>().MoveCardFixedPositionWithOpenCard(cardPostions[card.index].anchoredPosition, CONST.ANIMATION_SPEED.FLIP_CARD_SPEED);
            }
            else
            {
                card.value.GetComponent<CardUIManager>().MoveCardFixedPosition(cardPostions[card.index].anchoredPosition, CONST.ANIMATION_SPEED.FLIP_CARD_SPEED);

            }
        }
    }

    /// <summary>
    /// 選択されたカードのイベントを実行するインターフェース処理
    /// </summary>
    public async void DoEvent(CONST.QUEST.CardType selectedCardType, int rowID)
    {
        // 全カードを選択不可にする
        this.SetCanSelectedAllCard(false);

        // 選択範囲内の非選択カードを削除するアニメーションを実行する
        await this.DropUnselectedCard(rowID);

        // カードに設定されているイベントを実行する
        // 実行はQuestManagerで実行
        questManager.executeCardEvent(selectedCardType, canSelectCardNumber, rowID);
    }

    /// 選択可能かのフラグを更新
    /// </summary>
    /// <param name="canSelectedCardNumber"></param>
    public void SetCanSelected(int canSelectedCardNumber)
    {
        for (int i = 0; i < canSelectedCardNumber; i++)
        {
            this.eventCardList[i].gameObject.GetComponent<CardPropertyManager>().SetCanSelectCard(true);
        }
    }

    /// <summary>
    /// 全てのカードを選択可能に変更
    /// </summary>
    /// <param name="canSelect"></param>
    public void SetCanSelectedAllCard(bool canSelect)
    {
        foreach (RectTransform card in this.eventCardList)
        {
            card.gameObject.GetComponent<CardPropertyManager>().SetCanSelectCard(canSelect);
        }
    }

    // 選択されたカードを削除するエフェクトを実行
    public async Task DropSelectedCard(int selectedCardIndex)
    {
        Sequence drop_sequence = DOTween.Sequence();
        this.eventCardList[selectedCardIndex].gameObject.GetComponent<CardUIManager>().FadeOutForUnder(seq: drop_sequence);
        await drop_sequence.AsyncWaitForCompletion();
    }

    // 非選択カードを削除するエフェクトを実行する
    public async Task DropUnselectedCard(int selectedCardIndex)
    {
        Sequence drop_sequence = DOTween.Sequence();
        foreach (var card in this.eventCardList.Select((value, index) => new { value, index }))
        {
            if (card.index != selectedCardIndex && card.index < canSelectCardNumber)
            {
                card.value.gameObject.GetComponent<CardUIManager>().FadeOutForUnder(seq: drop_sequence);
            }
        }
        await drop_sequence.AsyncWaitForCompletion();
    }

}
