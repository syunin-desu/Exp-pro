using CONST;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public QuestManager questManager;

    // 画面外のカード置き場
    [SerializeField]
    RectTransform position0;

    // カード表示領域親オブジェクト
    [SerializeField]
    public RectTransform parentCardPositions;

    // カードリスト
    public List<RectTransform> eventCardList;

    // カードの配置リスト
    public List<RectTransform> cardPostions;

    // プレイヤーが選択可能なカード枚数
    public int canSelectCardNumber = 3;

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 初期化処理(通常のstartだとQuestManagerより遅く実行されるため)
    /// </summary>
    public void Initialize()
    {
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
    /// 画面外にカードを生成して配置
    /// </summary>
    /// <param name="cardList"></param>
    public void CreateCardOnPositon0(CONST.QUEST.CardType[] cardList)
    {
        foreach (var card in cardList.Select((value, index) => new { value, index }))
        {
            int cardNo = (int)card.value;
            var targetCardPrefab = GameObject.Instantiate(position0);
            targetCardPrefab.transform.SetParent(parentCardPositions.transform, false);
            targetCardPrefab.name = $"card_{cardNo}_{card.index}";
            targetCardPrefab.GetComponent<CardPropertyManager>().SetCardType(card.value);
            targetCardPrefab.GetComponent<CardPropertyManager>().SetCardRowID(card.index);

            // カード画像設定

            // カードリストに追加
            eventCardList.Add(targetCardPrefab);

            targetCardPrefab.gameObject.SetActive(true);

        }

        this.MoveCardToEachPosition(eventCardList);

        // 全カードを選択可能にする
        SetCanSelectedAllCard(true);
    }

    /// <summary>
    /// カードをそれぞれの表示位置に移動する
    /// </summary>
    public void MoveCardToEachPosition(List<RectTransform> eventCardList)
    {
        foreach (var card in eventCardList.Select((value, index) => new { value, index }))
        {
            card.value.GetComponents<CardUIManager>().Initialize();

            // 選択領域のカードが裏なら表にする
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
    /// 選択されたカードのイベントを実行までのインターフェース
    /// </summary>
    public void DoEvent(CONST.QUEST.CardType selectedCardType, int rowID)
    {
        // カード選択不可状態に移行
        this.SetCanSelectedAllCard(false);

        // 選択されたカードのみ残して、それ以外の選択可能範囲内のカードをUI上削除する
        this.DropUnselectedCard(rowID);

        // イベント実行
        // QuestManagerにイベントの種類だけ渡して、そちらで実行させる
        questManager.executeCardEvent(selectedCardType);
    }

    /// <summary>
    /// 全カードの選択フラグを切り替える
    /// </summary>
    /// <param name="canSelect"></param>
    public void SetCanSelectedAllCard(bool canSelect)
    {
        foreach (RectTransform card in this.eventCardList)
        {
            card.gameObject.GetComponent<CardPropertyManager>().SetCanSelectCard(canSelect);
        }
    }

    // 選択されたカードのみ残して、それ以外の選択可能範囲内のカードをUI上削除する
    public async void DropUnselectedCard(int selectedCardIndex)
    {
        foreach (var card in this.eventCardList.Select((value, index) => new { value, index }))
        {
            if (card.index != selectedCardIndex && card.index < canSelectCardNumber)
            {
                await card.value.gameObject.GetComponent<CardUIManager>().FadeOutForUnder();
            }
        }
    }
}
