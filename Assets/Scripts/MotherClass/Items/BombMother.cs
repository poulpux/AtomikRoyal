using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMother : MonoBehaviour
{
    protected PlayerInfos infos;
    protected GameObject explosionPrefab;
    protected EXPLOSIONSHAPE shape;
    protected float baseDomage, radius, lenght, thick;

    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float radius, EXPLOSIONSHAPE shape, Vector2 posToGo, float cdw = 0f)
    {
        this.infos = infos;
        this.baseDomage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.radius = radius;
        this.shape = shape; 
    }
    
    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float radius, EXPLOSIONSHAPE shape)
    {
        this.infos = infos;
        this.baseDomage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.radius = radius;
        this.shape = shape;
    }
    
    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float lenght, float thick, EXPLOSIONSHAPE shape, Vector2 posToGo, float cdw = 0f)
    {
        this.infos = infos;
        this.baseDomage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.lenght = lenght;
        this.thick = thick;
        this.shape = shape;
    }
    
    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float lenght, float thick, EXPLOSIONSHAPE shape)
    {
        this.infos = infos;
        this.baseDomage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.lenght = lenght;
        this.thick = thick;
        this.shape = shape;
    }

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        if(shape == EXPLOSIONSHAPE.CIRCLE || shape == EXPLOSIONSHAPE.SQUARE)
            explosion.GetComponent<Explosion>().Init(baseDomage, radius,shape, infos);
        else
            explosion.GetComponent<Explosion>().Init(baseDomage ,lenght ,thick ,shape, infos);
    }
}
