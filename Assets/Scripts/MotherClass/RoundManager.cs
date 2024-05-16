using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoundManager : Singleton<RoundManager>
{
    List<PlayerInfos> allPlayer = new List<PlayerInfos>();
    List<Vector2> allChestPos = new List<Vector2>();
    public Action gameStartEvent;
    public Action gameEndEvent;
    protected virtual void Start()
    {
        gameStartEvent += MakeChestSpawn;
    }

    private void MakeChestSpawn()
    {

    }

    protected virtual bool EndCondition()
    {
        return false;
    }

    public void AddPlayer(PlayerInfos infos)
    {
        infos.isDeadEvent += RemovePlayer;
        allPlayer.Add(infos);
    }

    private void RemovePlayer(PlayerInfos infos)
    {
        allPlayer.Remove(infos);
    }
}
