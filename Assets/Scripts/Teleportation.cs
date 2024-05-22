using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public void TeleportInBox(Vector2 pos, Vector2 size,int counter = 0)
    {
        Vector2 teleportPosition = new Vector2(Random.Range(-size.x / 2f, size.x / 2f), Random.Range(-size.y / 2f, size.y / 2f)) + pos;

        //if (!CheckCollision(teleportPosition))
            transform.position = teleportPosition;
        //else if (counter <= 5f)
        //    TeleportInBox(pos, size, counter++);
        //else
        //    Debug.Log("could not found a position, try hit later");
    }

    public void TeleportInCircle(Vector2 pos, float radius, int counter = 0)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized * radius;
        Vector2 teleportPosition = new Vector2(randomDirection.x, randomDirection.y) + pos;

        //if (!CheckCollision(teleportPosition))
            transform.position = teleportPosition;
        //else if (counter <= 5f)
        //    TeleportInCircle(pos, radius, counter++);
        //else
        //    Debug.Log("could not found a position, try hit later");
    }
    public void TeleportInSphere(Vector3 pos, float radius, int counter = 0)
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized * radius;
        Vector3 teleportPosition = randomDirection + pos;

        //if (!CheckCollision(teleportPosition))
            transform.position = teleportPosition;
        //else if (counter <= 5f)
        //    TeleportInCircle(pos, radius, counter++);
        //else
        //    Debug.Log("could not found a position, try hit later");
    }

    public void TeleportInCube(Vector3 pos, Vector3 size, int counter = 0)
    {
        Vector3 teleportPosition = new Vector3(Random.Range(-size.x / 2f, size.x / 2f), Random.Range(-size.y / 2f, size.y / 2f), Random.Range(-size.z / 2f, size.z / 2f)) + pos;

        //if (!CheckCollision(teleportPosition))
            transform.position = teleportPosition;
        //else if (counter <= 5f)
        //    TeleportInBox(pos, size, counter++);
        //else
        //    Debug.Log("could not found a position, try hit later");
    }

    //private bool CheckCollision(Vector3 position)
    //{
    //    Collider[] colliders = Physics.OverlapSphere(position, 0.1f);
    //    return colliders.Length > 0;
    //}
}
