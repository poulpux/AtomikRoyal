using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableOnGround : Interactible
{
    public UsableSO SO;
    public int nb;

    protected override void Use(PlayerInfos infos)
    {
        base.Use(infos);
        infos.inventory.AddObject(SO,nb, this);
        Destroy(gameObject);
    }
}
