using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ability関係を管理
public class AbilityManager : MonoBehaviour
{
    private List<Ability_base> ability_list = new List<Ability_base>();

    // Start is called before the first frame update
    void Start()
    {
        Object[] Ability_objects = Resources.LoadAll("Data/ability/", typeof(Ability_base));

        foreach (Ability_base ability in Ability_objects)
        {
            ability_list.Add(ability);

        }
    }

#nullable enable
    public void execAbility(CharBase performChar, CharBase? targetChar, string execAbilityName)
    {
        switch (execAbilityName)
        {
            case "burn":
                this.burn(performChar, targetChar, this.getAbilityData(execAbilityName));
                break;
            case "chill":
                this.chill(performChar, targetChar, this.getAbilityData(execAbilityName));
                break;
            case "volt":
                this.volt(performChar, targetChar, this.getAbilityData(execAbilityName));
                break;
            default:
                Debug.Log("アビリティデータに登録されていないアビリティが指定されました");
                break;
        }
    }
#nullable disable

    //======================================
    // アビリティの内容
    //======================================

    /// <summary>
    /// バーン(炎属性 魔法攻撃)
    /// </summary>
    /// <param name="performChar">実行キャラ</param>
    /// <param name="targetChar">対象キャラ</param>
    private void burn(CharBase performChar, CharBase targetChar, Ability_base execAbilityData)
    {

        int performerInt = performChar.intelligence;
        int power = execAbilityData.power;
        string Element = execAbilityData.Element;

        // TODO ダメージ計算用のファンクションを別のクラスで作成し、そこで行えるようにする
        float elementDamageRate = this.calcElementDamageRate(Element, targetChar);

        targetChar.Damage((int)((performerInt * power) * elementDamageRate));
    }

    /// <summary>
    /// チル(氷属性 魔法攻撃)
    /// </summary>
    /// <param name="performChar">実行キャラ</param>
    /// <param name="targetChar">対象キャラ</param>
    /// <param name="execAbilityData">実行するアビリティのデータ</param>
    private void chill(CharBase performChar, CharBase targetChar, Ability_base execAbilityData)
    {
        int performerInt = performChar.intelligence;
        int power = execAbilityData.power;
        string Element = execAbilityData.Element;

        // TODO ダメージ計算用のファンクションを別のクラスで作成し、そこで行えるようにする
        float elementDamageRate = this.calcElementDamageRate(Element, targetChar);

        targetChar.Damage((int)((performerInt * power) * elementDamageRate));
    }

    /// <summary>
    /// ボルト (雷属性 魔法攻撃)
    /// </summary>
    /// <param name="performChar">実行キャラ</param>
    /// <param name="targetChar">対象キャラ</param>
    /// <param name="execAbilityData">実行するアビリティのデータ</param>
    private void volt(CharBase performChar, CharBase targetChar, Ability_base execAbilityData)
    {
        int performerInt = performChar.intelligence;
        int power = execAbilityData.power;
        string Element = execAbilityData.Element;

        // TODO ダメージ計算用のファンクションを別のクラスで作成し、そこで行えるようにする
        float elementDamageRate = this.calcElementDamageRate(Element, targetChar);

        targetChar.Damage((int)((performerInt * power) * elementDamageRate));
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
        Ability_base abilityDisplayName = this.ability_list.Find(ability => ability.Name == abilityName);
        return abilityDisplayName.displayName;
    }

    /// <summary>
    /// 表示アビリティ名からアビリティ名を取得する
    /// </summary>
    /// <param name="ability_DisplayName">表示アビリティ名</param>
    /// <returns>アビリティ名</returns>
    public string getAbilityNameForDisplayName(string ability_DisplayName)
    {
        Ability_base abilityName = this.ability_list.Find(ability => ability.displayName == ability_DisplayName);
        return abilityName.Name;
    }

    /// <summary>
    /// アビリティ名からアビリティのアクションタイミングを取得
    /// </summary>
    /// <param name="abilityName">アビリティ名</param>
    /// <returns>アビリティタイミング</returns>
    public string getAbilityTimingType(string abilityName)
    {
        Ability_base ability = this.ability_list.Find(ability => ability.Name == abilityName);
        return ability != null ? ability.timingType : "";
    }

    /// <summary>
    /// アビリティのデータを取得
    /// </summary>
    /// <param name="abilityName">アビリティ名</param>
    /// <returns>アビリティのデータ</returns>
    private Ability_base getAbilityData(string abilityName)
    {
        return this.ability_list.Find(ability => ability.Name == abilityName);
    }

    /// <summary>
    /// 属性によるダメージ率の増減を計算し返す
    /// </summary>
    /// <param name="weakElement">弱点属性</param>
    /// <param name="strongElement">耐性属性</param>
    /// <param name="targetChar">対象のキャラ</param>
    /// <returns>ダメージ率</returns>
    private float calcElementDamageRate(string Element, CharBase targetChar)
    {
        bool haveTargetCharWeakElement = targetChar.weakElement.Contains(Element);
        bool haveTargetCharStrongElement = targetChar.strongElement.Contains(Element);
        float elementDamageRate = 1;
        if (haveTargetCharWeakElement)
        {
            elementDamageRate = haveTargetCharWeakElement ? CONST.BATTLE_RATE.RATE_WEAK_ELEMENT : 1;
        }
        else if (haveTargetCharStrongElement)
        {
            elementDamageRate = haveTargetCharStrongElement ? CONST.BATTLE_RATE.RATE_STRONG_ELEMENT : 1;

        }
        return elementDamageRate;
    }
}
