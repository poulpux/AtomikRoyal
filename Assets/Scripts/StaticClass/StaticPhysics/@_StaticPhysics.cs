using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticPhysics
{
    static public float onGroundDrag;
    static public float onGroundImpulseForce, chestRadius;

    static public void Init(StaticPhysicsSO SO)
    {
        onGroundDrag = SO.onGroundDrag;
        onGroundImpulseForce = SO.onGroundImpulseForce;
        chestRadius = SO.chestRadius;
    }
}
