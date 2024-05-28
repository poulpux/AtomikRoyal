using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityUsableBomb : Usable
{
    UtilityUsableSO realSO;

    protected override void Start()
    {
        base.Start();
        realSO = SO as UtilityUsableSO;
    }
    protected override void Use()
    {
        base.Use();
        GameObject objet = Instantiate(GF.GetPrefabAdress(realSO.GetPath()));
    }

}
