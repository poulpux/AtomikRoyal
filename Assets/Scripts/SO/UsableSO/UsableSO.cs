using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UsableSO", menuName = "SO/UsableSO")]
public class UsableSO : ScriptableObject
{
    [Header("UsableSO")]
    [Space(10)]
    public string nameUsable;
    public string description;
    public int nbMaxInventory, nbRecolted;
    public SpriteRenderer sprite;
    public RARITY rarity;

    public TextAsset script;

    private void OnValidate()
    {
        VerifType<Usable>();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    protected void VerifType<T>()
    {
        if (script == null)
            throw new System.Exception("No reference on item script");

        System.Type t = System.Type.GetType(script.name.Replace(".cs", ""));

        if (t == null)
        {
            script = null;
            throw new System.Exception("The referenced asset is not a script");
        }

        if (t.BaseType != typeof(T))
        {
            script = null;
            throw new System.Exception("The referenced script is not an Item");
        }
    }
}
