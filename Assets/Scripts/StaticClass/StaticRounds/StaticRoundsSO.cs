using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticRoundsSO", menuName = "Static/StaticRoundsSO")]
public class StaticRoundsSO : ScriptableObjectWithScript
{
    [Header("All GameMods")]
    [Space(10)]
    public List<GameModSO> allGameMods = new List<GameModSO>();
}
