using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "MyScriptable/Create CharData")]
public class CharData : ScriptableObject
{
    public string Name;
    public int maxHp;
    public int maxMp;
    public int STR;
    public int DEF;
    public int SPEED;
    public int INT;
    public int ROLE;

    public List<string> HavingAbility = new List<string>();

}