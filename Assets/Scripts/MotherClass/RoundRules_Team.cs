using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRules_Team : RoundManager
{
    [SerializeField] protected int nbPlayerSquad;
    List<List<PlayerInfos>> allTeams = new List<List<PlayerInfos>>();
    protected override bool EndCondition()
    {
        if (allTeams.Count<= 1)
            return true;
        return false;
    }

    protected override void RemovePlayer(PlayerInfos infos)
    {
        TryRemoveTeam(infos);
        base.RemovePlayer(infos);
    }

    protected void TryRemoveTeam(PlayerInfos infos)
    {
        foreach (var item in allTeams)
        {
            PlayerInfos playerCherch = item.Find(recherch => recherch == infos);
            if (playerCherch == null)
                break;

            bool allAreDead = true;
            foreach (var item1 in item)
            {
                if(!item1.isDead)
                    allAreDead = false;
            }

            if (allAreDead)
                allTeams.Remove(item);
            return;
        }
    }
}
