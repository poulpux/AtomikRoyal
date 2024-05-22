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
        Collider2D[] tabColli = Physics2D.OverlapCircleAll(transform.position, _StaticPlayer.rangeInteractible);
        float minDistance = 100f;
        Interactible nearestInteractible = null;
        for (int i = 0; i < tabColli.Length; i++)
        {
            if (!(Vector2.Distance(tabColli[i].transform.position, transform.position) < minDistance))
                return;

            Interactible test = tabColli[i].GetComponent<Interactible>();
            if (test != null)
            {
                minDistance = Vector2.Distance(tabColli[i].transform.position, transform.position);
                nearestInteractible = test;
            }
        }

        if (nearestInteractible != null) 
            nearestInteractible.Interact();
    }
}
