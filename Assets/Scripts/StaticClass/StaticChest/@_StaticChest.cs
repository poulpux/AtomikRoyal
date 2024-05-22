using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public enum RARITY
{
    COMMUN,
    RARE,
    LEGENDARY
}

public static class _StaticChest
{
    static public List<UtilityUsableSO> ToFindInChestUtility = new List<UtilityUsableSO>();
    static public List<BombUsableSO> ToFindInChestBomb = new List<BombUsableSO>();
    //static public int communLootPct, rareLootPct, legendaryLootPct;
    static public int minGoldInChest, maxGoldInChest, chestDropRate;
    static public List<Vector2> allChestPos = new List<Vector2>();
    static public GameObject objectOnGroundPrefab, coinsOnGroundPrefab;
    static public int nbUtility, nbBomb;

    static private int communLootPctUtility, rareLootPctUtility, legendaryLootPctUtility;
    static private int communLootPctBomb, rareLootPctBomb, legendaryLootPctBomb;
    static public void OpenChest(Vector3 position)
    {
        List<UsableSO> list = RandomUsable();

        foreach (var item in list)
        {
            GameObject objet = Object.Instantiate(objectOnGroundPrefab);
            objet.GetComponent<OnGroundItem>().Init(item);
            //TODO : faire spawn les items sur une position aléatoire
            objet.transform.position = position;
        }

        GameObject coins = Object.Instantiate(coinsOnGroundPrefab);
        coins.GetComponent<OnGroundCoins>().nb = Random.Range(minGoldInChest, maxGoldInChest);
        //TODO : faire spawn les items sur une position aléatoire
        coins.transform.position = position;
    }

    static public void Init(StaticChestSO SO)
    {
        if (SO == null)
        {
            Debug.LogError("No SO");
            return;
        }

        ToFindInChestUtility = SO.ToFindInChestUtility;
        ToFindInChestBomb = SO.ToFindInChestBomb;
        minGoldInChest = SO.minGoldInChest;
        maxGoldInChest = SO.maxGoldInChest;
        chestDropRate = SO.chestDropRate;
        objectOnGroundPrefab = SO.objectOnGroundPrefab;
        coinsOnGroundPrefab = SO.coinsOnGroundPrefab;
        nbUtility = SO.nbUtilty;
        nbBomb = SO.nbBomb;

        communLootPctUtility = ToFindInChestUtility.Any(usable => usable.rarity == RARITY.COMMUN) ? SO.communLootPct : 0;
        communLootPctBomb = ToFindInChestBomb.Any(usable => usable.rarity == RARITY.COMMUN) ? SO.communLootPct : 0;

        rareLootPctUtility = ToFindInChestUtility.Any(usable => usable.rarity == RARITY.RARE) ? SO.rareLootPct : 0;
        rareLootPctBomb = ToFindInChestBomb.Any(usable => usable.rarity == RARITY.RARE) ? SO.rareLootPct : 0;
        
        legendaryLootPctUtility = ToFindInChestUtility.Any(usable => usable.rarity == RARITY.LEGENDARY) ? SO.legendaryLootPct : 0;
        legendaryLootPctBomb = ToFindInChestBomb.Any(usable => usable.rarity == RARITY.LEGENDARY) ? SO.legendaryLootPct : 0;
    }

    static private List<UsableSO> RandomUsable()
    {
        List<UsableSO> list = new List<UsableSO>();
        for (int i = 0; i < nbUtility ; i++)
        {
            RARITY rarity = GetRandomRarity(true);
            list.Add( GetRandomUtility(rarity));
        }

        for (int i = 0; i < nbBomb; i++)
        {
            RARITY rarity = GetRandomRarity(false);
            list.Add(GetRandomBomb(rarity));
        }

        return list;
    }

    static private RARITY GetRandomRarity(bool utility)
    {
        int totalDropRate = utility ? communLootPctUtility + rareLootPctUtility + legendaryLootPctUtility : communLootPctBomb + rareLootPctBomb + legendaryLootPctBomb;
        int random = Random.Range(0, totalDropRate);
        if (random <= (utility ? communLootPctUtility : communLootPctBomb))
            return RARITY.COMMUN;
        else if (random <= (utility ? communLootPctBomb + rareLootPctUtility : communLootPctBomb + rareLootPctBomb))
            return RARITY.RARE;
        else
            return RARITY.LEGENDARY;
    }

    static private UsableSO GetRandomUtility(RARITY rarity)
    {
        List<UtilityUsableSO> filteredList = ToFindInChestUtility.Where(usable => usable.rarity == rarity).ToList();
        if (filteredList.Count == 0)
            return null;

        int randomIndex = Random.Range(0, filteredList.Count);
        return filteredList[randomIndex];
    }
    
    static private BombUsableSO GetRandomBomb(RARITY rarity)
    {
        List<BombUsableSO> filteredList = ToFindInChestBomb.Where(usable => usable.rarity == rarity).ToList();
        if (filteredList.Count == 0)
            return null;

        int randomIndex = Random.Range(0, filteredList.Count);
        return filteredList[randomIndex];
    }
}
