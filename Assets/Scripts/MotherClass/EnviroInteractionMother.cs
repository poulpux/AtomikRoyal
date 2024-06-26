using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnviroInteractionMother : MonoBehaviour
{
    public List<ELEMENTS> typeDisable = new List<ELEMENTS>();

    protected virtual void Start()
    {
        StartCoroutine(WaitOneFrame());
    }

    public virtual void Interact(int x, int y)
    {

    }

    private IEnumerator WaitOneFrame()
    {
        yield return new WaitForEndOfFrame();
        typeDisable = new List<ELEMENTS>();
    }
}
