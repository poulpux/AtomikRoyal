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
    DMGBOMB,
    COOLDOWNTHROW,
    THROWFORCE,
    RANGE
}

public static class _StaticPlayer 
{
    [Header("All curves")]
    [Space(10)]
    static public AnimationCurve spdCostCurve;
    static public AnimationCurve pvMaxCostCurve;
    static public AnimationCurve explosionSizeCostCurve;
    static public AnimationCurve dmgCaCCostCurve;
    static public AnimationCurve dmgExplosionCostCurve;
    static public AnimationCurve cooldownThrowCostCurve;
    static public AnimationCurve grenadeSpdCost;
    static public AnimationCurve rangeCostCurve;

    [Header("GeneralValues")]
    [Space(10)]
    static public float rangeInteractible;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    static public int GetPrice(PLAYERSTATS stats, int currentNbUpgrade)
    {
        //TODO
        return 0;
    }
    
    static public float GetValue(PLAYERSTATS stats, int currentNbUpgrade)
    {
        //TODO
        return 0f;
    }


    static public void Init(StaticPlayerSO SO)
    {
        spdCostCurve = SO.spdCostCurve;
        pvMaxCostCurve = SO.pvMaxCostCurve;
        explosionSizeCostCurve = SO.explosionSizeCostCurve;
        dmgCaCCostCurve = SO.dmgCaCCostCurve;
        dmgExplosionCostCurve = SO.dmgExplosionCostCurve;
        cooldownThrowCostCurve = SO.cooldownThrowCostCurve;
        grenadeSpdCost = SO.grenadeSpdCost;
        rangeCostCurve = SO.rangeCostCurve;
    }
}
