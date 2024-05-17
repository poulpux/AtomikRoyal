using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public RoundManager gameRules;

    public void GetRound()
    {
        gameRules = GetComponent<RoundManager>();
    }
}
