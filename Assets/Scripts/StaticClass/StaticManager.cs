using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManager : MonoBehaviour
{
    [Header("Stats")]
    [Space(10)]
    [SerializeField] private StaticRoundsSO roundsSO;
    [SerializeField] private StaticSkinsSO skinsSO;
    [SerializeField] private StaticPlayerSO playerSO;
    [SerializeField] private StaticCardsSO cardsSO;
    private StaticChestSO chestSO;

    void Awake()
    {
        SetGameMod();

        _StaticSkins.Init(skinsSO);
        _StaticChest.Init(chestSO);
        _StaticPlayer.Init(playerSO);
        _StaticCards.Init(cardsSO);
    }

    private void SetGameMod()
    {
        RoundRulesSO gameMod = roundsSO.allGameMods[PlayerPrefs.GetInt("gamemod")];
        TextAsset currentGameModeScript = gameMod.script;
        chestSO = gameMod.chestSO;

        GameManager gameManager = FindObjectOfType<GameManager>();
        GF.SetGameMod<RoundManager>(currentGameModeScript, gameManager.gameObject);
        gameManager.GetRound();
    }
}
