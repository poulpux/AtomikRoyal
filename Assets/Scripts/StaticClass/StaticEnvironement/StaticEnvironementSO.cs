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
    public float tabResolution;

    [Header("Fire")]
    public int flammablePvMax;
    public float CDWDamageFire, CDWFireDispertion;
    public int damageFire;
    
    [Header("GAZ")]
    public float CDWDamageGaz;
    public float CDWGazDispertion;
    public int damageGaz;

    [Header("Ring")]
    [Space(10)]
    public float CDWDamageRing;
    public AnimationCurve damageRingCurve;

    [Header("All prefabs")]
    public GameObject wallPrefab;
    public GameObject flammableWallPrefab, waterPrefab, bushPrefab, gluePrefab, smokePrefab, gazPrefab, firePrefab, explosionPrefab;

    [Header("All tags")]
    public string enviroLayerName;
    public string waterTag;
    public string bushTag, glueTag, smokeTag, gazTag, fireTag, ringTag;
    
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