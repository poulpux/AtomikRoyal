using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenPlayerDied : MonoBehaviour
{
    private void Awake()
    {
        MonoBehaviour[] tabMono = GetComponents<MonoBehaviour>();
        foreach (var item in tabMono)
        {
            if (item is IActiveWhenPlayerIsDead)
                item.enabled = false;
        }

        GetComponent<PlayerInfos>().isDeadEvent.AddListener((infos) => PlayerDied());
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void PlayerDied()
    {
        MonoBehaviour[] tabMono = GetComponents<MonoBehaviour>();
        foreach (var item in tabMono)
        {
            if (item is IActiveWhenPlayerIsDead)
                item.enabled = true;
            else if (item is IDesactiveWhenPlayerIsDead)
            {
                item.GetComponent<IDesactiveWhenPlayerIsDead>().WhenDead();
                item.enabled = false;
            }
        }

        enabled = false;
    }
}