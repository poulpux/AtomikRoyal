using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IDesactiveWhenPlayerIsDead
{
    PlayerInfos infos;

    void Start()
    {
        infos = GetComponent<PlayerInfos>();
    }

    void Update()
    {
        TryInteract();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void TryInteract()
    {
        if (infos.inputSystem.isInteracting)
            Interact();
    }

    private void Interact()
    {
        Collider2D[] tabColli = Physics2D.OverlapCircleAll(transform.position, _StaticPlayer.rangeInteractible);
        float minDistance = 63f;
        InteractibleMother nearestInteractible = null;

        for (int i = 0; i < tabColli.Length; i++)
        {
            float distance = Vector2.Distance(tabColli[i].transform.position, transform.position);
            InteractibleMother test = tabColli[i].GetComponent<InteractibleMother>();

            if (test != null && distance < minDistance)
            {
                minDistance = distance;
                nearestInteractible = test;
            }
        }

        if (nearestInteractible != null)
            nearestInteractible.Interact(infos);
    }

    public void WhenDead() { }
}