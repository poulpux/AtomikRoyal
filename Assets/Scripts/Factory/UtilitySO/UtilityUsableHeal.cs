using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityUsableHeal : UsableCDWMother
{

    protected override void Use()
    {
        print("heal");
        base.Use();
        infos.IncreaseLife(realSO.nbHeal);
    }
}
