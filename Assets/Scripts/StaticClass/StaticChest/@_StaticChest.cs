using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RARITY
{
    COMMUN,
    RARE,
    LEGENDARY
}

public static class _StaticChest
{
    static public List<UtilitySO> ToFindInChestUtility = new List<UtilitySO>();
    static public List<BombUsableSO> ToFindInChestBomb = new List<BombUsableSO>();
    //static public int communLootPct, rareLootPct, legendaryLootPct;
    static public int minGoldInChest, maxGoldInChest, chestDropRate;
    static public List<Vector2> allChestPos = new List<Vector2>();
    static public GameObject objectOnGroundPrefab;
    static public int nbUsable, nbBomb;

    static private int communLootPctUsable, rareLootPctUsable, legendaryLootPctUsable;
    static private int communLootPctBomb, rareLootPctBomb, legendaryLootPctBomb;
    static public void OpenChest(Vector3 position)
    {
        //TODU
        GameObject objet = Object.Instantiate(objectOnGroundPrefab);
        objet.GetComponent<OnGroundItem>().Init(ToFindInChestUtility[0]);
        objet.transform.position = position;
    }

    static public void Init(StaticChestSO SO)
    {
        if (SO == null)
        {
            Debug.LogError("No SO");
            return;
        }

        ToFindInChestUtility = SO.ToFindInChestUsable;
        ToFindInChestBomb = SO.ToFindInChestBomb;
        minGoldInChest = SO.minGoldInChest;
        maxGoldInChest = SO.maxGoldInChest;
        chestDropRate = SO.chestDropRate;
        objectOnGroundPrefab = SO.objectOnGroundPrefab;
        nbUsable = SO.nbUsable;
        nbBomb = SO.nbBomb;

        legendaryLootPctUsable = ToFindInChestUtility.Any(usable => usable.rarity == RARITY.COMMUN) ? SO.communLootPct : 0;
        legendaryLootPctBomb = ToFindInChestBomb.Any(usable => usable.rarity == RARITY.COMMUN) ? SO.communLootPct : 0;

        legendaryLootPctUsable = ToFindInChestUtility.Any(usable => usable.rarity == RARITY.RARE) ? SO.rareLootPct : 0;
        legendaryLootPctBomb = ToFindInChestBomb.Any(usable => usable.rarity == RARITY.RARE) ? SO.rareLootPct : 0;
        
        legendaryLootPctUsable = ToFindInChestUtility.Any(usable => usable.rarity == RARITY.LEGENDARY) ? SO.legendaryLootPct : 0;
        legendaryLootPctBomb = ToFindInChestBomb.Any(usable => usable.rarity == RARITY.LEGENDARY) ? SO.legendaryLootPct : 0;
    }
}
