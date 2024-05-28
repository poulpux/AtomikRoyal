using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BombUsable : Usable
{
    BombUsableSO realSO;

    protected void Explose()
    {
        //Instantiate()
    }

    public override void AddSO(UsableSO SO)
    {
        if (SO.GetType() == typeof(BombUsableSO))
            this.SO = SO;
        else
            Debug.Log("Try to add the SO variable but it's not the right type");
    }
}
