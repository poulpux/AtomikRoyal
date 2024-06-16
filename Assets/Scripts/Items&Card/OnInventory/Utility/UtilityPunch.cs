using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityPunch : UtilityUsableMother
{
    private bool canPunch = true;
    private GameObject punchPivot;

    protected override void Start()
    {
        base.Start();
        punchPivot = FindChildStartingWith("Punch");
        punchPivot.SetActive(false);
    }

    protected override void Use()
    {
        base.Use();
        StartCoroutine(CDWCoroutine());
        StartCoroutine(DuraCoroutine());
    }

    protected override bool UseCondition()
    {
        return canPunch;
    }

    private IEnumerator DuraCoroutine()
    {
        punchPivot.SetActive(true);
        canPunch = false; 

        float angle = Mathf.Atan2(infos.inputSystem.mouseDirection.y, infos.inputSystem.mouseDirection.x) * Mathf.Rad2Deg;
        punchPivot.transform.eulerAngles = new Vector3(0, 0, angle - 90f);

        yield return new WaitForSeconds(_StaticPlayer.puchDura);
        punchPivot.SetActive(false);
    }

    private IEnumerator CDWCoroutine()
    {
        yield return new WaitForSeconds(_StaticPlayer.punchCDW);
        canPunch = true;
    }

    GameObject FindChildStartingWith(string nameStart)
    {
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith(nameStart))
                return child.gameObject;
        }

        return null;
    }
}
