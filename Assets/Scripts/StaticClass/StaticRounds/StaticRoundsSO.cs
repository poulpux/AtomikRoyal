using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticRoundsSO", menuName = "Static/StaticRoundsSO")]
public class StaticRoundsSO : ScriptableObject
{
    [Header("All GameMods")]
    [Space(10)]
    public List<RoundRulesSO> allGameMods = new List<RoundRulesSO>();
}
