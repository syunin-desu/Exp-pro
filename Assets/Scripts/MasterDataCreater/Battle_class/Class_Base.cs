using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "CreateData/Create Class_Base")]
public class Class_Base : ScriptableObject
{
    public string passiveAbilityName;

    public string skillAbilityName;

    public string UltimateAbilityName;

    [ValueDropdown("classList")]
    public CONST.CHARCTOR.Class charClass;


    private static List<CONST.CHARCTOR.Class> classList = Enum.GetValues(typeof(CONST.CHARCTOR.Class))
                                                         .Cast<CONST.CHARCTOR.Class>()
                                                         .ToList();
}
