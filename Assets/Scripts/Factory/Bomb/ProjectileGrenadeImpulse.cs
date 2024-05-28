using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGrenadeImpulse : ProjectileBombMother
{
    private void Start()
    {
        StartCoroutine(TouchPosToGo());
    }

    private IEnumerator TouchPosToGo()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, posToGo) < 0.5f)
            {
                print("distance explose");
                Destroy(gameObject);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    //public override void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, Vector2 posToGo, float cdw = 0)
    //{
    //    base.Init(infos, explosionPrefab, baseDomage, posToGo, cdw);
    //}

}
