using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDectectEnviro : MonoBehaviour
{
    [Header("Ring")]
    private int ringMakeDamage;
    private UnityEvent<bool> onRingEnterExitEvent = new UnityEvent<bool> ();
    private bool closeRingCoroutine;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void Start()
    {
        onRingEnterExitEvent.AddListener((enter) => OnRingEnterExit(enter));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("EnviroPlayer")) return;

        if (collision.tag == "Ring")
            onRingEnterExitEvent.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("EnviroPlayer")) return;

        if (collision.tag == "Ring")
            onRingEnterExitEvent.Invoke(false);
    }

    private void OnRingEnterExit(bool enter)
    {
        if (ringMakeDamage == 0)
        {
            closeRingCoroutine = false;
            StartCoroutine(MakeDamageRingCoroutine());
        }
        ringMakeDamage += enter ? 1 : -1;
        if (ringMakeDamage == 0)
            closeRingCoroutine = true;

        print(ringMakeDamage);

    }

    private IEnumerator MakeDamageRingCoroutine()
    {
        float timer = 0f;
        while (!closeRingCoroutine)
        {
            timer += Time.deltaTime;
            if (timer > _StaticRound.CDWTicDamage)
            {
                timer = 0f;
                GameManager.Instance.currentPlayer.DecreaseLife(_StaticRound.GetDamageOfZone(GameManager.Instance.ringGestion.nbZoneClosed), null);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
