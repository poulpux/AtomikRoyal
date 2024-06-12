using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : HitableByBombMother, IDesactiveWhenPlayerIsDead
{
    PlayerInfos infos;
    private void Awake()
    {
        infos = GetComponent<PlayerInfos>();
    }
    public void WhenDead()
    {
        this.enabled = false;
    }

    protected override void HitEffect(int damage)
    {
        base.HitEffect(damage);
        infos.TakeDomage(damage);
    }

    protected override bool HitCondtion()
    {
        return infos.isInvincibleList.Count == 0;
    }
}
