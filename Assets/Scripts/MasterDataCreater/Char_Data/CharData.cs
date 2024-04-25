using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using CONST;

[CreateAssetMenu(menuName = "CreateData/Create CharData")]
public class CharData : ScriptableObject
{
    public string Name;
    public int currentHP;
    public int currentMP;
    public int maxHp;
    public int maxMp;
    public int STR;
    public int DEF;
    public int SPEED;
    public int INT;
    public int ROLE;
    public int countOfActions;

    // TODO: StringからEnum型にする
    public List<string> HavingAbility = new List<string>();

    [ValueDropdown("elementList")]
    public List<CONST.UTILITY.Element> WeakElement = new List<CONST.UTILITY.Element>();
    [ValueDropdown("elementList")]
    public List<CONST.UTILITY.Element> StrongElement = new List<CONST.UTILITY.Element>();

    private static List<CONST.UTILITY.Element> elementList = Enum.GetValues(typeof(CONST.UTILITY.Element))
                                                             .Cast<CONST.UTILITY.Element>()
                                                             .ToList();

}