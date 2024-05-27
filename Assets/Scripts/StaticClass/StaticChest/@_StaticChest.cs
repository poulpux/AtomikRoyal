using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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
    static public int minGoldInChest, maxGoldInChest, chestDropRate;
    static public List<Vector2> allChestPos = new List<Vector2>();
    static public GameObject objectOnGroundPrefab, coinsOnGroundPrefab;
    static public int nbUtility, nbBomb, nbCoins;

    static private int communLootPctUtility, rareLootPctUtility, legendaryLootPctUtility;
    static private int communLootPctBomb, rareLootPctBomb, legendaryLootPctBomb;
    static public void OpenChest(Vector3 position)
    {
        List<UsableSO> list = RandomUsable();

        foreach (var item in list)
            InstantiateUsable(item, position);

        for (int i = 0; i < nbCoins; i++)
            InstantiateCoin(position);
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
        nbCoins = SO.nbCoins;

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

    static private Vector2 RandomSpawnPos(Vector2 chestPos)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized * _StaticPhysics.chestRadius;//radius
        Vector2 teleportPosition = new Vector2(randomDirection.x, randomDirection.y) + chestPos;

        return teleportPosition;
    }

    static private void InstantiateUsable(UsableSO model, Vector2 position)
    {
        GameObject objet = Object.Instantiate(objectOnGroundPrefab);
        objet.GetComponent<OnGroundItem>().Init(model);
        objet.transform.position = RandomSpawnPos(position);
        objet.GetComponent<OnGroundPhysics>().Init(position);
        
        UsableOnGround onGround = objet.GetComponent<UsableOnGround>();
        onGround.SO = model;
    }

    static private void InstantiateCoin(Vector2 position)
    {
        GameObject coins = Object.Instantiate(coinsOnGroundPrefab);
        coins.GetComponent<OnGroundCoins>().nb = Random.Range(minGoldInChest, maxGoldInChest);
        coins.transform.position = RandomSpawnPos(position);
        coins.GetComponent<OnGroundPhysics>().Init(position);
    }
}
