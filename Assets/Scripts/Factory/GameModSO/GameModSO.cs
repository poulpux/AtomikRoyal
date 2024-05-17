using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModSO_filename", menuName = "SO/GameModSO")]
public class GameModSO : ScriptableObjectWithScript
{
    public bool test;
    public int nb;
    [ConditionalField("nb", 10, "==")]
    public float supTen;
    
}
