using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine;

//Ability実行管理
public class AbilityManager : MonoBehaviour
{
    private List<Ability_base> _abilityList = new List<Ability_base>();

    private Calc_Battle_Manager calc_battle = new Calc_Battle_Manager();

    // Start is called before the first frame update
    void Start()
    {
        _abilityList = MasterData.instance.masterAbilityList;
    }

#nullable enable
    /// <summary>
    /// アビリティを実行する
    /// </summary>
    /// <param name="performChar">実行キャラ</param>
    /// <param name="targetChar">対象</param>
    /// <param name="execAbilityName">実行アビリティ名</param>
    /// <returns></returns>
    public async UniTask execAbility(CharBase performChar, CharBase? targetChar, string execAbilityName, List<CONST.ACTION.Ability_Action_Cell> execAbilityAction)
    {
        // アビリティの実行に必要なMPを消費する
        if (performChar.ConsumeMP(this.getAbilityData(execAbilityName).requiredMp))
        {
            foreach (CONST.ACTION.Ability_Action_Cell action in execAbilityAction)
            {
                switch (action)
                {
                    case CONST.ACTION.Ability_Action_Cell.MagicSingleAttack:
                        await this.MagicSingleAttack(performChar, targetChar, this.getAbilityData(execAbilityName));
                        break;
                    default:
                        Debug.Log("アクションとして登録されていないアクションが指定されました");
                        break;
                }
            }
        }
        else
        {
            Debug.Log("MPが足りず、実行できませんでした。");
        }

    }
#nullable disable

    //======================================
    // アクションの内容
    //======================================

    /// <summary>
    /// 魔法単体攻撃アクション
    /// </summary>
    /// <param name="performChar"></param>
    /// <param name="targetChar"></param>
    /// <param name="execAbilityData"></param>
    /// <returns></returns>
    private async UniTask MagicSingleAttack(CharBase performChar, CharBase targetChar, Ability_base execAbilityData)
    {

        int performerInt = performChar.GetInteli();
        int power = execAbilityData.power;
        CONST.UTILITY.Element Element = execAbilityData.Element;

        // TODO ダメージ計算用のファンクションを別のクラスで作成し、そこで行えるようにする
        float elementDamageRate = this.calc_battle.calcElementDamageRate(Element, targetChar);

        targetChar.Damage((int)((performerInt * power) * elementDamageRate));

        await UniTask.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));
    }

    //======================================
    // アビリティリストの呼び出し、書き込み関係
    //======================================

    /// <summary>
    /// 表示アビリティ名を返す
    /// </summary>
    /// <param name="abilityName">アビリティ名</param>
    /// <returns>表示アビリティ名</returns>
    public string getAbilityDisplayName(string abilityName)
    {
        Ability_base abilityDisplayName = this._abilityList.Find(ability => ability.Name == abilityName);
        return abilityDisplayName.displayName;
    }

    /// <summary>
    /// 消費MPを返す
    /// </summary>
    /// <param name="abilityName">アビリティ名</param>
    /// <returns>表示アビリティ名</returns>
    public int getAbilityConsumeMP(string abilityName)
    {
        Ability_base abilityDisplayName = this._abilityList.Find(ability => ability.Name == abilityName);
        return abilityDisplayName.requiredMp;
    }

    /// <summary>
    /// 表示アビリティ名からアビリティ名を取得する
    /// </summary>
    /// <param name="ability_DisplayName">表示アビリティ名</param>
    /// <returns>アビリティ名</returns>
    public string getAbilityNameForDisplayName(string ability_DisplayName)
    {
        Ability_base abilityName = this._abilityList.Find(ability => ability.displayName == ability_DisplayName);
        return abilityName.Name;
    }

    /// <summary>
    /// 表示アビリティ名からアビリティ名を取得する
    /// </summary>
    /// <param name="ability_DisplayName">表示アビリティ名</param>
    /// <returns>アビリティ名</returns>
    public List<CONST.ACTION.Ability_Action_Cell> getAbilityActionsForDisplayName(string ability_DisplayName)
    {
        Ability_base abilityName = this._abilityList.Find(ability => ability.displayName == ability_DisplayName);
        return abilityName.executeActionList;
    }

    /// <summary>
    /// アビリティ名からアビリティのアクションタイミングを取得
    /// </summary>
    /// <param name="abilityName">アビリティ名</param>
    /// <returns>アビリティタイミング</returns>
    public CONST.ACTION.Speed getAbilityTimingType(string abilityName)
    {
        Ability_base ability = this._abilityList.Find(ability => ability.Name == abilityName);
        return ability != null ? ability.timingType : CONST.ACTION.Speed.Normal;
    }

    /// <summary>
    /// アビリティのデータを取得
    /// </summary>
    /// <param name="abilityName">アビリティ名</param>
    /// <returns>アビリティのデータ</returns>
    private Ability_base getAbilityData(string abilityName)
    {
        return this._abilityList.Find(ability => ability.Name == abilityName);
    }
}
