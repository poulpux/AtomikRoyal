using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static StaticPlayerSO;

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
    //Courbe "nbAmélioration/prix"
    static public UpgradeCurves spd;
    static public UpgradeCurves pvMax;
    static public UpgradeCurves explosionSize;
    static public UpgradeCurves dmgCAC;
    static public UpgradeCurves dmgBomb;
    static public UpgradeCurves cdwThrow;
    static public UpgradeCurves throwForce;
    static public UpgradeCurves range;

    [Header("GeneralValues")]
    [Space(10)]
    static public float rangeInteractible;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    static public int GetPrice(PLAYERSTATS stat, int currentNbUpgrade)
    {
        return (int)GetUpgradeCurve(stat).CostCurve.Evaluate((float)currentNbUpgrade+1f);
    }
    
    static public float GetValue(PLAYERSTATS stat, int currentNbUpgrade)
    {
        UpgradeCurves currentCurve = GetUpgradeCurve(stat);
        return currentCurve.startValue + currentCurve.statsParLv * (float)currentNbUpgrade;
    }


    static public void Init(StaticPlayerSO SO)
    {
        spd = SO.spd;
        pvMax = SO.pvMax;
        explosionSize = SO.explosionSize;
        dmgCAC = SO.dmgCAC;
        dmgBomb = SO.dmgBomb;
        cdwThrow = SO.cdwThrow;
        throwForce = SO.throwForce;
        range = SO.range;
    }

    static private UpgradeCurves GetUpgradeCurve(PLAYERSTATS stat)
    {
        if (stat == PLAYERSTATS.SPD)
            return spd;
        else if(stat == PLAYERSTATS.PVMAX)
            return pvMax;
        else if(stat == PLAYERSTATS.EXPLOSIONSIZE)
            return explosionSize;
        else if(stat == PLAYERSTATS.DMGCAC)
            return dmgCAC;
        else if(stat == PLAYERSTATS.DMGBOMB)
            return dmgBomb;
        else if(stat == PLAYERSTATS.COOLDOWNTHROW)
            return cdwThrow;
        else if(stat == PLAYERSTATS.THROWFORCE)
            return throwForce;
        else if(stat == PLAYERSTATS.RANGE)
            return range;

        return null; 
    }
}
