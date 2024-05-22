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
        print(rb.drag);
        rb.drag = 3f;
    }
    public void Init(Vector3 chestPos)
    {
        Vector3 direction = transform.position - chestPos;
        direction.Normalize();
        rb.AddForce(direction *1.5f , ForceMode2D.Impulse);
    }
}
