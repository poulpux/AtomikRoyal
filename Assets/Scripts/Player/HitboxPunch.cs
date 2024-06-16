using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitboxPunch : MonoBehaviour
{
    bool findPunch;
    private List<GameObject> touchedList = new List<GameObject>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void Start()
    {
        StartCoroutine(CherchPunch());
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!findPunch) return;

        print("toucheqqc " + collision.name);
        if (touchedList.Contains(collision.gameObject)) return;
        touchedList.Add(collision.gameObject);
        HitableByBombMother hit = collision.gameObject.GetComponentInParent<HitableByBombMother>();
        if (hit != null)
        {
            print(hit.gameObject);
            hit.GetHit((int)GameManager.Instance.currentPlayer.stats[(int)PLAYERSTATS.DMGCAC], GameManager.Instance.currentPlayer);
        }
    }

    public IEnumerator CherchPunch()
    {
        while (GameManager.Instance.currentPlayer == null || GameManager.Instance.currentPlayer.inventory.inventory.Count == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        GameManager.Instance.currentPlayer.inventory.inventory[0].UseEvent.AddListener(() => touchedList = new List<GameObject>());
        findPunch = true;
    }
}
