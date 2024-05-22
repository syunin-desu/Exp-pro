using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using System.Linq;
using CONST;

[CreateAssetMenu(menuName = "CreateData/Create ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;

    public string displayName;

    [ValueDropdown("itemType")]

    public CONST.ACTION.TYPE Type;

    // アイテム実行自体がFAなので、アイテム同じFAアクションの中で優先順位付けをする
    // 行動速度値(低いほど実行が早い)
    public int speed_rank;

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
