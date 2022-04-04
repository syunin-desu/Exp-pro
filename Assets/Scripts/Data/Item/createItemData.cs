using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using CONST;

[CreateAssetMenu(menuName = "MyScriptable/Create ItemData")]
public class createItemData : ScriptableObject
{
    public string Name;

    public string displayName;

    [ValueDropdown("itemType")]

    public string Type;

    [ValueDropdown("itemRange")]
    public string Range;

    [ValueDropdown("element")]
    public string Element;

    public int power;


    private static string[] itemType = CONST.UTILITY.ACTION_TYPE_LIST;

    private static string[] itemRange = CONST.UTILITY.RANGE;
    private static string[] element = CONST.UTILITY.ELEMENT;
}
