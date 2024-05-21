using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public RoundManager gameRules;
    public void GetRound()
    {
        gameRules = GetComponent<RoundManager>();
    }
}
