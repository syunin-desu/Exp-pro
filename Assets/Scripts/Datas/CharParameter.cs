using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

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
    public int INT;
    public int ROLE;
    public int countOfActions;

    // TODO: StringからEnum型にする
    public List<string> HavingAbility = new List<string>();

    public List<CONST.UTILITY.Element> WeakElement = new List<CONST.UTILITY.Element>();
    public List<CONST.UTILITY.Element> StrongElement = new List<CONST.UTILITY.Element>();

}