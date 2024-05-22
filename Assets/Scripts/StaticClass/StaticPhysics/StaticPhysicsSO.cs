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
}
