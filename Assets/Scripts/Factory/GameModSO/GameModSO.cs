using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModSO_filename", menuName = "SO/GameModSO")]
public class GameModSO : ScriptableObjectWithScript
{
    public GAMEMODE gameMode;
    public StaticChestSO chestSO;
}
