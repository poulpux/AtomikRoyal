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
        if (infos.inputSystem.isInteracting)
            Interact();
    }

    private void Interact()
    {
        Collider2D[] tabColli = Physics2D.OverlapCircleAll(transform.position, _StaticPlayer.rangeInteractible);
        float minDistance = 63f;
        Interactible nearestInteractible = null;

        for (int i = 0; i < tabColli.Length; i++)
        {
            float distance = Vector2.Distance(tabColli[i].transform.position, transform.position);
            Interactible test = tabColli[i].GetComponent<Interactible>();

            if (test != null && distance < minDistance)
            {
                minDistance = distance;
                nearestInteractible = test;
            }
        }

        if (nearestInteractible != null)
            nearestInteractible.Interact();
    }
}