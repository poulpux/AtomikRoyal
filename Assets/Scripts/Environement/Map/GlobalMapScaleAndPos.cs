using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapScaleAndPos : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector2(_StaticEnvironement.tabResolution, _StaticEnvironement.tabResolution);
        transform.position = new Vector2(_StaticEnvironement.originX, _StaticEnvironement.originY);
    }
}
