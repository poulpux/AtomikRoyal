using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DISPERTIONTYPE
{
    NONE,
    FIRE,
    GAZ
}

[CreateAssetMenu(fileName = "ElementalBombSO_filename", menuName = "SO/ElementalBombSO")]
public class ElementalBombSO : ScriptableObject
{
    public ELEMENTS type;
    public DISPERTIONTYPE dispertionType;
    public int distOnStart;
    public int maxDistanceDispertion;
}
