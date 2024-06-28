using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELEMENTS
{
    WALL,
    FLAMMABLEWALL,
    WATER,
    BUSH,
    GLUE,
    SMOKE,
    GAZ,
    FIRE,
    EXPLOSION
}

[CreateAssetMenu(fileName = "StaticEnvironementSO", menuName = "Static/StaticEnvironementSO")]
public class StaticEnvironementSO : ScriptableObject
{
    public List<InteractionElement> allInteractions = new List<InteractionElement>();
    public int mapLenght, tabResolution;
    [Header("All prefabs")]
    public GameObject wallPrefab;
    public GameObject flammableWallPrefab, waterPrefab, bushPrefab, gluePrefab, smokePrefab, gazPrefab, firePrefab, explosionPrefab;
    [Header("All interactions")]
    [TextArea]
    public string ATTENTION = "toutes les enums doivent etre prensente qu'une fois, c'est essentiel pour le déroulement des interactions";
    public List<ELEMENTS> elementsInteractionsPriority = new List<ELEMENTS>();
}

[Serializable]
public class InteractionElement
{
    public ELEMENTS currentElement;
    public List<ELEMENTS> interactions = new List<ELEMENTS>();
}