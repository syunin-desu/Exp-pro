using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using CONST;

[CreateAssetMenu(menuName = "CreateData/Create ItemData")]
public class BaseItemData : ScriptableObject
{
    public string Name;

    public string displayName;

    public string id;

    // レアリティ
    public int Rarity;

    [ValueDropdown("ItemCategory")]
    public CONST.ITEM.CATEGORY category;

    // 買値
    public int PurchasePrice;

    // 売値
    public int Sellingrice;


    public string item_description;

    private static List<CONST.ITEM.CATEGORY> ItemCategory = Enum.GetValues(typeof(CONST.ITEM.CATEGORY))
                                                         .Cast<CONST.ITEM.CATEGORY>()
                                                         .ToList();
}
