using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RingGestion))]
public class GameManager : SingletonMother<GameManager>
{
    [HideInInspector] public RoundManagerMother gameRules;
    [HideInInspector] public RingGestion ringGestion;
    [HideInInspector] public PlayerInfos currentPlayer {  get; private set; }

    public void GetRound()
    {
        gameRules = GetComponent<RoundManagerMother>();
        ringGestion = GetComponent<RingGestion>();
    }

    public void SetCurrentPlayer(PlayerInfos player)
    {
        currentPlayer = player;
    }

    
}
