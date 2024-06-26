using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGazInteraction : EnviroInteractionMother
{
    public override void Interact(int x, int y)
    {
        base.Interact(x, y);
        EnviroInteractionManager.Instance.RemoveElementEvent.Invoke(x, y, ELEMENTS.GAZ);
        EnviroInteractionManager.Instance.AddElementEvent.Invoke(x, y, ELEMENTS.EXPLOSION);
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
        if (GF.IsOnBinaryMask(EnviroInteractionManager.Instance.binaryMaskMap[x, y].binaryMask, (int)ELEMENTS.GAZ))
            EnviroInteractionManager.Instance.AddElementEvent.Invoke(x, y, ELEMENTS.EXPLOSION);
    }
}
