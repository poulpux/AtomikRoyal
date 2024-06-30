using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapScaleAndPos : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector2(_StaticEnvironement.tabResolution, _StaticEnvironement.tabResolution);
        //On fait en sorte que la position de la map concorde avec le tableau d'environement
        int x = (int)(transform.localPosition.x - _StaticEnvironement.originX) +1;
        int y = (int)(transform.localPosition.y - _StaticEnvironement.originY) +1;
        transform.position = new Vector2(x * _StaticEnvironement.tabResolution, y * _StaticEnvironement.tabResolution) + new Vector2(_StaticEnvironement.originX, _StaticEnvironement.originY);
    }
}
