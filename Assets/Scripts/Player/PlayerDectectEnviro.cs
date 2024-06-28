using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDectectEnviro : MonoBehaviour, IDesactiveWhenPlayerIsDead
{
    private DamageEnviro ring, fire, gaz;
    private NoDamageEnviro glue, water, bush;
    public UnityEvent<float> changeSpdModifEvent = new UnityEvent<float>();

    [HideInInspector] public bool isInBush;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void Start()
    {
        InstantiateRing();
        InstantiateFire();
        InstantiateGaz();
        InstantiateGlue();
        InstantiateWater();
        InstantiateBush();
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
        else if (collision.tag == _StaticEnvironement.bushTag)
            bush.onEnterExitEvent.Invoke(true);
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
        else if (collision.tag == _StaticEnvironement.bushTag)
            bush.onEnterExitEvent.Invoke(false);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InstantiateRing()
    {
        ring = new DamageEnviro();
        ring.damage = _StaticEnvironement.GetDamageOfZone(GameManager.Instance.ringGestion.nbZoneClosed);
        ring.onEnterExitEvent.AddListener((enter) => OnDamageEnviroEnterExit(enter, ref ring));
        ring.CDWTikDomage = _StaticEnvironement.CDWDamageRing;
        ring.attackLifeOnly = true;
    }

    private void InstantiateFire()
    {
        fire = new DamageEnviro();
        fire.damage = _StaticEnvironement.damageFire;
        fire.onEnterExitEvent.AddListener((enter) => OnDamageEnviroEnterExit(enter, ref fire));
        fire.CDWTikDomage = _StaticEnvironement.CDWDamageFire;
    }
    
    private void InstantiateGaz()
    {
        gaz = new DamageEnviro();
        gaz.damage = _StaticEnvironement.damageGaz;
        gaz.onEnterExitEvent.AddListener((enter) => OnDamageEnviroEnterExit(enter, ref gaz));
        gaz.CDWTikDomage = _StaticEnvironement.CDWDamageGaz;
        gaz.attackLifeOnly = true;
    }

    private void InstantiateGlue()
    {
        glue = new NoDamageEnviro();
        glue.onEnterExitEvent.AddListener((enter) => OnSlowEnviroEnterExit(enter, ref glue));
    }

    private void InstantiateWater()
    {
        water = new NoDamageEnviro();
        water.onEnterExitEvent.AddListener((enter) => OnSlowEnviroEnterExit(enter, ref water));
    }
    
    private void InstantiateBush()
    {
        bush = new NoDamageEnviro();
        bush.onEnterExitEvent.AddListener((enter) => { bush.nbCollision += enter ? 1 : -1; isInBush = bush.nbCollision > 0; });
    }

    private void OnSlowEnviroEnterExit(bool enter, ref NoDamageEnviro enviro)
    {
        enviro.nbCollision += enter ? 1 : -1;
        changeSpdModifEvent.Invoke(calculateSpdModif());
    }

    private void OnDamageEnviroEnterExit(bool enter,ref DamageEnviro enviro)
    {
        if (enviro.nbCollision == 0)
        {
            enviro.stopCoroutine = false;
            StartCoroutine(enviro.MakeDamageRingCoroutine());
        }
        enviro.nbCollision += enter ? 1 : -1;
        if (enviro.nbCollision == 0)
            enviro.stopCoroutine = true;
    }

    private float calculateSpdModif()
    {
        return glue.nbCollision > 0 ? _StaticPlayer.glueSpdModifier : water.nbCollision > 0 ? _StaticPlayer.waterSpdModifier : 1f;
    }

    public class NoDamageEnviro
    {
        public int nbCollision;
        public UnityEvent<bool> onEnterExitEvent = new UnityEvent<bool>();
    }

    public class DamageEnviro : NoDamageEnviro
    {
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
