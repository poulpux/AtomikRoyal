using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSpawnPos : MonoBehaviour
{
    private void Awake()
    {
        _StaticChest.allChestPos.Add(transform.position);
        Destroy(gameObject);
    }
}
