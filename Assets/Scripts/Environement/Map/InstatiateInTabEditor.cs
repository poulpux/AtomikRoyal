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
        Vector2Int positionInTab = GF.EnterRealPositionInEnviroTab(transform.position);
        EnviroManager.Instance.AddElementEvent.Invoke(positionInTab.x, positionInTab.y, type);
        Destroy(gameObject, 0.1f);
    }
}
