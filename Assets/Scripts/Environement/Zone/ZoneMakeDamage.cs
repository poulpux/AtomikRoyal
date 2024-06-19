using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoneMakeDamage : MonoBehaviour
{
    [HideInInspector] public PlayerInfos infos = new PlayerInfos();
    public UnityEvent OnPlayerEnterEvent = new UnityEvent();
    public UnityEvent OnPlayerExitEvent = new UnityEvent();

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            infos = GameManager.Instance.currentPlayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            infos = null;
        }
    }
}
