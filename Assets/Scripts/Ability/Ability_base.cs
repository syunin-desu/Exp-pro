using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create AbilityData")]
public class Ability_base : ScriptableObject
{
    public string Name;
    public string displayName;
    // アビリティのタイプ(Attack, Baff, etc)
    [ValueDropdown("ability_type_list")]
    public string Type;
    // アビリティの速さ(Fast,Normal,Delay)
    [ValueDropdown("ability_speed_list")]
    public string timingType;
    //攻撃範囲(単体、全体)
    [ValueDropdown("ability_range_list")]
    public string Range;
    public int requiredMp;
    //攻撃属性
    [ValueDropdown("ability_Element_list")]
    public string Element;
    public int power;

    //パラメーター設定値
    /// <summary>アビリティのタイプ(Attack, Baff, etc)</summary>
    private static string[] ability_type_list = {
        "Attack", "Buff", "deBuff", "heal"
    };

    /// <summary>アビリティの速さ(Fast,Normal,Delay)</summary>
    private static string[] ability_speed_list = {
        "Fast", "Normal", "Delay",
    };

    /// <summary>範囲(単体、全体)</summary>
    private static string[] ability_range_list = {
        "Single", "All",
    };

    /// <summary>属性</summary>
    private static string[] ability_Element_list = {
        "Fire", "Ice", "Thunder","None"
    };
}
