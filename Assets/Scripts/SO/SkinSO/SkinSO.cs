using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinsSO", menuName = "SO/SkinsSO")]
public class SkinSO : ScriptableObject
{
    public SpriteDirection idle1, idle2, walk1, walk2;
}

[Serializable]
public class SpriteDirection
{
    public Sprite south, south_west, west, northWest, north;
}
