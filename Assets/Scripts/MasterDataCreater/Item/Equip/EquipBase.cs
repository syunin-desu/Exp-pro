using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CONST;

public class EquipBase : BaseItemData
{
    public int Attack;

    public int Defence;

    //各パラメータ補正値
    public int addMaxHp;
    public int addMaxMp;
    public int addSTR;
    public int addDEF;
    public int addSPD;
    public int addMagicPower;
    public int addINT;
    public int Kindness;

    //追加効果
    [ValueDropdown("addBuff")]
    public List<CONST.ITEM.ADDBUFF> buff;

    // 追加デバフ
    [ValueDropdown("addDebuff")]
    public List<CONST.ITEM.ADDDEBUFF> debuff;

    public string description_equip;

    [ValueDropdown("elementList")]
    public List<CONST.UTILITY.Element> WeakElement = new List<CONST.UTILITY.Element>();
    [ValueDropdown("elementList")]
    public List<CONST.UTILITY.Element> StrongElement = new List<CONST.UTILITY.Element>();

    private static List<CONST.UTILITY.Element> elementList = Enum.GetValues(typeof(CONST.UTILITY.Element))
                                                             .Cast<CONST.UTILITY.Element>()
                                                             .ToList();


    private static List<CONST.ACTION.TYPE> itemType = Enum.GetValues(typeof(CONST.ACTION.TYPE))
                                                             .Cast<CONST.ACTION.TYPE>()
                                                             .ToList();
    private static List<CONST.ITEM.CATEGORY> ItemCategory = Enum.GetValues(typeof(CONST.ITEM.CATEGORY))
                                                         .Cast<CONST.ITEM.CATEGORY>()
                                                         .ToList();

    private static List<CONST.ACTION.Range> itemRange = Enum.GetValues(typeof(CONST.ACTION.Range))
                                                             .Cast<CONST.ACTION.Range>()
                                                             .ToList();
    private static List<CONST.UTILITY.Element> element = Enum.GetValues(typeof(CONST.UTILITY.Element))
                                                             .Cast<CONST.UTILITY.Element>()
                                                             .ToList();
    private static List<CONST.ITEM.ADDBUFF> addBuff = Enum.GetValues(typeof(CONST.ITEM.ADDDEBUFF))
                                                             .Cast<CONST.ITEM.ADDBUFF>()
                                                             .ToList();
    private static List<CONST.ITEM.ADDDEBUFF> addDebuff = Enum.GetValues(typeof(CONST.ITEM.ADDDEBUFF))
                                                             .Cast<CONST.ITEM.ADDDEBUFF>()
                                                             .ToList();
}
