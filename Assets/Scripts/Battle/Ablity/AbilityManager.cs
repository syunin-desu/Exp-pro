using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public async Task execAbility(CharBase performChar, CharBase? targetChar, string execAbilityID, List<CONST.ACTION.Ability_Action_Cell> execAbilityAction)
    {
        // アビリティの実行に必要なMPを消費する
        if (performChar.ConsumeMP(this.getAbilityData(execAbilityID).requiredMp))
        {
            foreach (CONST.ACTION.Ability_Action_Cell action in execAbilityAction)
            {
                switch (action)
                {
                    case CONST.ACTION.Ability_Action_Cell.MagicSingleAttack:
                        if (targetChar is null)
                        {
                            Debug.Log("対象が選択されていないため実行できませんでした");
                            break;
                        }
                        await this.MagicSingleAttack(performChar, targetChar, this.getAbilityData(execAbilityID));
                        break;
                    case CONST.ACTION.Ability_Action_Cell.Heal:
                        await this.MagicSingleHeal(performChar, targetChar ?? performChar, this.getAbilityData(execAbilityID));
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
    private async Task MagicSingleAttack(CharBase performChar, CharBase targetChar, Ability_base execAbilityData)
    {

        int performerInt = performChar.GetInteli();
        int power = execAbilityData.power;
        CONST.UTILITY.Element Element = execAbilityData.Element;

        // TODO ダメージ計算用のファンクションを別のクラスで作成し、そこで行えるようにする
        float elementDamageRate = this.calc_battle.calcElementDamageRate(Element, targetChar);

        targetChar.Damage((int)((performerInt * power) * elementDamageRate));

        await Task.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));
    }

    /// <summary>
    /// 魔法単体回復アクション
    /// </summary>
    /// <param name="performChar"></param>
    /// <param name="targetChar"></param>
    /// <param name="execAbilityData"></param>
    /// <returns></returns>
    private async Task MagicSingleHeal(CharBase performChar, CharBase targetChar, Ability_base execAbilityData)
    {
        int performerMagicPower = performChar.GetMagicPoser();
        int kindness = performChar.GetKindness();
        targetChar.HealHP((int)(performerMagicPower * kindness));
        await Task.Delay(TimeSpan.FromSeconds(CONST.UTILITY.BATTLEACTION_DELAY));
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

    public string getAbilityDisplayNameForID(string id)
    {
        Ability_base abilityDisplayName = this._abilityList.Find(ability => ability.id == id);
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
    public string getAbilityNameForAbilityID(string abilityID)
    {
        Ability_base abilityName = this._abilityList.Find(ability => ability.id == abilityID);
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
    /// 表示アビリティ名からアビリティ名を取得する
    /// </summary>
    /// <param name="ability_DisplayName">表示アビリティ名</param>
    /// <returns>アビリティ名</returns>
    public List<CONST.ACTION.Ability_Action_Cell> getAbilityActionsForID(string abilityID)
    {
        Ability_base abilityName = this._abilityList.Find(ability => ability.id == abilityID);
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
    /// アビリティ名からアビリティの実行優先度ランクを取得
    /// </summary>
    /// <param name="abilityName">アビリティ名</param>
    /// <returns>アビリティタイミング</returns>
    public int getAbilitySpeedRank(string abilityName)
    {
        Ability_base ability = this._abilityList.Find(ability => ability.Name == abilityName);
        return ability != null ? ability.speed_rank : 0;
    }

    public int getAbilityConsumeMPForID(string abilityID)
    {
        return this._abilityList.FirstOrDefault(ability => ability.id == abilityID).requiredMp;
    }

    public string GetAbilityDiscriptionfromMaster(string abilityID)
    {
        return this._abilityList.FirstOrDefault(ability => ability.id == abilityID).description;
    }

    public CONST.ACTION.TYPE GetAbilityActionType(string abilityID)
    {
        return this._abilityList.FirstOrDefault(ability => ability.id == abilityID).Type;
    }

    /// <summary>
    /// アビリティのデータを取得
    /// </summary>
    /// <param name="abilityName">アビリティ名</param>
    /// <returns>アビリティのデータ</returns>
    private Ability_base getAbilityData(string abilityID)
    {
        return this._abilityList.Find(ability => ability.id == abilityID);
    }
}
