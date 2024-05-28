using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGrenadeCDW : ProjectileBombMother
{
    Vector2 direction;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = posToGo - (Vector2)transform.position;
        StartCoroutine(TouchPosToGo());
        Destroy(gameObject, cdw);
    }

    private IEnumerator TouchPosToGo()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, posToGo) < 0.2f)
            {
                direction /= 3f;//mettre en static
                posToGo += direction;
                rb.drag *= 2f;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
