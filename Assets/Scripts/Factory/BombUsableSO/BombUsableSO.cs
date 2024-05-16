using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BombUsableSO", menuName = "SO/BombUsableSO")]
public class BombUsableSO : UsableSO
{
    [Header("BombUsableSO")]
    [Space(10)]
    public int baseDamage;
    public PlayerInfos owner;
    public GameObject objectToInstantiate;
    public GameObject explosionPrefab;

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
