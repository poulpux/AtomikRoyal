using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBombMother : BombMother
{
   
    protected float cdw;
    protected Vector2 posToGo;

    public override void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, Vector2 posToGo,float cdw = 0f)
    {
        base.Init(infos, explosionPrefab, baseDomage, posToGo, cdw);
        this.cdw = cdw;
        this.posToGo = posToGo;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.drag = _StaticPhysics.grenadeDrag;
        rb.AddForce(infos.inputSystem.mouseDirection * infos.throwForce, ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        Explosion explosionScipt = explosion.GetComponent<Explosion>();
        explosionScipt.baseDomage = baseDomage;
        explosionScipt.infos = infos;
    }
}