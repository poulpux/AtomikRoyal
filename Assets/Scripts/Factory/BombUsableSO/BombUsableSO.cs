using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BOMBTYPE
{
    GRENADECDW,
    GRENADEIMPULSE,
    MINE
}

[CreateAssetMenu(fileName = "BombSO_fileName", menuName = "SO/BombSO")]
public class BombUsableSO : UsableSO
{
    [Header("BombUsableSO")]
    [Space(10)]
    public BOMBTYPE type;
    public int baseDamage;
    public PlayerInfos owner;
    public Sprite objectToInstantiate;
    public GameObject explosionPrefab;
    [SerializeField] private TextAsset bombScript;

    public const string prefabPathGrenadeImpulse = "Prefabs/ArchiUsable/PrefabGrenadeImpulse";
    public const string prefabPathGrenadeCDW = "Prefabs/ArchiUsable/PrefabGrenadeCDW";
    public const string prefabPathMine = "Prefabs/ArchiUsable/PrefabMine";

    private void OnValidate()
    {
        VerifBombType<BombUsable>();
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

    protected void VerifBombType<T>()
    {
        if (bombScript == null)
            throw new System.Exception("No reference on " + typeof(T) + " script");

        System.Type t = System.Type.GetType(bombScript.name.Replace(".cs", ""));

        if (t == null)
        {
            bombScript = null;
            throw new System.Exception("The referenced asset is not a script");
        }

        if (t.BaseType != typeof(T))
        {
            bombScript = null;
            throw new System.Exception("The referenced script is not an " + typeof(T));
        }

        script = bombScript;
    }
}
