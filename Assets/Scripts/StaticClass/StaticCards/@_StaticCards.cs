using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticCards
{
    static public List<CardSO> allCards = new List<CardSO>();

    static public void Init(StaticCardsSO SO)
    {
        allCards = SO.allCards;
    }
}
