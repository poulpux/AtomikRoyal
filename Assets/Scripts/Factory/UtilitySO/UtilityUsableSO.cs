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
    [SerializeField] UTILITYTYPE type;

    [ConditionalField("type", UTILITYTYPE.HEAL, "==")]  public int nbHeal;
    [ConditionalField("type", UTILITYTYPE.HEAL, "==")]  public float timeUse;



    //Other
    [ConditionalField("type", UTILITYTYPE.OTHER, "==")] public TextAsset otherScript;

    private const string healScriptPath = "Assets/Scripts/Factory/UtilitySO/UtilityUsableHeal.cs";

    private void OnValidate()
    {
        if (type == UTILITYTYPE.HEAL)
            script = AssetDatabase.LoadAssetAtPath<TextAsset>(healScriptPath);
        else
            VerifOtherType<UtilityUsable>();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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
