using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using CONST;

[CreateAssetMenu(menuName = "CreateData/Create AbilityData")]
public class Ability_base : ScriptableObject
{
    public string Name;
    public string displayName;
    // アビリティのタイプ(Attack, Baff, etc)
    [ValueDropdown("ability_type_list")]
    public CONST.ACTION.TYPE Type;
    // アビリティの速さ(Fast,Normal,Delay)
    [ValueDropdown("ability_speed_list")]
    public CONST.ACTION.Speed timingType;
    //攻撃範囲(単体、全体)
    [ValueDropdown("ability_range_list")]
    public CONST.ACTION.Range Range;
    public int requiredMp;
    //攻撃属性
    [ValueDropdown("ability_Element_list")]
    public CONST.UTILITY.Element Element;
    public int power;

    //パラメーター設定値
    /// <summary>アビリティのタイプ(Attack, Baff, etc)</summary>
    private static CONST.ACTION.TYPE[] ability_type_list = {
        CONST.ACTION.TYPE.Attack,
        CONST.ACTION.TYPE.Buff,
        CONST.ACTION.TYPE.DeBuff,
        CONST.ACTION.TYPE.Heal
    };

    /// <summary>アビリティの速さ(Fast,Normal,Delay)</summary>
    private static CONST.ACTION.Speed[] ability_speed_list = {
        CONST.ACTION.Speed.Fast,
        CONST.ACTION.Speed.Normal,
        CONST.ACTION.Speed.Delay,
    };

    /// <summary>範囲(単体、全体)</summary>
    private static CONST.ACTION.Range[] ability_range_list = {
        CONST.ACTION.Range.Single,
        CONST.ACTION.Range.All,
    };

    /// <summary>属性</summary>
    private static CONST.UTILITY.Element[] ability_Element_list = {
        CONST.UTILITY.Element.Fire,
        CONST.UTILITY.Element.Ice,
        CONST.UTILITY.Element.Thunder,
        CONST.UTILITY.Element.None
    };
}