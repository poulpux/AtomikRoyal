using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDectectEnviro : MonoBehaviour, IDesactiveWhenPlayerIsDead
{
    private MakeDamageEnviro ring, fire, gaz;
    private EnviroSlowPlayer glue, water;
    public UnityEvent<float> changeSpdModifEvent = new UnityEvent<float>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void Start()
    {
        InstantiateRing();
        InstantiateFire();
        InstantiateGaz();
        InstantiateGlue();
        InstantiateWater();
        //Exeption pour les dégâts du ring qui changent
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
        else if (collision.tag == _StaticEnvironement.glueTag)
            glue.onEnterExitEvent.Invoke(true);
        else if (collision.tag == _StaticEnvironement.waterTag)
            water.onEnterExitEvent.Invoke(true);
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
        else if (collision.tag == _StaticEnvironement.glueTag)
            glue.onEnterExitEvent.Invoke(false);
        else if (collision.tag == _StaticEnvironement.waterTag)
            water.onEnterExitEvent.Invoke(false);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InstantiateRing()
    {
        ring = new MakeDamageEnviro();
        ring.damage = _StaticEnvironement.GetDamageOfZone(GameManager.Instance.ringGestion.nbZoneClosed);
        ring.onEnterExitEvent.AddListener((enter) => OnDamageEnviroEnterExit(enter, ref ring));
        ring.CDWTikDomage = _StaticEnvironement.CDWDamageRing;
        ring.attackLifeOnly = true;
    }

    private void InstantiateFire()
    {
        fire = new MakeDamageEnviro();
        fire.damage = _StaticEnvironement.damageFire;
        fire.onEnterExitEvent.AddListener((enter) => OnDamageEnviroEnterExit(enter, ref fire));
        fire.CDWTikDomage = _StaticEnvironement.CDWDamageFire;
    }
    
    private void InstantiateGaz()
    {
        gaz = new MakeDamageEnviro();
        gaz.damage = _StaticEnvironement.damageGaz;
        gaz.onEnterExitEvent.AddListener((enter) => OnDamageEnviroEnterExit(enter, ref gaz));
        gaz.CDWTikDomage = _StaticEnvironement.CDWDamageGaz;
        gaz.attackLifeOnly = true;
    }
    private void InstantiateGlue()
    {
        glue = new EnviroSlowPlayer();
        glue.onEnterExitEvent.AddListener((enter) => OnSlowEnviroEnterExit(enter, ref glue));
    }
    private void InstantiateWater()
    {
        water = new EnviroSlowPlayer();
        water.onEnterExitEvent.AddListener((enter) => OnSlowEnviroEnterExit(enter, ref water));
    }

    private void OnSlowEnviroEnterExit(bool enter, ref EnviroSlowPlayer enviro)
    {
        enviro.nbCollision += enter ? 1 : -1;
        changeSpdModifEvent.Invoke(calculateSpdModif());
    }

    private void OnDamageEnviroEnterExit(bool enter,ref MakeDamageEnviro enviro)
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

    private float calculateSpdModif()
    {
        return glue.nbCollision > 0 ? _StaticPlayer.glueSpdModifier : water.nbCollision > 0 ? _StaticPlayer.waterSpdModifier : 1f;
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

    public class EnviroSlowPlayer
    {
        public int nbCollision;
        public UnityEvent<bool> onEnterExitEvent = new UnityEvent<bool>();
    }

}
