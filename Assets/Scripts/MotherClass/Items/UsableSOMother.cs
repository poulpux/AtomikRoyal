using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableSOMother : ScriptableObject
{
    [Header("UsableSO")]
    [Space(10)]
    public string nameUsable;
    public string description;
    public int nbMaxInventory, nbRecolted;
    public Sprite sprite;
    public RARITY rarity;
    [HideInInspector] public TextAsset script;
}
