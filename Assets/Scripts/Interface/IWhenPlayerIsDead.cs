using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActiveWhenPlayerIsDead
{
    //Ne pas mettre de trucs dans l'Awake
    //Et mettre un ScriptActivateWhenPlayerDead dans lobjet
}

public interface IDesactiveWhenPlayerIsDead
{
    //Et mettre un ScriptActivateWhenPlayerDead dans lobjet
    void WhenDead();
}