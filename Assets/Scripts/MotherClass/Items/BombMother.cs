using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BombMother : HitableByBombMother
{
    protected PlayerInfos infos;
    protected GameObject explosionPrefab;
    protected EXPLOSIONSHAPE shape;
    protected float baseDamage, radius, lenght, thick;
    protected Rigidbody2D rb;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float radius, EXPLOSIONSHAPE shape, Vector2 posToGo, float cdw = 0f)
    {
        this.infos = infos;
        this.baseDamage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.radius = radius;
        this.shape = shape; 
    }
    
    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float radius, EXPLOSIONSHAPE shape)
    {
        this.infos = infos;
        this.baseDamage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.radius = radius;
        this.shape = shape;
    }
    
    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float lenght, float thick, EXPLOSIONSHAPE shape, Vector2 posToGo, float cdw = 0f)
    {
        this.infos = infos;
        this.baseDamage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.lenght = lenght;
        this.thick = thick;
        this.shape = shape;
        print("throw"+thick);
    }
    
    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float lenght, float thick, EXPLOSIONSHAPE shape)
    {
        this.infos = infos;
        this.baseDamage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.lenght = lenght;
        this.thick = thick;
        this.shape = shape;
    }

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        Explosion explosionScript = explosion.GetComponent<Explosion>();
        if (explosionScript == null) return;
        if(shape == EXPLOSIONSHAPE.CIRCLE || shape == EXPLOSIONSHAPE.SQUARE)
            explosionScript.Init(baseDamage, radius,shape, infos);
        else
            explosionScript.Init(baseDamage ,lenght ,thick ,shape, infos);
    }

    protected override void HitEffect(int damage, PlayerInfos offenser)
    {
        base.HitEffect(damage, offenser);
        Destroy(gameObject);
    }
}
