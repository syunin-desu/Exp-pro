using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトル時のアビリティ選択画面の中身を管理
/// </summary>
public class AbilityScrollController : MonoBehaviour
{
    [SerializeField]
    RectTransform ability_contents = null;
    private RectTransform _contentArea;

    public BattleActionList battleActionList;

    public PartyMember partyMember;

    /// <summary>
    /// アビリティ実行クラス
    /// </summary>
    [SerializeField]
    private AbilityManager abilityManager;

    void Start()
    {
        GameObject.Find("Ability_button").SetActive(false);
    }


    /// <summary>
    /// アビリティのパラメータを設定する
    /// </summary>
    public void Setup_abilityUI(List<string> ability_List)
    {
        foreach (string ability in ability_List)
        {
            var ability_content = GameObject.Instantiate(ability_contents) as RectTransform;
            ability_content.SetParent(transform, false);

            var texts = ability_content.GetComponentsInChildren<Text>();

            texts.First(t => t.name == "Text").text = this.abilityManager.getAbilityDisplayName(ability);
            texts.First(t => t.name == "ConsumeMP").text = this.abilityManager.getAbilityConsumeMP(ability).ToString();
            ability_content.gameObject.SetActive(true);

            // アクションリスト登録中のアビリティの消費MPの合計分を取得
            List<BattleAction> P_ActionList = battleActionList.GetP_ActionList();
            int all_ConsumeMp = 0;
            foreach (BattleAction action in P_ActionList)
            {
                if (action.action == CONST.BATTLE_ACTION.COMMAND.Ability)
                {
                    int consumeMp = this.abilityManager.getAbilityConsumeMP(action.abilityName);
                    all_ConsumeMp += consumeMp;
                }
            }

            // 現在のプレイヤーのMPから総消費MPを減らし、MPが足りないアビリティは非活性にする
            int leftMP = partyMember.GetMp() - all_ConsumeMp;
            if (leftMP < this.abilityManager.getAbilityConsumeMP(ability))
            {
                ability_content.GetComponentInChildren<Button>().enabled = false;
                texts.First(t => t.name == "Text").color = Color.gray;
                texts.First(t => t.name == "ConsumeMP").color = Color.gray;
                texts.First(t => t.name == "MP_Label").color = Color.gray;
            }
        }
    }
}
