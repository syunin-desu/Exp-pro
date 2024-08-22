using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードに関して、プレイヤーからの入力を管理するクラス
/// </summary>
public class CardInputManager : MonoBehaviour
{
    public CardManager cardManager;


    /// <summary>
    /// カードが選択されたとき
    /// </summary>
    public void SelectCard()
    {
        if (this.gameObject.GetComponent<CardPropertyManager>().GetCanSelectCard())
        {
            CONST.QUEST.CardType selected_obj_card_type = this.gameObject.GetComponent<CardPropertyManager>().GetCardType();
            int rowID = this.gameObject.GetComponent<CardPropertyManager>().GetCardRowID();

            cardManager.DoEvent(selected_obj_card_type, rowID);
        }
    }
}
