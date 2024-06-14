using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBombMother : BombMother
{
   
    protected float cdw;
    protected Vector2 posToGo;

    public override void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float radius, EXPLOSIONSHAPE shape, Vector2 posToGo,float cdw = 0f)
    {
        base.Init(infos, explosionPrefab, baseDomage, radius, shape, posToGo, cdw);
        this.cdw = cdw;
        this.posToGo = posToGo;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.drag = _StaticPhysics.grenadeDrag;
        rb.AddForce(infos.inputSystem.mouseDirection * infos.stats[(int)PLAYERSTATS.THROWFORCE], ForceMode2D.Impulse);
    }
    
    public override void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float lenght, float thick, EXPLOSIONSHAPE shape, Vector2 posToGo,float cdw = 0f)
    {
        base.Init(infos, explosionPrefab, baseDomage, lenght, thick, shape, posToGo, cdw);
        this.cdw = cdw;
        this.posToGo = posToGo;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.drag = _StaticPhysics.grenadeDrag;
        rb.AddForce(infos.inputSystem.mouseDirection * infos.stats[(int)PLAYERSTATS.THROWFORCE], ForceMode2D.Impulse);
    }
}
