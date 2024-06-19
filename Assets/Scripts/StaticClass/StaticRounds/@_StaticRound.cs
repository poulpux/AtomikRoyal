using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public enum RINGNAME
{
    UPLEFT,
    UPMIDDLE,
    UPRIGHT,
    MIDDLELEFT,
    MIDDLE,
    MIDDLERIGHT,
    DOWNLEFT,
    DOWNMIDDLE,
    DOWNRIGHT,
}

public enum RINGSTATE
{
    FREE,
    WILLCLOSE,
    CLOSE
}

public class RingZone
{
    public RINGNAME name;
    public RINGSTATE state;
}

public static class _StaticRound 
{
    //[Header("TEAM")]
    //[Space(10)]
    static public GAMEMODE gameMode { get; private set; }
    //[Header("Ring")]
    //[Space(10)]
    static public float timeToCloseRing {  get; private set; }
    static public float closeRingCooldown { get; private set; }
    static public float CDWTicDamage { get; private set; }
    static public AnimationCurve damageCurve { get; private set; }
    static public UnityEvent<RINGNAME> OpenRingEvent = new UnityEvent<RINGNAME>();
    static public UnityEvent<RINGNAME> CloseRingEvent = new UnityEvent<RINGNAME>();
    static public UnityEvent<RINGNAME> WillCloseRingEvent = new UnityEvent<RINGNAME>();
    static public void Init(RoundRulesSO SO)
    {
        gameMode = SO.gameMode;
        timeToCloseRing = SO.timeToCloseRing;
        closeRingCooldown = SO.closeRingCooldown;
        CDWTicDamage = SO.CDWTicDamage;
        damageCurve = SO.damageCurve;
    }

    static public void CloseRandomRing()=>
        GameManager.Instance.ringGestion.TryCloseRandomRing();
    
    static public void CloseRing(RINGNAME name) =>
        GameManager.Instance.ringGestion.TryCloseRing(name);

    static public int GetDamageOfZone(int nbRingClosed)
    {
        Debug.Log("nb ring close : "+ nbRingClosed+" damages : " + (int)damageCurve.Evaluate(nbRingClosed));
        return (int)damageCurve.Evaluate(nbRingClosed);
    }
}
