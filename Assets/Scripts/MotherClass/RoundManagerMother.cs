using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMEMODE
{
    SOLO,
    DUO,
    TRIO,
    QUATUOR,
    BLUEVSRED
}

public abstract class RoundManagerMother : SingletonMother<RoundManagerMother>
{
    protected List<PlayerInfos> allPlayerAlife = new List<PlayerInfos>();
    public Action gameStartEvent;
    public Action gameEndEvent;

    protected override void Awake()
    {
        base.Awake();
        gameStartEvent += MakeChestSpawn;
    }

    private void MakeChestSpawn()
    {
        foreach (var item in _StaticChest.AllChestPos)
        {
            //MakeSpawnChest
        }
    }

    protected virtual bool EndCondition()
    {
        return false;
    }

    public void AddPlayer(PlayerInfos infos)
    {
        infos.isDeadEvent.AddListener((infos)=>RemovePlayer(infos));
        allPlayerAlife.Add(infos);
    }

    protected virtual void RemovePlayer(PlayerInfos infos)
    {
        allPlayerAlife.Remove(infos);
        if (EndCondition())
            gameEndEvent.Invoke();
    }
}
