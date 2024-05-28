using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBombMother : MonoBehaviour
{
    protected GameObject explosionPrefab;
    protected PlayerInfos infos;
    protected float baseDomage, cdw;
    protected Vector2 posToGo;

    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, Vector2 posToGo,float cdw = 0f)
    {
        this.infos = infos;
        this.baseDomage = baseDomage;
        this.explosionPrefab = explosionPrefab;
        this.cdw = cdw;

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
