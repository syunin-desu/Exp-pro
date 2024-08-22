using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h�̏��N���X
/// </summary>
public class CardPropertyManager : MonoBehaviour
{
    /// <summary>
    /// �J�[�h�̎��
    /// </summary>
    [SerializeField]
    private CONST.QUEST.CardType cardType;

    /// <summary>
    /// ����ID(0�X�^�[�g)
    /// </summary>
    private int rowID;

    /// <summary>
    /// �I���\���ǂ���
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
