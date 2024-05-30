using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticCards
{
    private static List<CardSO> _allCards = new List<CardSO>();
    public static IReadOnlyList<CardSO> allCards => _allCards.AsReadOnly();

    static public void Init(StaticCardsSO SO)
    {
        _allCards = SO.allCards;
    }
}
