using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestructionInteraction : EnviroMother
{
    [SerializeField] private ELEMENTS elementToDestruct;
    public override void Interact(int x, int y)
    {
        base.Interact(x, y);
        EnviroManager.Instance.RemoveElementEvent.Invoke(x, y, elementToDestruct);
    }
}
