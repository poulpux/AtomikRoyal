using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticMedals
{
    public static List<MedalsSO> allMedals { get; private set; } = new List<MedalsSO>();

    static public void Init(StaticMedalsSO SO)
    {
        allMedals = SO.allMedals;
    }
}
