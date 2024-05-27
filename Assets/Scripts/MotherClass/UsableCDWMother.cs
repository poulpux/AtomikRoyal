using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UsableCDWMother : UtilityUsable
{
    protected UtilityUsableSO realSO;
    private float timer;
    private void Start()
    {
        realSO = SO as UtilityUsableSO;
        infos = GetComponent<PlayerInfos>();
    }

    override public void TryUse()
    {
        if (timer != 0) return;
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
        while (!UseCondition() && !infos.isMoving)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (!infos.isMoving)
            Use();
        timer = 0f;
    }

}