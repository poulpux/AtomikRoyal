using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum UTILITYTYPE
{
    HEAL,
    SHIELD,
    SPDBOOST,
    GRENADECDW,
    GRENADEIMPULSE,
    MINE,
    OTHER
}

[CreateAssetMenu(fileName = "UtilitySO_filename", menuName = "SO/UtilitySO")]
public class UtilityUsableSO : UsableSO
{
    [Header("TYPE")]
    [Space(10)]
    [SerializeField] UTILITYTYPE type;

    [ConditionalField("type", UTILITYTYPE.HEAL, "==")]  public int nbHeal;
    [ConditionalField("type", UTILITYTYPE.SHIELD, "==")]  public int nbShield;
    [ConditionalField("type", UTILITYTYPE.SPDBOOST, "==")]  public float spdModifier;
    [ConditionalField("type", UTILITYTYPE.GRENADECDW, "!=")][ConditionalField("type", UTILITYTYPE.GRENADEIMPULSE, "!=")][ConditionalField("type", UTILITYTYPE.MINE, "!=")][ConditionalField("type", UTILITYTYPE.OTHER, "!=")]  
    public float timeUse;

    [ConditionalField("type", UTILITYTYPE.GRENADECDW, "==")] public float cdw;

    [ConditionalField("type", UTILITYTYPE.HEAL, "!=")]
    [ConditionalField("type", UTILITYTYPE.SHIELD, "!=")]
    [ConditionalField("type", UTILITYTYPE.SPDBOOST, "!=")]
    [ConditionalField("type", UTILITYTYPE.OTHER, "!=")]
    public Sprite spriteObjectToInstantiate;
    
    [ConditionalField("type", UTILITYTYPE.HEAL, "!=")]
    [ConditionalField("type", UTILITYTYPE.SHIELD, "!=")]
    [ConditionalField("type", UTILITYTYPE.SPDBOOST, "!=")]
    [ConditionalField("type", UTILITYTYPE.OTHER, "!=")]
    public GameObject  explosionPrefab;


    //Other
    [ConditionalField("type", UTILITYTYPE.OTHER, "==")] public TextAsset otherScript;

    private const string healScriptPath = "Assets/Scripts/Factory/UtilitySO/UtilityUsableHeal.cs";
    private const string shieldScriptPath = "Assets/Scripts/Factory/UtilitySO/UtilityUsableShield.cs";
    private const string spdBoostScriptPath = "Assets/Scripts/Factory/UtilitySO/UtilityUsableSpdBoost.cs";
    private const string BombScriptPath = "Assets/Scripts/Factory/UtilitySO/UtilityUsableBomb.cs";


    public const string prefabPathGrenadeImpulse = "PrefabGrenadeImpulse";
    public const string prefabPathGrenadeCDW = "PrefabGrenadeCDW";
    public const string prefabPathMine = "PrefabMine";

    private void OnValidate()
    {
        if (type == UTILITYTYPE.HEAL)
            script = AssetDatabase.LoadAssetAtPath<TextAsset>(healScriptPath);
        else if (type == UTILITYTYPE.SHIELD)
            script = AssetDatabase.LoadAssetAtPath<TextAsset>(shieldScriptPath);
        else if (type == UTILITYTYPE.SPDBOOST)
            script = AssetDatabase.LoadAssetAtPath<TextAsset>(spdBoostScriptPath);
        else if (type == UTILITYTYPE.GRENADECDW || type == UTILITYTYPE.GRENADEIMPULSE || type == UTILITYTYPE.MINE)
            script = AssetDatabase.LoadAssetAtPath<TextAsset>(BombScriptPath);  
        else
            VerifOtherType<UtilityUsable>();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public string GetPath()
    {
        if (type == UTILITYTYPE.GRENADECDW)
            return prefabPathGrenadeCDW;
        else if (type == UTILITYTYPE.GRENADEIMPULSE)
            return prefabPathGrenadeImpulse;
        else if (type == UTILITYTYPE.MINE)
            return prefabPathMine;

        return prefabPathGrenadeCDW;
    }

    protected void VerifOtherType<T>()
    {
        if (otherScript == null)
            throw new System.Exception("No reference on " + typeof(T) + " script");

        System.Type t = System.Type.GetType(otherScript.name.Replace(".cs", ""));

        if (t == null)
        {
            otherScript = null;
            throw new System.Exception("The referenced asset is not a script");
        }

        if (t.BaseType != typeof(T))
        {
            otherScript = null;
            throw new System.Exception("The referenced script is not an " + typeof(T));
        }

        script = otherScript;
    }
}
