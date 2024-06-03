using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public enum RINGNAME
{
    UPLEFT,
    UPRIGHT,
    MIDDLELEFT,
    MIDDLE,
    MIDDLERIGHT,
    DOWNLEFT,
    DOWNRIGHT,
}

public enum RINGSTATE
{
    FREE,
    WILLCLOSE,
    CLOSE
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
    static public UnityEvent<RINGNAME> CloseRingEvent = new UnityEvent<RINGNAME>();
    static public UnityEvent<RINGNAME> WillCloseRingEvent = new UnityEvent<RINGNAME>();
    static public void Init(RoundRulesSO SO)
    {
        gameMode = SO.gameMode;
        timeToCloseRing = SO.timeToCloseRing;
        closeRingCooldown = SO.closeRingCooldown;
    }

    static public void CloseRandomRing()=>
        GameManager.Instance.ringGestion.TryCloseRandomRing();
    
    static public void CloseRing(RINGNAME name) =>
        GameManager.Instance.ringGestion.TryCloseRing(name);
}

public class RingZone
{
    public RINGNAME name;
    public RINGSTATE state;
}
