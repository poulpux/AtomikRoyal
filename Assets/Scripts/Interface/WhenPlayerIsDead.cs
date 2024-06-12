using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenPlayerDied : MonoBehaviour
{
    //Sert à récupérer tous les interfaces dans l'objet en question et jouer une logique de spawn / die

    private List<MonoBehaviour> activeWhenDeadList = new List<MonoBehaviour>();
    private List<MonoBehaviour> desactiveWhenDeadList = new List<MonoBehaviour>();
    private List<IActWhenPlayerIsDead> actWhenDeadList;
    private List<IActWhenPlayerSpawn> actWhenSpawnList;
    private void Awake()
    {
        actWhenDeadList = new List<IActWhenPlayerIsDead>(GetComponents<IActWhenPlayerIsDead>());
        actWhenSpawnList = new List<IActWhenPlayerSpawn>(GetComponents<IActWhenPlayerSpawn>());

        GetListMono();

        foreach (var item in activeWhenDeadList)
            item.enabled = false;
        
        PlayerInfos infos = GetComponent<PlayerInfos>();
        infos.isDeadEvent.AddListener(() => PlayerDied());
        infos.isSpawningEvent.AddListener(() => PlayerSpawn());
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void GetListMono()
    {
        MonoBehaviour[] tabMono = GetComponents<MonoBehaviour>();
        foreach (var item in tabMono)
        {
            if (item is IActiveWhenPlayerIsDead)
                activeWhenDeadList.Add(item);
            else if (item is IDesactiveWhenPlayerIsDead)
                desactiveWhenDeadList.Add(item);
        }
    }

    private void PlayerDied()
    {
        foreach (var item in activeWhenDeadList)
            item.enabled = true;
        foreach (var item in desactiveWhenDeadList)
            item.enabled = false;
        foreach (var item in actWhenDeadList)
            item.WhenDied();
    }

    private void PlayerSpawn()
    {
        foreach (var item in actWhenSpawnList)
            item.WhenSpawn();
    }
}