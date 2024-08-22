using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    private Image imgCard;
    public Sprite frontCard;
    public Sprite backCard;
    private bool isCardFront;

    private void Awake()
    {
        isCardFront = false;
        imgCard = GetComponent<Image>();
        imgCard.sprite = backCard;
    }

    // 特定位置にカードを移動する
    public void MoveCardFixedPosition(Vector2 reachPos, float speed)
    {
        this.gameObject.GetComponent<RectTransform>().DOAnchorPos(reachPos, CONST.ANIMATION_SPEED.MOVE_CARD_SPEED)
                .SetEase(Ease.Linear);
    }

    /// <summary>
    /// 特定位置にカードを移動しつつ、カードを表にする
    /// </summary>
    public void MoveCardFixedPositionWithOpenCard(Vector2 reachPos, float speed)
    {
        this.gameObject.GetComponent<RectTransform>().DOAnchorPos(reachPos, CONST.ANIMATION_SPEED.MOVE_CARD_SPEED)
                .SetEase(Ease.Linear);
        imgCard.transform.DOLocalRotate(new Vector3(0, 90, 0), CONST.ANIMATION_SPEED.FLIP_CARD_SPEED)
                              .SetEase(Ease.OutQuad).OnComplete(() => ChangeCard(!isCardFront));
    }

    /// <summary>
    /// カードの絵柄を変えて回転表示させる
    /// </summary>
    /// <param name="toFront"></param>
    private void ChangeCard(bool toFront)
    {

        imgCard.sprite = toFront ? frontCard : backCard;


        // 画像を差し替えて表に表示
        imgCard.transform.DOLocalRotate(new Vector3(0, 0, 0), CONST.ANIMATION_SPEED.FLIP_CARD_SPEED).SetEase(Ease.OutQuad);
        isCardFront = toFront;
    }

    /// <summary>
    /// 下側にカードをフェードアウトさせる
    /// </summary>
    public async UniTask FadeOutForUnder()
    {
        await DOTween.Sequence()
                .Append(this.gameObject.GetComponent<RectTransform>().DOMoveY(5f, CONST.ANIMATION_SPEED.FADEOUT_CARD_SPEED))
                .Join(this.gameObject.GetComponent<Image>().DOFade(endValue: 0f, duration: CONST.ANIMATION_SPEED.FADEOUT_CARD_SPEED))
                .AsyncWaitForCompletion();
    }
}
