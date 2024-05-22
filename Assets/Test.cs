using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        _StaticChest.OpenChest(transform.position);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            _StaticChest.OpenChest(transform.position);
    }
}
