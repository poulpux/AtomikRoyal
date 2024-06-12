using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticPhysicsSO", menuName = "Static/StaticPhysicsSO")]
public class StaticPhysicsSO : ScriptableObject
{
    [Header("OnGroundPhysics")]
    [Space(10)]
    public float onGroundDrag;
    public float onGroundImpulseForce;

    [Header("Grenade")]
    [Space(10)]
    public float grenadeDrag;
    public float lostSpdBounce = 3f;

    [Header("Explosion")]
    [Space(10)]
    public int explosionResolution;
    public LayerMask ObstructingLayers;

    [Header("Chest")]
    [Space(10)]
    public float chestRadius;
}
