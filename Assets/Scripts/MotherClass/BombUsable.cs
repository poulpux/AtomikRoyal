using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUsable : Usable
{
    public override void Use()
    {
        base.Use();
    }

    public override void AddSO(UsableSO SO)
    {
        if (SO.GetType() == typeof(BombUsableSO))
            this.SO = SO;
        else
            Debug.Log("Try to add the SO variable but it's not the right type");
    }
}
