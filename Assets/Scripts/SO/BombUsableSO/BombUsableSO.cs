using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BombUsableSO", menuName = "SO/BombUsableSO")]
public class BombUsableSO : UsableSO
{
    PlayerInfos owner;
    GameObject objectToInstantiate;
    GameObject explosionPrefab;

    private void OnValidate()
    {
        VerifType<BombUsable>();
        VerifExplosionPrefabComponent();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void VerifExplosionPrefabComponent()
    {
        Explosion explo = explosionPrefab.GetComponent<Explosion>();
        if (explo == null)
        {
            explosionPrefab = null;
            throw new System.Exception("explosion prefab haven't the script Explosion");
        }
    }
}
