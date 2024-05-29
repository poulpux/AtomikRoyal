using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinsSO_filename", menuName = "SO/SkinsSO")]
public class SkinSO : ScriptableObject
{
    public List<SpriteDirection> idle = new List<SpriteDirection>(), walk = new List<SpriteDirection>();
}

[Serializable]
public class SpriteDirection
{
    public Sprite south, south_west, west, northWest, north;
}
