using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticCardsSO", menuName = "Static/StaticCardsSO")]
public class StaticCardsSO : ScriptableObject
{
    [Header("All Cards")]
    [Space(10)]
    public List<CardSO> allCards = new List<CardSO>();
}
