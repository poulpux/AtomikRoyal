using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RARITY
{
    COMMUN,
    RARE,
    LEGENDARY
}

public static class _StaticChest
{
    static public List<UsableSO> ToFindInChest = new List<UsableSO>();
    static public int communLootPct, rareLootPct, legendaryLootPct;
    static public int minGoldInChest, maxGoldInChest;

    static void OpenChest(Vector3 position)
    {
        //TODU
    }

    static public void Init(StaticChestSO SO)
    {
        ToFindInChest = SO.ToFindInChest;
        communLootPct = SO.communLootPct;
        rareLootPct = SO.rareLootPct;
        legendaryLootPct = SO.legendaryLootPct;
        minGoldInChest = SO.minGoldInChest;
        maxGoldInChest= SO.maxGoldInChest;
    }
}
