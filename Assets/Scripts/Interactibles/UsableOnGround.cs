using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableOnGround : Interactible
{
    protected override void Use(PlayerInfos infos)
    {
        base.Use(infos);
        infos.inventory.AddObject(GetComponent<Usable>());
        Destroy(gameObject);
    }
}
