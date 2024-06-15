using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractibleMother
{
    protected override void Use(PlayerInfos infos)
    {
        base.Use(infos);
        _StaticChest.OpenChest(transform.position);
        GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(this);
    }
}
