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
    [SerializeField] private StaticPhysicsSO physicsSO;
    private StaticChestSO chestSO;

    void Awake()
    {
        SetGameMod();

        _StaticPhysics.Init(physicsSO);
        _StaticSkins.Init(skinsSO);
        _StaticChest.Init(chestSO);
        _StaticPlayer.Init(playerSO);
        _StaticCards.Init(cardsSO);
    }

    private void SetGameMod()
    {
        //Get chestSO
        RoundRulesSO gameMod = roundsSO.allGameMods[PlayerPrefs.GetInt("gamemod")];
        TextAsset currentGameModeScript = gameMod.script;
        chestSO = gameMod.chestSO;

        //Set gameManager
        GameManager gameManager = FindObjectOfType<GameManager>();
        GF.SetScripts<RoundManager>(currentGameModeScript, gameManager.gameObject);
        gameManager.GetRound();
    }
}
