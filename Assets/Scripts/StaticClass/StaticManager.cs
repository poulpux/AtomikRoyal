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
    [SerializeField] private StaticMedalsSO medalsSO;
    private StaticChestSO chestSO;

    void Awake()
    {
        SetGameMod();

        _StaticRound.Init(roundsSO.AllGameMods[PlayerPrefs.GetInt("gameMod")]);
        _StaticPhysics.Init(physicsSO); ;
        _StaticSkins.Init(skinsSO);
        _StaticChest.Init(chestSO);
        _StaticPlayer.Init(playerSO);
        _StaticCards.Init(cardsSO);
        _StaticMedals.Init(medalsSO);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void SetGameMod()
    {
        //Get chestSO
        RoundRulesSO gameMod = roundsSO.AllGameMods[PlayerPrefs.GetInt("gamemod")];
        TextAsset currentGameModeScript = gameMod.script;
        chestSO = gameMod.chestSO;

        //Set gameManager
        GameManager gameManager = FindObjectOfType<GameManager>();
        GF.SetScripts<RoundManagerMother>(currentGameModeScript, gameManager.gameObject);
        gameManager.GetRound();
    }
}
