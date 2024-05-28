using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticPhysics
{
    static public float onGroundDrag { private set; get; }
    static public float grenadeDrag { private set; get; }
    static public float onGroundImpulseForce { private set; get; }
    static public float lostSpdBounce { private set; get; }
    static public float chestRadius { private set; get; }
    static public void Init(StaticPhysicsSO SO)
    {
        onGroundDrag = SO.onGroundDrag;
        onGroundImpulseForce = SO.onGroundImpulseForce;
        lostSpdBounce = SO.lostSpdBounce;    
        chestRadius = SO.chestRadius;
        grenadeDrag = SO.grenadeDrag;
    }
}
