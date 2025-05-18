using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;


public class PlayerEquipData
{
    public WeaponData weaponData;
    public HeadData armedHead;
    public BodyData armedBody;
    public AccessoryData armedAccessory_1;
    public AccessoryData armedAccessory_2;
}

/// <summary>
/// 内部用キャラのパラメータをまとめたクラス
/// </summary>
public class CharParameter
{
    public string Name;
    public int currentHP;
    public int currentMP;
    public int maxHp;
    public int maxMp;
    public int STR;
    public int DEF;
    public int SPEED;
    public int MGC; // 魔力
    public int INT; // 知性
    public int KID; //慈愛
    public int ROLE;
    public int countOfActions;

    public PlayerEquipData equipDatas = new PlayerEquipData();

    // TODO: StringからEnum型にする
    public List<Ability_base> HavingAbility = new List<Ability_base>();

    public List<CONST.UTILITY.Element> WeakElement = new List<CONST.UTILITY.Element>();
    public List<CONST.UTILITY.Element> StrongElement = new List<CONST.UTILITY.Element>();

}