using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticPlayerSO", menuName = "SO/StaticPlayerSO")]
public class StaticPlayerSO : ScriptableObject
{
    [Header("All curves")]
    [Space(10)]
    public AnimationCurve spdCostCurve;
    public AnimationCurve pvMaxCostCurve;
    public AnimationCurve explosionSizeCostCurve;
    public AnimationCurve dmgCaCCostCurve;
    public AnimationCurve dmgExplosionCostCurve;
    public AnimationCurve cooldownThrowCostCurve;
    public AnimationCurve grenadeSpdCost;
    public AnimationCurve rangeCostCurve;

    [Header("GeneralValues")]
    [Space(10)]
    public float rangeInteractible;
}
