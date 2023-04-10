using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using System.Linq;
using CONST;

[CreateAssetMenu(menuName = "CreateData/Create ItemData")]
public class createItemData : ScriptableObject
{
    public string Name;

    public string displayName;

    [ValueDropdown("itemType")]

    public CONST.ACTION.TYPE Type;

    [ValueDropdown("itemRange")]
    public CONST.ACTION.Range Range;

    [ValueDropdown("element")]
    public CONST.UTILITY.Element Element;

    public int value;

    public int price;


    private static List<CONST.ACTION.TYPE> itemType = Enum.GetValues(typeof(CONST.ACTION.TYPE))
                                                             .Cast<CONST.ACTION.TYPE>()
                                                             .ToList();

    private static List<CONST.ACTION.Range> itemRange = Enum.GetValues(typeof(CONST.ACTION.Range))
                                                             .Cast<CONST.ACTION.Range>()
                                                             .ToList();
    private static List<CONST.UTILITY.Element> element = Enum.GetValues(typeof(CONST.UTILITY.Element))
                                                             .Cast<CONST.UTILITY.Element>()
                                                             .ToList();
}
