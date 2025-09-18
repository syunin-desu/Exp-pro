using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using CONST;

[CreateAssetMenu(menuName = "CreateData/Create CharData")]
[System.Serializable]
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
    public int MagicPower;
    public int INT;
    public int Kindness;
    public CONST.CHARCTOR.Role ROLE;
    public int countOfActions;
    [ValueDropdown("classList")]
    public CONST.CHARCTOR.Class charClass;

    // TODO: StringからEnum型にする
    public List<Ability_base> HavingAbility = new List<Ability_base>();

    public WeaponData weaponData;
    public HeadData armedHead;
    public BodyData armedBody;
    public AccessoryData armedAccessory_1;
    public AccessoryData armedAccessory_2;

    [ValueDropdown("elementList")]
    public List<CONST.UTILITY.Element> WeakElement = new List<CONST.UTILITY.Element>();
    [ValueDropdown("elementList")]
    public List<CONST.UTILITY.Element> StrongElement = new List<CONST.UTILITY.Element>();

    private static List<CONST.UTILITY.Element> elementList = Enum.GetValues(typeof(CONST.UTILITY.Element))
                                                             .Cast<CONST.UTILITY.Element>()
                                                             .ToList();
    private static List<CONST.CHARCTOR.Class> classList = Enum.GetValues(typeof(CONST.CHARCTOR.Class))
                                                             .Cast<CONST.CHARCTOR.Class>()
                                                             .ToList();

}