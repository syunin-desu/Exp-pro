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

    // ��ʊO�̃J�[�h�u����
    [SerializeField]
    RectTransform position0;

    // �J�[�h�\���̈�e�I�u�W�F�N�g
    [SerializeField]
    public RectTransform parentCardPositions;

    // �J�[�h���X�g
    public List<RectTransform> eventCardList;

    // �J�[�h�̔z�u���X�g
    public List<RectTransform> cardPostions;

    // �v���C���[���I���\�ȃJ�[�h����
    public int canSelectCardNumber = 3;

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// ����������(�ʏ��start����QuestManager���x�����s����邽��)
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
    /// ��ʊO�ɃJ�[�h�𐶐����Ĕz�u
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

            // �J�[�h�摜�ݒ�

            // �J�[�h���X�g�ɒǉ�
            eventCardList.Add(targetCardPrefab);

            targetCardPrefab.gameObject.SetActive(true);

        }

        this.MoveCardToEachPosition(eventCardList);

        // �S�J�[�h��I���\�ɂ���
        SetCanSelectedAllCard(true);
    }

    /// <summary>
    /// �J�[�h�����ꂼ��̕\���ʒu�Ɉړ�����
    /// </summary>
    public void MoveCardToEachPosition(List<RectTransform> eventCardList)
    {
        foreach (var card in eventCardList.Select((value, index) => new { value, index }))
        {
            card.value.GetComponents<CardUIManager>().Initialize();

            // �I��̈�̃J�[�h�����Ȃ�\�ɂ���
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
    /// �I�����ꂽ�J�[�h�̃C�x���g�����s�܂ł̃C���^�[�t�F�[�X
    /// </summary>
    public void DoEvent(CONST.QUEST.CardType selectedCardType, int rowID)
    {
        // �J�[�h�I��s��ԂɈڍs
        this.SetCanSelectedAllCard(false);

        // �I�����ꂽ�J�[�h�̂ݎc���āA����ȊO�̑I���\�͈͓��̃J�[�h��UI��폜����
        this.DropUnselectedCard(rowID);

        // �C�x���g���s
        // QuestManager�ɃC�x���g�̎�ނ����n���āA������Ŏ��s������
        questManager.executeCardEvent(selectedCardType);
    }

    /// <summary>
    /// �S�J�[�h�̑I���t���O��؂�ւ���
    /// </summary>
    /// <param name="canSelect"></param>
    public void SetCanSelectedAllCard(bool canSelect)
    {
        foreach (RectTransform card in this.eventCardList)
        {
            card.gameObject.GetComponent<CardPropertyManager>().SetCanSelectCard(canSelect);
        }
    }

    // �I�����ꂽ�J�[�h�̂ݎc���āA����ȊO�̑I���\�͈͓��̃J�[�h��UI��폜����
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
