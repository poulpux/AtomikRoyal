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
    static public List<RingZone> zones { get; private set; } = new List<RingZone>();
    static public UnityEvent<RINGSTATE> CloseRingEvent = new UnityEvent<RINGSTATE>();
    static public void Init(RoundRulesSO SO)
    {
        gameMode = SO.gameMode;
        timeToCloseRing = SO.timeToCloseRing;
    }
}

public class RingZone
{
    RINGNAME name;
    RINGSTATE state;
}
