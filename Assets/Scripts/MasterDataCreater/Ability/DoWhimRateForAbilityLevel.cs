using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateData/DoWhimRateForAbilityLevel")]
public class DoWhimRateForAbilityLevel : ScriptableObject
{
    public int abilityLevel;
    public float whimRate;
}
