using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using CONST;

[CreateAssetMenu(menuName = "CreateData/Create BodyData")]
public class BodyData : EquipBase
{
    BodyData()
    {
        this.category = CONST.ITEM.CATEGORY.BODY_EQUIP_ITEM;
    }
}
