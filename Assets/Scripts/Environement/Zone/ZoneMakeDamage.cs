using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoneMakeDamage : MonoBehaviour
{
    public UnityEvent<bool> OnPlayerEnterOrExitEvent = new UnityEvent<bool>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPlayerEnterOrExitEvent.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnPlayerEnterOrExitEvent.Invoke(false);
    }
}
