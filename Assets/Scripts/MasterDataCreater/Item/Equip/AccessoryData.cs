using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using CONST;

[CreateAssetMenu(menuName = "CreateData/Create AccessoryData")]
public class AccessoryData : EquipBase
{

    AccessoryData()
    {
        this.category = CONST.ITEM.CATEGORY.ACCESSORY_ITEM;
    }
}
