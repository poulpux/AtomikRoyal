using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonMother<GameManager>
{
    [HideInInspector] public RoundManagerMother gameRules;
    public void GetRound()
    {
        gameRules = GetComponent<RoundManagerMother>();
    }
}
