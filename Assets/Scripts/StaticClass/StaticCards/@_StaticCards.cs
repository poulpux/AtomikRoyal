using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticCards
{
    public static List<CardSO> allCards { get; private set; } = new List<CardSO>();

    static public void Init(StaticCardsSO SO)
    {
        allCards = SO.allCards;
    }
}
