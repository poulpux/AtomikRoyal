using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateInTabEditor : MonoBehaviour
{
    [SerializeField] private ELEMENTS type;

    void Start()
    {
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        EnviroManager.Instance.AddElementEvent.Invoke((int)(transform.localPosition.x - _StaticEnvironement.originX)+1, (int)(transform.localPosition.y - _StaticEnvironement.originY)+1, type);
        Destroy(gameObject, 0.1f);
    }
}
