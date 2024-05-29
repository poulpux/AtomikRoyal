using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableOnGround : Interactible
{
    public UsableSOMother SO;
    public int nb;

    protected override void Use(PlayerInfos infos)
    {
        base.Use(infos);
        infos.inventory.AddObject(SO,nb, this);
        if(nb <= 0)
            Destroy(gameObject);
    }
}
