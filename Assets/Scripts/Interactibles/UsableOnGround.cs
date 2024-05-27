using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableOnGround : Interactible
{
    public UsableSO SO;

    private void Start()
    {
        print(SO.script.name);
    }
    protected override void Use(PlayerInfos infos)
    {
        base.Use(infos);
        infos.inventory.AddObject(SO,SO.nbRecolted);
        Destroy(gameObject);
    }
}
