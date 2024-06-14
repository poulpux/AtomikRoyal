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

    protected override void HitEffect(int damage, PlayerInfos offenser)
    {
        base.HitEffect(damage, offenser);
        infos.TakeDomage(damage, offenser);
    }

    protected override bool HitCondtion()
    {
        return infos.isInvincibleList.Count == 0;
    }
}
