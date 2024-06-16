using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGrenadeImpulse : ProjectileBombMother
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(TouchPosToGo());
    }

    private IEnumerator TouchPosToGo()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, posToGo) < 0.5f)
                Destroy(gameObject);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
