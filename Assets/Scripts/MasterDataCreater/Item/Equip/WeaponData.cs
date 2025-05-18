using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using CONST;

[CreateAssetMenu(menuName = "CreateData/Create WeaponData")]
public class WeaponData : EquipBase
{

    WeaponData()
    {
        this.category = CONST.ITEM.CATEGORY.WEAPON_ITEM;
    }
}
