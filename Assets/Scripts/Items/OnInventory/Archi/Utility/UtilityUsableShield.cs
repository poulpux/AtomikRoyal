using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityUsableShield : UsableCDWMother
{
    protected override void Use()
    {
        base.Use();
        infos.IncreaseShield(realSO.nbShield);
    }
}
