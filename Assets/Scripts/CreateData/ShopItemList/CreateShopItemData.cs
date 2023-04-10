using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using CONST;


/// <summary>
/// ショップのアイテムリストDataを作成するためのスクリプト
/// </summary>
[CreateAssetMenu(menuName = "CreateData/Create ShopItemList")]
public class CreateShopItemData : ScriptableObject
{
    /// <summary>
    /// ショップのアイテムリスト
    /// </summary>
    /// <typeparam name="CONST.ITEM.AllItemNames">アイテム一覧</typeparam>
    /// <returns></returns>
    [ValueDropdown("itemList")]
    public List<CONST.ITEM.AllItemNames> ShopSellingItemsList = new List<CONST.ITEM.AllItemNames>();
    private static List<CONST.ITEM.AllItemNames> itemList = Enum.GetValues(typeof(CONST.ITEM.AllItemNames))
                                                             .Cast<CONST.ITEM.AllItemNames>()
                                                             .ToList();
}
