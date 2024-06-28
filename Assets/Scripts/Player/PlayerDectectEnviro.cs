using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDectectEnviro : MonoBehaviour
{
    [Header("Ring")]
    private int ringMakeDamage;
    private UnityEvent<bool> onRingEnterExitEvent = new UnityEvent<bool> ();
    private void Start()
    {
        onRingEnterExitEvent.AddListener((enter) => OnRingEnterExit(enter));
    }

    private void OnRingEnterExit(bool enter)
    {
       if(ringMakeDamage == 0)
            StartCoroutine(MakeDamageRingCoroutine());
        ringMakeDamage += enter ? 1 : -1;
        if(ringMakeDamage == 0)
            StopCoroutine(MakeDamageRingCoroutine());
    }

    private IEnumerator MakeDamageRingCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_StaticRound.CDWTicDamage);
            GameManager.Instance.currentPlayer.DecreaseLife(_StaticRound.GetDamageOfZone(GameManager.Instance.ringGestion.nbZoneClosed), null);
            yield return new WaitForEndOfFrame();
        }
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
}
