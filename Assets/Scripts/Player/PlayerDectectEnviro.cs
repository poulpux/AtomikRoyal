using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDectectEnviro : MonoBehaviour
{
    [Header("Ring")]
    private MakeDamageEnviro ring, fire, gaz;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void Start()
    {
        InstantiateRing();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(_StaticEnvironement.enviroLayerName)) return;

        if (collision.tag == _StaticEnvironement.ringTag)
            ring.onEnterExitEvent.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(_StaticEnvironement.enviroLayerName)) return;

        if (collision.tag == _StaticEnvironement.ringTag)
            ring.onEnterExitEvent.Invoke(false);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InstantiateRing()
    {
        ring = new MakeDamageEnviro();
        ring.damage = _StaticRound.GetDamageOfZone(GameManager.Instance.ringGestion.nbZoneClosed);
        ring.onEnterExitEvent.AddListener((enter) => OnRingEnterExit(enter, ref ring));
        ring.CDWTikDomage = _StaticRound.CDWTicDamage;
        ring.attackLifeOnly = true;
    }

    private void InstantiateFire()
    {
        fire = new MakeDamageEnviro();
    }

    private void OnRingEnterExit(bool enter,ref MakeDamageEnviro enviro)
    {
        if (enviro.nbMakeDamage == 0)
        {
            enviro.stopCoroutine = false;
            StartCoroutine(enviro.MakeDamageRingCoroutine());
        }
        enviro.nbMakeDamage += enter ? 1 : -1;
        if (enviro.nbMakeDamage == 0)
            enviro.stopCoroutine = true;
    }

    public class MakeDamageEnviro
    {
        public int nbMakeDamage;
        public UnityEvent<bool> onEnterExitEvent = new UnityEvent<bool>();
        public bool stopCoroutine, attackLifeOnly;
        public float CDWTikDomage;
        public int damage;

        public IEnumerator MakeDamageRingCoroutine()
        {
            float timer = 0f;
            while (!stopCoroutine)
            {
                timer += Time.deltaTime;
                if (timer > _StaticRound.CDWTicDamage)
                {
                    timer = 0f;
                    if(attackLifeOnly)
                        GameManager.Instance.currentPlayer.DecreaseLife(_StaticRound.GetDamageOfZone(GameManager.Instance.ringGestion.nbZoneClosed), null);
                    else
                        GameManager.Instance.currentPlayer.TakeDamage(_StaticRound.GetDamageOfZone(GameManager.Instance.ringGestion.nbZoneClosed), null);
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
