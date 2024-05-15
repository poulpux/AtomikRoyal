using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticChestSO", menuName = "SO/StaticChestSO")]
public class StaticChestSO : ScriptableObject
{
    [Header("List of usable in chest")]
    [Space(10)]
    public List<Usable> ToFindInChest = new List<Usable>();
    [Header("Drop Pourcentage")]
    [Space(10)]
    public int communLootPct;
    public int rareLootPct, legendaryLootPct;
    [Header("Coins earnin")]
    [Space(10)]
    public int minGoldInChest;
    public int maxGoldInChest;
}
