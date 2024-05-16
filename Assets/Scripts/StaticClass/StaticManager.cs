using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManager : MonoBehaviour
{
    [SerializeField] private StaticSkinsSO skinsSO;
    [SerializeField] private StaticChestSO chestSO;
    [SerializeField] private StaticPlayerSO playerSO;
    [SerializeField] private StaticCardsSO cardsSO;
    void Awake()
    {
        _StaticSkins.Init(skinsSO);
        _StaticChest.Init(chestSO);
        _StaticPlayer.Init(playerSO);
        _StaticCards.Init(cardsSO);

        print(_StaticChest.communLootPct);
    }
}
