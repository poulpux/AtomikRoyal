using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IActiveWhenPlayerIsDead
{
    //Ne pas mettre de trucs dans l'Awake
    //Et mettre un WhenPlayerDied dans lobjet
}

public interface IDesactiveWhenPlayerIsDead
{
    //Et mettre un WhenPlayerDied dans lobjet
}

public interface IActWhenPlayerIsDead
{
    //Joue bien une action avant de désactiver
    void WhenDied();
}

public interface IActWhenPlayerSpawn
{
    //Joue bien une action avant de désactiver
    void WhenSpawn();
}