using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMother : MonoBehaviour
{
    protected GameObject explosionPrefab;
    protected PlayerInfos infos;
    protected float baseDomage;

    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage, Vector2 posToGo, float cdw = 0f)
    {
        this.infos = infos;
        this.baseDomage = baseDomage;
        this.explosionPrefab = explosionPrefab;
    }
    
    public virtual void Init(PlayerInfos infos, GameObject explosionPrefab, float baseDomage)
    {
        this.infos = infos;
        this.baseDomage = baseDomage;
        this.explosionPrefab = explosionPrefab;
    }
}
