using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMother : MonoBehaviour
{
    protected PlayerInfos infos;
    protected GameObject explosionPrefab;
    protected float baseDomage, radius;

    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float radius, Vector2 posToGo, float cdw = 0f)
    {
        this.infos = infos;
        this.baseDomage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.radius = radius;
    }
    
    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, float radius)
    {
        this.infos = infos;
        this.baseDomage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.radius = radius;
    }

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        explosion.GetComponent<Explosion>().Init(baseDomage, radius, infos);
    }
}
