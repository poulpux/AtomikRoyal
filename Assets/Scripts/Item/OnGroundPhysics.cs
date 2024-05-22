using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OnGroundPhysics : MonoBehaviour
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = _StaticPhysics.onGroundDrag;
    }
    public void Init(Vector3 chestPos)
    {
        Vector3 direction = transform.position - chestPos;
        direction.Normalize();
        rb.AddForce(direction * _StaticPhysics.onGroundImpulseForce , ForceMode2D.Impulse);
    }
}
