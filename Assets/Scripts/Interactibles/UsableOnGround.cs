using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableOnGround : Interactible
{
    public UsableSO SO;
    public int nb;

    private void Start()
    {
        print("SO " + SO.name);
    }

    protected override void Use(PlayerInfos infos)
    {
        base.Use(infos);
        infos.inventory.AddObject(SO,nb, this);
        if(nb <= 0)
            Destroy(gameObject);
    }
}
