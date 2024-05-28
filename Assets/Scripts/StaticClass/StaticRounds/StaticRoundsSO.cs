using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticRoundsSO", menuName = "Static/StaticRoundsSO")]
public class StaticRoundsSO : ScriptableObject
{
    [Header("All GameMods")]
    [Space(10)]
    private List<RoundRulesSO> _allGameMods = new List<RoundRulesSO>();
    public IReadOnlyList<RoundRulesSO> AllGameMods => _allGameMods.AsReadOnly();
}
