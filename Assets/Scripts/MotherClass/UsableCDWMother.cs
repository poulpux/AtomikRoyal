using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UsableCDWMother : UtilityUsable
{
    private float timer;
    private int cursor;
    protected override void Start()
    {
        base.Start();
        realSO = SO as UtilityUsableSO;
        infos.GetCancelEvent.AddListener(() => CancelRecovery());
    }

    override public void TryUse()
    {
        if (timer != 0) return;
        cursor = infos.inventory.cursorPos;
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
        while (!UseCondition() && !infos.isMoving && infos.inventory.cursorPos != cursor)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        timer = 0f;
        if (!infos.isMoving && infos.inventory.cursorPos != cursor)
            Use();
    }

    private void CancelRecovery()
    {
        StopCoroutine(UseCoroutine());
        timer = 0f;
    }

}
