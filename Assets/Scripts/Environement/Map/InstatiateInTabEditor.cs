using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateInTabEditor : MonoBehaviour
{
    [SerializeField] private ELEMENTS type;

    void Update()
    {
        EnviroManager.Instance.AddElementEvent.Invoke((int)transform.localPosition.x, (int)transform.localPosition.y, type);
        Destroy(gameObject, 0.1f);
    }
}
