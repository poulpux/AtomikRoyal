using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RingGestion : MonoBehaviour
{
    static public List<RingZone> zones = new List<RingZone>();

    void Start()
    {
        SetZoneList();
        StartCoroutine(CloseRandomRingCoroutine());
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public int TryCloseRandomRing()
    {
        List<int> indexList = new List<int>();
        for (int i = 0; i < zones.Count; i++)
        {
            if (zones[i].state == RINGSTATE.FREE)
                indexList.Add(i);
        }

        if (indexList.Count == 0)
            return -1;

        int index = Random.Range(0, indexList.Count);

        TryCloseRing(zones[indexList[index]].name);
        return index;
    }

    public void TryCloseRing(RINGNAME name)
    {
        if (zones[(int)name].state == RINGSTATE.FREE)
            StartCoroutine(WillCloseRing(zones[(int)name]));
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void SetZoneList()
    {
        for (int i = 0; i < 7; i++)
        {
            RingZone zone = new RingZone();
            zone.name = (RINGNAME)i;
            zone.state = RINGSTATE.FREE;
            zones.Add(zone);
        }
    }

    private IEnumerator CloseRandomRingCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_StaticRound.closeRingCooldown);
            if (TryCloseRandomRing() == -1)
                break;
        }
    }
    private IEnumerator WillCloseRing(RingZone zone)
    {
        zone.state = RINGSTATE.WILLCLOSE;
        _StaticRound.WillCloseRingEvent.Invoke(zone.name);
        yield return new WaitForSeconds(_StaticRound.timeToCloseRing);
        zone.state = RINGSTATE.CLOSE;
        _StaticRound.CloseRingEvent.Invoke(zone.name);
    }
}
