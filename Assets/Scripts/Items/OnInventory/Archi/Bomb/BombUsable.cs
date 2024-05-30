using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUsable : UsableMother
{
    BombUsableSO realSO;

    protected override void Start()
    {
        base.Start();
        realSO = SO as BombUsableSO;
    }

    protected override void Use()
    {
        base.Use();
        GameObject objet = Instantiate(GF.GetPrefabAdress(realSO.GetPath()).gameObject);
        objet.transform.position = transform.position;
        objet.GetComponent<SpriteRenderer>().sprite = realSO.objectToInstantiate;
        objet.GetComponent<BombMother>().Init(infos, realSO.explosionPrefab, 0f, infos.inputSystem.mousePos, realSO.cdw);
    }
}
