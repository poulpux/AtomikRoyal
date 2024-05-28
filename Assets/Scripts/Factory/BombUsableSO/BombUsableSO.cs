using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BombSO_fileName", menuName = "SO/BombSO")]
public class BombUsableSO : UsableSO
{
    [Header("BombUsableSO")]
    [Space(10)]
    public int baseDamage;
    public PlayerInfos owner;
    public Sprite objectToInstantiate;
    public GameObject explosionPrefab;
    public const string prefabAdressGrenadeImpulse = "Prefabs/ArchiUsable/PrefabGrenadeImpulse";
    public const string prefabAdressGrenadeCDW = "Prefabs/ArchiUsable/PrefabGrenadeCDW";
    public const string prefabAdressMine = "Prefabs/ArchiUsable/PrefabMine";

    private void OnValidate()
    {
        VerifType<BombUsable>();
        VerifExplosionPrefabComponent();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


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
