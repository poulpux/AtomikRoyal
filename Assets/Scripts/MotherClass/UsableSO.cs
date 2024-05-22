using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableSO : ScriptableObjectWithScript
{
    [Header("UsableSO")]
    [Space(10)]
    public string nameUsable;
    public string description;
    public int nbMaxInventory, nbRecolted;
    public Sprite sprite;
    public RARITY rarity;
}
