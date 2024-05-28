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
    //[Header("All curves")]
    //[Space(10)]
    public static int maxShield { private set; get; }
    public static UpgradeCurves spd { private set; get; }
    public static UpgradeCurves pvMax { private set; get; }
    public static UpgradeCurves explosionSize { private set; get; }
    public static UpgradeCurves dmgCAC { private set; get; }
    public static UpgradeCurves dmgBomb { private set; get; }
    public static UpgradeCurves cdwThrow { private set; get; }
    public static UpgradeCurves throwForce { private set; get; }
    public static UpgradeCurves range { private set; get; }

    //[Header("GeneralValues")]
    //[Space(10)]
    public static float rangeInteractible { private set; get; }
    public static float timeInteractibleBecomeZero { private set; get; }
    public static float scrollCdwCursor { private set; get; }

    //[Header("Speed")]
    //[Space(10)]
    public static float glueSpdModifier { private set; get; }
    public static float waterSpdModifier { private set; get; }
    public static float deadSpd { private set; get; }
    public static AnimationCurve beginEndMoveCurve { private set; get; }
    public static float beginEndMoveCurveDuration { private set; get; } = 0.1f;

    //[Header("Card")]
    //[Space(10)]
    public static int pieceByKill { private set; get; }
    public static float cdwPiece { private set; get; }
    public static int nbCardInDeck { private set; get; }
    public static int nbCardInHand { private set; get; }

    //[Header("Inventory")]
    //[Space(10)]
    public static int nbCasesInventory { private set; get; }

    //[Header("Ground")]
    //[Space(10)]
    public static float onGroundDrag { private set; get; }
    public static float onGroundImpulseForce { private set; get; }
    public static float chestRadius { private set; get; }


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
        maxShield = SO.maxShield;
        spd = SO.spd;
        pvMax = SO.pvMax;
        explosionSize = SO.explosionSize;
        dmgCAC = SO.dmgCAC;
        dmgBomb = SO.dmgBomb;
        cdwThrow = SO.cdwThrow;
        throwForce = SO.throwForce;
        range = SO.range;
        rangeInteractible = SO.rangeInteractible;
        timeInteractibleBecomeZero = SO.timeInteractibleBecomeZero;
        scrollCdwCursor = SO.scrollCdwCursor;
        glueSpdModifier = SO.glueSpdModifier;
        waterSpdModifier = SO.waterSpdModifier;
        deadSpd = SO.deadSpd;

        beginEndMoveCurve = SO.beginEndMoveCurve;
        beginEndMoveCurveDuration = SO.beginEndMoveCurveDuration;

        pieceByKill = SO.pieceByKill;
        cdwPiece = SO.cdwPiece;
        nbCardInDeck = SO.nbCardInDeck;

        nbCasesInventory = SO.nbCaseInventory;
        nbCardInHand = SO.nbCardInHand;
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
