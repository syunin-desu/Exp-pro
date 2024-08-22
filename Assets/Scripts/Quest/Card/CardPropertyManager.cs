using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードの情報クラス
/// </summary>
public class CardPropertyManager : MonoBehaviour
{
    /// <summary>
    /// カードの種別
    /// </summary>
    [SerializeField]
    private CONST.QUEST.CardType cardType;

    /// <summary>
    /// 順番ID(0スタート)
    /// </summary>
    private int rowID;

    /// <summary>
    /// 選択可能かどうか
    /// </summary>
    private bool canSelect = false;

    public CONST.QUEST.CardType GetCardType()
    {
        return cardType;
    }

    public int GetCardRowID()
    {
        return rowID;
    }

    public bool GetCanSelectCard()
    {
        return canSelect;
    }

    public void SetCardType(CONST.QUEST.CardType targetCardType)
    {
        this.cardType = targetCardType;
    }

    public void SetCardRowID(int row)
    {
        this.rowID = row;
    }

    public void SetCanSelectCard(bool canSelect)
    {
        this.canSelect = canSelect;
    }



}
