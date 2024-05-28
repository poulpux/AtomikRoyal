using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGrenadeCDW : ProjectileBombMother
{
    private void Start()
    {
        Destroy(gameObject, cdw);
    }

    public override void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, Vector2 posToGo, float cdw = 0)
    {
        base.Init(infos, explosionPrefab, baseDomage, posToGo, cdw);
    }
}
