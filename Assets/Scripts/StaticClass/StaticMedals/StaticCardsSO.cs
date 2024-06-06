using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticMedalsSO", menuName = "Static/StaticMedalsSO")]
public class StaticMedalsSO : ScriptableObject
{
    [Header("All Medals")]
    [Space(10)]

    public List<MedalsSO> allMedals;
}
