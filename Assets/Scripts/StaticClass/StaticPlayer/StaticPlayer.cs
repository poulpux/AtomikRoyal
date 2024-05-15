using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum PLAYERSTATS
{
    SPD,
    PVMAX,
    EXPLOSIONSIZE,
    DMGCAC,
    DMGEXPLOSION,
    COOLDOWNTHROW,
    THROWFORCE,
    RANGE
}

public static class StaticPlayer 
{
    [Header("All curves")]
    [Space(10)]
    static AnimatingCurve spdCostCurve;
    static AnimatingCurve pvMaxCostCurve;
    static AnimatingCurve explosionSizeCostCurve;
    static AnimatingCurve dmgCaCCostCurve;
    static AnimatingCurve dmgExplosionCostCurve;
    static AnimatingCurve cooldownThrowCostCurve;
    static AnimatingCurve grenadeSpdCost;
    static AnimatingCurve rangeCostCurve;

    [Header("GeneralValues")]
    [Space(10)]
    static float rangeInteractible;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    static int GetPrice(PLAYERSTATS stats, int currentNbUpgrade)
    {
        //TODO
        return 0;
    }
    
    static float GetValue(PLAYERSTATS stats, int currentNbUpgrade)
    {
        //TODO
        return 0f;
    }


}
