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
    [Header("Cam")]
    [Space(10)]
    [SerializeField] private Camera _cam;
    [HideInInspector] public Camera cam { get { return _cam; } private set { _cam = value; } }
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;
    [HideInInspector] public CinemachineVirtualCamera cinemachineCam { get { return _cinemachineCam; } private set { _cinemachineCam = value; } }


    [HideInInspector] public RoundManagerMother gameRules;
    [HideInInspector] public RingGestion ringGestion;
    [HideInInspector] public PlayerInfos currentPlayer {  get; private set; }

    [Header("Limit Map")]
    [Space(10)]

    [SerializeField] private Transform pilierNorthWest;
    [SerializeField] private Transform pilierSouthEast;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void Init()
    {
        GetRound();
        InstantiateLimitMap();
    }

    public void SetCurrentPlayer(PlayerInfos player)
    {
        currentPlayer = player;
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InstantiateLimitMap()
    {
        int lenght = (int)(-pilierNorthWest.transform.position.x + pilierSouthEast.transform.position.x);
        int height = (int)(pilierNorthWest.transform.position.y - pilierSouthEast.transform.position.y);
        int originX = (int) pilierNorthWest.transform.position.x;
        int originY = (int) pilierSouthEast.transform.position.y;
        _StaticEnvironement.DefineLimiteMap(lenght, height, originX, originY);
    }

    private void GetRound()
    {
        gameRules = GetComponent<RoundManagerMother>();
        ringGestion = GetComponent<RingGestion>();
    }    
}
