using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticChestSO", menuName = "Static/StaticChestSO")]
public class StaticChestSO : ScriptableObject
{
    [Header("All usable")]
    [Space(10)]
    public List<UsableSO> ToFindInChest = new List<UsableSO>();
    [Header("Drop Pourcentage between 0 and 100")]
    [Space(10)]
    public int communLootPct;
    public int rareLootPct, legendaryLootPct, chestDropRate;
    [Header("Coins earnin")]
    [Space(10)]
    public int minGoldInChest;
    public int maxGoldInChest;
}
