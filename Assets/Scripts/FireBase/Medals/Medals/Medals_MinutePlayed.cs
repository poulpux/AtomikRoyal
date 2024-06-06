using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medals_MinutePlayed : MedalsMother
{
    void Start()
    {
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            infos.allMedals_Stats.minutePlayed++;
            print(infos.allMedals_Stats.minutePlayed);
        }
    }
}
