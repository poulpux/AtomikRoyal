using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "SO/CardSO")]
public class CardSO : ScriptableObject
{
    [Header("CardSO")]
    [Space(10)]
    public string namee;
    public string description;
    public int cost;
    public Sprite spriteCard;

    public TextAsset script;

    private void OnValidate()
    {
        VerifType<Card>();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    protected void VerifType<T>()
    {
        if (script == null)
            throw new System.Exception("No reference on "+ typeof(T)+" script");

        System.Type t = System.Type.GetType(script.name.Replace(".cs", ""));

        if (t == null)
        {
            script = null;
            throw new System.Exception("The referenced asset is not a script");
        }

        if (t.BaseType != typeof(T))
        {
            script = null;
            throw new System.Exception("The referenced script is not an "+ typeof(T));
        }
    }
}
