using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �s�����\���pUI
/// </summary>
public class ActionRowNumberListScrollController : MonoBehaviour
{
    [SerializeField]
    RectTransform action_List_contents = null;
    private RectTransform _contentArea;

    BattleActionList _battleActionList;

    /// <summary>
    /// �A�r���e�B���s�N���X
    /// </summary>
    [SerializeField]
    private AbilityManager abilityManager;

    [SerializeField]
    private ItemManager itemManager;

    void Start()
    {
        // editor��ɕ\������Ă�����̂��\���ɂ���
        GameObject.Find("Action_List_Canvas").SetActive(false);

    }


    /// <summary>
    /// �s�������Ƃ�UI�ɕ\������
    /// </summary>
    public void Setup_ActionRowNumberListUI(List<BattleAction> action_List)
    {
        // ���ɕ\������Ă���Ε\�����e��S�폜
        var items = GameObject.FindGameObjectsWithTag("Action_row_number_content");

        //�\�����Ă���{�^���̍폜 
        foreach (var item in items)
        {
            Destroy(item);
        }

        var content = GameObject.Find("Action_List_Canvas");
        foreach (var ability in action_List.Select((value, index) => new { value, index }))
        {
            var ability_content = GameObject.Instantiate(action_List_contents) as RectTransform;
            ability_content.SetParent(transform, false);

            var texts = ability_content.GetComponentsInChildren<Text>();
            int count = 1;

            foreach(var text in texts) {
                Debug.Log(text.name);
                // TODO: �I�u�W�F�N�g����ύX�����炱�̕������ύX���邱��
                if (text.name.StartsWith("Action_row"))
                {
                    text.text = (ability.index + 1).ToString();
                }else
                {
                    switch (ability.value.action)
                    {
                        case CONST.BATTLE_ACTION.COMMAND.Attack:
                        case CONST.BATTLE_ACTION.COMMAND.Defence:
                        case CONST.BATTLE_ACTION.COMMAND.Ability:
                            text.text = this.abilityManager.getAbilityDisplayName(ability.value.abilityName);
                            break;
                        case CONST.BATTLE_ACTION.COMMAND.Item:
                            text.text = this.itemManager.getItemDisplayName(ability.value.itemName);
                            break;

                    }
                }

                count++;
            }

            ability_content.gameObject.SetActive(true);

        }
    }
}
