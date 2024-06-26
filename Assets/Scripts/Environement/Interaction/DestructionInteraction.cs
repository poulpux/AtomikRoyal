using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestructionInteraction : EnviroInteractionMother
{
    [SerializeField] private ELEMENTS elementToDestruct;
    public override void Interact(int x, int y)
    {
        base.Interact(x, y);
        GF.RemoveInBinaryMask(ref EnviroInteractionManager.Instance.binaryMaskMap[x, y].binaryMask, (int)elementToDestruct);
    }
}
