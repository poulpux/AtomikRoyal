using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RingGestion))]
public class GameManager : SingletonMother<GameManager>
{
    [SerializeField] private Camera _cam;
    [HideInInspector] public Camera cam { get { return _cam; } private set { _cam = value; } }
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;
    [HideInInspector] public CinemachineVirtualCamera cinemachineCam { get { return _cinemachineCam; } private set { _cinemachineCam = value; } }


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
