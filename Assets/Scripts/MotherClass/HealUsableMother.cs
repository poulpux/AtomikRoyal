using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class HealUsableMother : UtilityUsable
{
    UtilityUsableSO realSO;
    PlayerInfos infos;
    private float timer;
    private void Start()
    {
        realSO = GetComponent<UtilityUsableSO>();
        infos = GetComponent<PlayerInfos>();
    }

    override public void TryUse()
    {
        StopCoroutine(UseCoroutine());
        StartCoroutine(UseCoroutine());
        base.TryUse();
    }
    override protected void Use()
    {
        base.Use();
    }

    override protected bool UseCondition()
    {
        return timer > realSO.timeUse;
    }

    protected IEnumerator UseCoroutine()
    {
        while (!UseCondition() || !infos.isMoving)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (infos.isMoving)
            timer = 0;
        else
            Use();
    }

}
