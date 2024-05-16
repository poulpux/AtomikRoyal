using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticChestSO", menuName = "Static/StaticChestSO")]
public class StaticChestSO : ScriptableObject
{
    [Header("List of usable in chest")]
    [Space(10)]
    public List<UsableSO> ToFindInChest = new List<UsableSO>();
    [Header("Drop Pourcentage")]
    [Space(10)]
    public int communLootPct;
    public int rareLootPct, legendaryLootPct;
    [Header("Coins earnin")]
    [Space(10)]
    public int minGoldInChest;
    public int maxGoldInChest;
}
