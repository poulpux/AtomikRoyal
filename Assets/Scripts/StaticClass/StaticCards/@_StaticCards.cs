using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticCards
{
    static public List<UsableSO> allCards = new List<UsableSO>();

    static public void Init(StaticCardsSO SO)
    {
        allCards = SO.allCards;
    }
}
