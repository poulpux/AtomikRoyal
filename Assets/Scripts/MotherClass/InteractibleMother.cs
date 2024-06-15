using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleMother : MonoBehaviour
{
    [SerializeField] protected float timeToInteract = 0.2f;
    private float timer;

    public void Interact(PlayerInfos infos)
    {
        //StopCoroutine(ResetCoroutine());
        //StartCoroutine(ResetCoroutine());

        timer += Time.deltaTime;
        if(timer > timeToInteract )
        {
            timer = 0;
            Use(infos);
        }
    }

    protected virtual void Use(PlayerInfos infos)
    {
        infos.inputSystem.needToGoUpInteractEvent.Invoke();
    }

    private IEnumerator ResetCoroutine()
    {
        float timerC = 0f;
        while (timerC < _StaticPlayer.timeInteractibleBecomeZero)
        {
            timerC += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        print("end coroutine");
        timer = 0f;
    }
}
