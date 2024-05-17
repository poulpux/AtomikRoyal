using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManager : MonoBehaviour
{
    [SerializeField] private StaticRoundsSO roundsSO;
    [SerializeField] private StaticSkinsSO skinsSO;
    [SerializeField] private StaticPlayerSO playerSO;
    [SerializeField] private StaticCardsSO cardsSO;
    private StaticChestSO chestSO;
    void Awake()
    {
        GetGameMod();

        _StaticSkins.Init(skinsSO);
        _StaticChest.Init(chestSO);
        _StaticPlayer.Init(playerSO);
        _StaticCards.Init(cardsSO);
    }

    private void GetGameMod()
    {
        GameModSO gameMod = roundsSO.allGameMods[PlayerPrefs.GetInt("gamemod")];
        TextAsset currentGameModeScript = gameMod.script;
        GF.SetGameMod<RoundManager>(currentGameModeScript, GameManager.Instance.gameObject);
        chestSO = gameMod.chestSO;
    }
}
