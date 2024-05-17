using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRules_Solo : RoundManager
{
    protected override void Awake()
    {
        base.Awake();

    }


    protected override bool EndCondition()
    {
        if (allPlayerAlife.Count <= 1)
            return true;
        return false;
    }
}
