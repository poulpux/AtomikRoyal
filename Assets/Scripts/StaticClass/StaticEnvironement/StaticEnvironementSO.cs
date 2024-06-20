using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELEMENTS
{
    WALL,
    WATER,
    BUSH,
    GLUE,
    GAZ,
    FIRE,
    SMOKE
}

[CreateAssetMenu(fileName = "StaticEnvironementSO", menuName = "Static/StaticEnvironementSO")]
public class StaticEnvironementSO : ScriptableObject
{
    public List<InteractionElement> allInteractions = new List<InteractionElement>();
}

[Serializable]
public class InteractionElement
{
    public ELEMENTS currentElement;
    public List<ELEMENTS> interactions = new List<ELEMENTS>();
}