using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RARITY
{
    COMMUN,
    RARE,
    LEGENDARY
}

public static class StaticChest
{
    static List<Usable> ToFindInChest = new List<Usable>();
    static int communLootPct, rareLootPct, legendaryLootPct;
    static int minGoldInChest, maxGoldInChest;

    static void OpenChest(Vector3 position)
    {
        //TODU
    }
}
