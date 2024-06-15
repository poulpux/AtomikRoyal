using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour, IDesactiveWhenPlayerIsDead
{
    PlayerInfos infos;
    InteractibleMother nearestInteractible;

    public UnityEvent<bool> changeStateInteractibleEvent = new UnityEvent<bool>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void Awake()
    {
        infos = GetComponent<PlayerInfos>();
    }

    private void FixedUpdate()
    {
        print(nearestInteractible);
        if (infos.inputSystem.isInteracting && nearestInteractible != null)
            nearestInteractible?.Interact(infos);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactible"))
        {
            if(nearestInteractible == null)
            {
                nearestInteractible = collision.GetComponent<InteractibleMother>();
                return;
            }

            float dist = Vector2.Distance(nearestInteractible.transform.position, transform.position);
            if (Vector2.Distance(collision.transform.position, transform.position) < dist)
                nearestInteractible = collision.GetComponent<InteractibleMother>();
        }
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}