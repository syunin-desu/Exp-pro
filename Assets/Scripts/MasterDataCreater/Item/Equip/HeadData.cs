using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateData/Create HeadData")]
public class HeadData : EquipBase
{

    HeadData()
    {
        this.category = CONST.ITEM.CATEGORY.HEAD_EQUIP_ITEM;
    }
}

