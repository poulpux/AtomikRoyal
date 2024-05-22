using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] protected float timeToInteract;
    private float timer;

    public void Interact()
    {
        print(gameObject);
        StopCoroutine(ResetCoroutine());
        StartCoroutine(ResetCoroutine());

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

    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(_StaticPlayer.timeInteractibleBecomeZero);
        timer = 0f;
    }
}
