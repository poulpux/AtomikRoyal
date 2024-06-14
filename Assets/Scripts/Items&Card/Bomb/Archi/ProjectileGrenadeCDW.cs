using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectileGrenadeCDW : ProjectileBombMother
{
    Vector2 direction;
    float timeToGo;
    private void Start()
    {
        direction = posToGo - (Vector2)transform.position;
        CalculateTimeToGo();
        StartCoroutine(TouchPosToGo());
        Destroy(gameObject, cdw);
    }

    private IEnumerator TouchPosToGo()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (Vector2.Distance(transform.position, posToGo) < 0.1f || timer > timeToGo)
            {
                timer = 0f;
                direction /= _StaticPhysics.lostSpdBounce;
                timeToGo /= _StaticPhysics.lostSpdBounce;
                posToGo += direction;

                rb.velocity = Vector2.zero;
                rb.AddForce(direction, ForceMode2D.Impulse);
                if (direction.magnitude < 0.1f)
                {
                    rb.drag = _StaticPhysics.onGroundDrag;
                    yield break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void CalculateTimeToGo()
    {
        float distance = Vector2.Distance(transform.position, posToGo);
        float speed = rb.velocity.magnitude;

        if (speed == 0)
        {
            Debug.LogWarning("Velocity is zero, can't reach the target.");
            timeToGo = float.PositiveInfinity;
        }

        timeToGo = distance / speed;
    }
}
