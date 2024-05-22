using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerInfos infos;
    void Start()
    {
        infos = GetComponent<PlayerInfos>();
        infos.isDeadEvent.AddListener((infos) => Destroy(this));
    }

    void Update()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        if(infos.inputSystem.isInteracting)
            Interact();
    }

    private void Interact()
    {
        //TODO : prendre le plus proche, etc.
    }
}
