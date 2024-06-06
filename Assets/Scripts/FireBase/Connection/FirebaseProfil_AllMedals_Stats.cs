using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FirebaseProfil_AllMedals_Stats
{
    public string pseudo;
    public int minutePlayed;

    public void AddValues(FirebaseProfil_AllMedals_Stats stats)
    {
        minutePlayed += stats.minutePlayed;
    }
}
