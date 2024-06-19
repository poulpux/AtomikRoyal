using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RingGestion : MonoBehaviour
{
    static public List<RingZone> theoreticalZone { get; private set; } = new List<RingZone>();

    [SerializeField] private List<GameObject> _concretZone;
    [HideInInspector] public List<GameObject> concretZone { get { return _concretZone; } private set { _concretZone = value; } }

    private List<ZoneMakeDamage> listDamageZone = new List<ZoneMakeDamage>();
    void Start()
    {
        InstantiateAll();
        SetZoneList();
        StartCoroutine(CloseRandomRingCoroutine());
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public int TryCloseRandomRing()
    {
        List<int> indexList = new List<int>();
        for (int i = 0; i < theoreticalZone.Count; i++)
        {
            if (theoreticalZone[i].state == RINGSTATE.FREE)
                indexList.Add(i);
        }

        if (indexList.Count == 1)
            return -1;

        int index = Random.Range(0, indexList.Count);

        TryCloseRing(theoreticalZone[indexList[index]].name);
        return index;
    }

    public void TryCloseRing(RINGNAME name)
    {
        if (theoreticalZone[(int)name].state == RINGSTATE.FREE)
            StartCoroutine(WillCloseRing(theoreticalZone[(int)name]));
    }

    public void OpenRing(RINGNAME name) //TO BE TESTED
    {
        theoreticalZone[(int)name].state = RINGSTATE.FREE;
        _concretZone[(int)name].SetActive(false);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InstantiateAll()
    {
        foreach (var item in _concretZone)
            listDamageZone.Add(item.GetComponent<ZoneMakeDamage>());
    }
    private void SetZoneList()
    {
        for (int i = 0; i < 9; i++)
        {
            RingZone zone = new RingZone();
            zone.name = (RINGNAME)i;
            zone.state = RINGSTATE.FREE;
            theoreticalZone.Add(zone);
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
        float timer = 0f;
        while (timer < _StaticRound.timeToCloseRing && zone.state == RINGSTATE.WILLCLOSE)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (zone.state == RINGSTATE.FREE)
            yield return null;
        zone.state = RINGSTATE.CLOSE;
        _StaticRound.CloseRingEvent.Invoke(zone.name);
        _concretZone[(int)zone.name].SetActive(true);
        print(zone.name);
    }
}
