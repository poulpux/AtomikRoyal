using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum BOMBTYPE
{
    GRENADECDW,
    GRENADEIMPULSE,
    MINE
}

[CreateAssetMenu(fileName = "BombSO_fileName", menuName = "SO/BombSO")]
public class BombUsableSO : UsableSOMother
{
    [Header("BombUsableSO")]
    [Space(10)]
    public BOMBTYPE type;
    public float baseDamage;

    [ConditionalField("type", BOMBTYPE.GRENADECDW, "==")] public float cdw;
    public float radius;
    [HideInInspector] public PlayerInfos owner;
    public Sprite objectToInstantiate;
    public GameObject explosionPrefab;
    //[SerializeField] private TextAsset bombScript;

    public const string prefabPathGrenadeImpulse = "PrefabGrenadeImpulse";
    public const string prefabPathGrenadeCDW = "PrefabGrenadeCDW";
    public const string prefabPathMine = "PrefabMine";

    //private const string bombScriptPath = "Assets/Scripts/MotherClass/BombMother.cs";
    private const string bombScriptPath = "Assets/Scripts/Items&Card/OnInventory/Archi/Bomb/BombUsable.cs";

    private void OnValidate()
    {
        script = AssetDatabase.LoadAssetAtPath<TextAsset>(bombScriptPath);
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
}
