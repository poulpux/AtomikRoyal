using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenPlayerDied : MonoBehaviour
{
    private List<MonoBehaviour> scriptToActivate = new List<MonoBehaviour>();
    private List<MonoBehaviour> scriptToDesactivate = new List<MonoBehaviour>();

    private void Awake()
    {
        GetAllMonobehaviour();
        GetComponent<PlayerInfos>().isDeadEvent.AddListener((infos)=> PlayerDied());
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void PlayerDied()
    {
        foreach (var item in scriptToActivate)
           item.enabled = true;
        foreach (var item in scriptToDesactivate)
            item.enabled = false;

        enabled = false;
    }

    private void GetAllMonobehaviour()
    {
        MonoBehaviour[] tabMono = GetComponents<MonoBehaviour>();
        foreach (var item in tabMono)
        {
            if (item is ActiveWhenPlayerIsDead)
            {
                scriptToActivate.Add(item);
                item.enabled = false;
            }
            if (item is DesactiveWhenPlayerIsDead)
                scriptToDesactivate.Add(item);
        }
    }
}
