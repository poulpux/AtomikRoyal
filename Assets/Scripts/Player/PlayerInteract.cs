using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour, IDesactiveWhenPlayerIsDead
{
    PlayerInfos infos;
    InteractibleMother nearestInteractible;

    private bool VientDeQuitter = false;
    public UnityEvent<bool> changeStateInteractibleEvent = new UnityEvent<bool>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void Awake()
    {
        infos = GetComponent<PlayerInfos>();
    }

    private void FixedUpdate()
    {
        if (infos.inputSystem.isInteracting && nearestInteractible != null)
        {
            nearestInteractible?.Interact(infos);
            if(nearestInteractible == null)
                changeStateInteractibleEvent.Invoke(false);
        }

        //Pour que ça clignote pas dans la condition où tu quitte ton nea                     
        if (VientDeQuitter && nearestInteractible == null)
            changeStateInteractibleEvent.Invoke(false);
        VientDeQuitter = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactible"))
        {
            if(nearestInteractible == null)
            {
                nearestInteractible = collision.GetComponent<InteractibleMother>();
                changeStateInteractibleEvent.Invoke(true);
                return;
            }

            float dist = Vector2.Distance(nearestInteractible.transform.position, transform.position);
            if (Vector2.Distance(collision.transform.position, transform.position) < dist)
                nearestInteractible = collision.GetComponent<InteractibleMother>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (nearestInteractible != null &&  collision.gameObject == nearestInteractible.gameObject)
        {
            nearestInteractible = null;
            VientDeQuitter = true;
        }
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}