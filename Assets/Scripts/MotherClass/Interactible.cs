using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    [SerializeField] protected float timeToInteract;
    private float timer;
    public void Interact()
    {
        timer += Time.deltaTime;
        if(timer > timeToInteract )
        {
            timer = 0;
            Use();
        }
    }

    protected virtual void Use()
    {

    }
}
