using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticChestSO", menuName = "Static/StaticChestSO")]
public class StaticChestSO : ScriptableObject
{
    [Header("All usable")]
    [Space(10)]
    public List<UtilityUsableSO> ToFindInChestUtility = new List<UtilityUsableSO>();
    public List<BombUsableSO> ToFindInChestBomb = new List<BombUsableSO>();

    [Header("Drop Pourcentage between 0 and 100")]
    [Space(10)]
    public int communLootPct;
    public int rareLootPct, legendaryLootPct, chestDropRate;

    [Header("Nb earning")]
    [Space(10)]
    public int minGoldInChest;
    public int maxGoldInChest, nbUtilty, nbBomb;

    [Header("Positions")]
    [Space(10)]
    public List<Vector2> allChestPos = new List<Vector2>();
    
    [Header("ObjectOnGround")]
    [Space(10)]
    public GameObject objectOnGroundPrefab;
    public GameObject coinsOnGroundPrefab;
}
