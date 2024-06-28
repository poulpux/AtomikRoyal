using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGazInteraction : EnviroMother
{
    public override void Interact(int x, int y)
    {
        base.Interact(x, y);
        EnviroManager.Instance.RemoveElementEvent.Invoke(x, y, ELEMENTS.GAZ);
        EnviroManager.Instance.AddElementEvent.Invoke(x, y, ELEMENTS.EXPLOSION);
        TryExpendExplosion(x + 1, y);
        TryExpendExplosion(x + 1, y + 1);
        TryExpendExplosion(x + 1, y - 1);
        TryExpendExplosion(x - 1, y);
        TryExpendExplosion(x - 1, y +1);
        TryExpendExplosion(x - 1, y - 1);
        TryExpendExplosion(x, y + 1);
        TryExpendExplosion(x, y - 1);
    }

    private void TryExpendExplosion(int y, int x)
    {
        if (GF.IsOnBinaryMask(EnviroManager.Instance.binaryMaskMap[x, y].binaryMask, (int)ELEMENTS.GAZ))
            EnviroManager.Instance.AddElementEvent.Invoke(x, y, ELEMENTS.EXPLOSION);
    }
}
