using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUsable : Usable
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
        GameObject objet = Instantiate(GF.GetPrefabAdress(realSO.GetPath()));
    }
    public override void AddSO(UsableSO SO)
    {
        if (SO.GetType() == typeof(BombUsableSO))
            this.SO = SO;
        else
            Debug.Log("Try to add the SO variable but it's not the right type");
    }
}
