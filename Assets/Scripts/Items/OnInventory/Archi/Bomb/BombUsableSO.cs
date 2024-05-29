using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum BOMBTYPE
{
    GRENADECDW,
    GRENADEIMPULSE,
    MINE,
    OTHER
}

[CreateAssetMenu(fileName = "BombSO_fileName", menuName = "SO/BombSO")]
public class BombUsableSO : UsableSOMother
{
    [Header("BombUsableSO")]
    [Space(10)]
    public BOMBTYPE type;
    public float baseDamage;

    [ConditionalField("type", BOMBTYPE.GRENADECDW, "==")] public float cdw;
    [HideInInspector] public PlayerInfos owner;
    [ConditionalField("type", BOMBTYPE.OTHER, "!=")]
    public Sprite objectToInstantiate;
    [ConditionalField("type", BOMBTYPE.OTHER, "!=")]
    public GameObject explosionPrefab;
    [ConditionalField("type", BOMBTYPE.OTHER, "==")]
    public TextAsset otherScript;

    //[SerializeField] private TextAsset bombScript;

    public const string prefabPathGrenadeImpulse = "PrefabGrenadeImpulse";
    public const string prefabPathGrenadeCDW = "PrefabGrenadeCDW";
    public const string prefabPathMine = "PrefabMine";

    //private const string bombScriptPath = "Assets/Scripts/MotherClass/BombMother.cs";
    private const string bombScriptPath = "Assets/Scripts/Items/OnInventory/Archi/Bomb/BombUsable.cs";

    private void OnValidate()
    {
        script = AssetDatabase.LoadAssetAtPath<TextAsset>(bombScriptPath);
        if(type == BOMBTYPE.OTHER)
            VerifOtherType<UsableMother>();

        VerifExplosionPrefabComponent();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public string GetPath()
    {
        if (type == BOMBTYPE.GRENADECDW)
            return prefabPathGrenadeCDW;
        else if (type == BOMBTYPE.GRENADEIMPULSE)
            return prefabPathGrenadeImpulse;
        else if(type == BOMBTYPE.MINE)
            return prefabPathMine;

        return prefabPathGrenadeCDW;
    }

    private void VerifExplosionPrefabComponent()
    {
        if(explosionPrefab ==  null) 
            throw new System.Exception("explosionPrefab is null");
        Explosion explo = explosionPrefab.GetComponent<Explosion>();
        if (explo == null)
        {
            explosionPrefab = null;
            throw new System.Exception("explosion prefab haven't the script Explosion");
        }
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
