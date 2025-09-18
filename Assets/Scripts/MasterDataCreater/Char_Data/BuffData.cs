using System.Collections.Generic;
using System;
using UnityEngine;
using CONST;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "CreateData/Create BuffData")]
public class BuffData : ScriptableObject
{
    public string buffName;

    [ValueDropdown("buff_list")]
    public CONST.CHARCTOR.BuffCategory buffCategory;

    public int value_degree;

    public int value_rate;

    [ValueDropdown("buff_period")]
    public CONST.CHARCTOR.EffectPeriod_Category effectPeriod_Category;

    public int effectPeriod;

    /// <summary>
    /// ƒoƒtˆê——
    /// </summary>
    private static List<CONST.CHARCTOR.BuffCategory> buff_list = Enum.GetValues(typeof(CONST.CHARCTOR.BuffCategory))
                                                         .Cast<CONST.CHARCTOR.BuffCategory>()
                                                         .ToList();

    /// <summary>
    /// Œø‰ÊŠúŒÀ
    /// </summary>
    private static List<CONST.CHARCTOR.EffectPeriod_Category> buff_period = Enum.GetValues(typeof(CONST.CHARCTOR.EffectPeriod_Category))
                                                         .Cast<CONST.CHARCTOR.EffectPeriod_Category>()
                                                         .ToList();
}
