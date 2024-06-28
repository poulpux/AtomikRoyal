using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDectectEnviro : MonoBehaviour
{
    private MakeDamageEnviro ring, fire, gaz;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void Start()
    {
        InstantiateRing();
        InstantiateFire();
        InstantiateGaz();
        _StaticRound.CloseRingEvent.AddListener((ringName) => ring.damage = _StaticEnvironement.GetDamageOfZone(GameManager.Instance.ringGestion.nbZoneClosed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(_StaticEnvironement.enviroLayerName)) return;

        if (collision.tag == _StaticEnvironement.ringTag)
            ring.onEnterExitEvent.Invoke(true);
        else if (collision.tag == _StaticEnvironement.fireTag)
            fire.onEnterExitEvent.Invoke(true);
        else if (collision.tag == _StaticEnvironement.gazTag)
            gaz.onEnterExitEvent.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(_StaticEnvironement.enviroLayerName)) return;

        if (collision.tag == _StaticEnvironement.ringTag)
            ring.onEnterExitEvent.Invoke(false);
        else if (collision.tag == _StaticEnvironement.fireTag)
            fire.onEnterExitEvent.Invoke(false);
        else if (collision.tag == _StaticEnvironement.gazTag)
            gaz.onEnterExitEvent.Invoke(false);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InstantiateRing()
    {
        ring = new MakeDamageEnviro();
        ring.damage = _StaticEnvironement.GetDamageOfZone(GameManager.Instance.ringGestion.nbZoneClosed);
        ring.onEnterExitEvent.AddListener((enter) => OnRingEnterExit(enter, ref ring));
        ring.CDWTikDomage = _StaticEnvironement.CDWDamageRing;
        ring.attackLifeOnly = true;
    }

    private void InstantiateFire()
    {
        fire = new MakeDamageEnviro();
        fire.damage = _StaticEnvironement.damageFire;
        fire.onEnterExitEvent.AddListener((enter) => OnRingEnterExit(enter, ref fire));
        fire.CDWTikDomage = _StaticEnvironement.CDWDamageFire;
    }
    
    private void InstantiateGaz()
    {
        gaz = new MakeDamageEnviro();
        gaz.damage = _StaticEnvironement.damageGaz;
        gaz.onEnterExitEvent.AddListener((enter) => OnRingEnterExit(enter, ref gaz));
        gaz.CDWTikDomage = _StaticEnvironement.CDWDamageGaz;
        gaz.attackLifeOnly = true;
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
                if (timer > CDWTikDomage)
                {
                    timer = 0f;
                    if(attackLifeOnly)
                        GameManager.Instance.currentPlayer.DecreaseLife(damage, null);
                    else
                        GameManager.Instance.currentPlayer.TakeDamage(damage, null);
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
