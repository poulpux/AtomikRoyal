using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundRulesSO_filename", menuName = "SO/RoundRulesSO")]
public class RoundRulesSO : ScriptableObjectWithScript
{
    [Header("TEAM")]
    [Space(10)]
    public GAMEMODE gameMode;
    [Header("Chest")]
    [Space(10)]
    public StaticChestSO chestSO;
}
