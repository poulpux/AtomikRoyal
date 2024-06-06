using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MedalsMother : MonoBehaviour
{
    protected PlayerInfos infos;
    private void Awake()
    {
        infos = GetComponent<PlayerInfos>();
    }
}
