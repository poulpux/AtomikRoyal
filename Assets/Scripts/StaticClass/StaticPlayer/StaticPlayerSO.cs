using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticPlayerSO", menuName = "Static/StaticPlayerSO")]
public class StaticPlayerSO : ScriptableObject
{
    [Header("All curves")]
    [Space(10)]
    public UpgradeCurves spd;
    public UpgradeCurves pvMax;
    public UpgradeCurves explosionSize;
    public UpgradeCurves dmgCAC;
    public UpgradeCurves dmgBomb;
    public UpgradeCurves cdwThrow;
    public UpgradeCurves throwForce;
    public UpgradeCurves range;

    [Header("Interact")]
    [Space(10)]
    public float rangeInteractible;
    public float timeInteractibleBecomeZero;

    [Header("Control")]
    [Space(10)]
    public float scrollCdwCursor;
    
    [Header("Speed")]
    [Space(10)]
    public float glueSpdModifier;
    public float waterSpdModifier, deadSpd;
    public AnimationCurve beginEndMoveCurve;
    public float beginEndMoveCurveDuration = 0.1f;

    [Serializable]
    public class UpgradeCurves
    {
        public AnimationCurve CostCurve;
        public float startValue;
        public float statsParLv;
    }
}
