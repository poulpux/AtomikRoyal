using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGrenadeImpulse : ProjectileBombMother
{
    Vector2 startDirection;
    protected override void Start()
    {
        base.Start();
        startDirection =(posToGo - (Vector2) transform.position).normalized;
        StartCoroutine(TouchPosToGo());
    }

    private IEnumerator TouchPosToGo()
    {
        while (true)
        {
            Vector2 currentDirection = (posToGo - (Vector2)transform.position).normalized;
            float angle = Vector2.Angle(startDirection, currentDirection);
            if (angle > 5f)
                Destroy(gameObject);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
