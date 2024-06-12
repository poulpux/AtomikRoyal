using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenPlayerDied : MonoBehaviour
{
    MonoBehaviour[] tabMono;
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
        foreach (var item in tabMono)
        {
            if(item is IActWhenPlayerIsDead)
                item.GetComponent<IActWhenPlayerIsDead>().WhenDied();

            if (item is IActiveWhenPlayerIsDead)
                item.enabled = true;
            else if (item is IDesactiveWhenPlayerIsDead)
                item.enabled = false;
        }

        enabled = false;
    }
}