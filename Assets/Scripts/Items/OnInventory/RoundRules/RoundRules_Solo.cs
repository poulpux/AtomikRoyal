using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRules_Solo : RoundManagerMother
{
    protected override bool EndCondition()
    {
        if (allPlayerAlife.Count <= 1)
            return true;
        return false;
    }
}
