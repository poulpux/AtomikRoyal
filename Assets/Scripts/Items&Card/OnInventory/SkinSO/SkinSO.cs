using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATEANIM
{
    IDLE,
    WALK
}

[CreateAssetMenu(fileName = "SkinsSO_filename", menuName = "SO/SkinsSO")]
public class SkinSO : ScriptableObject
{
    public List<SpriteDirection> idle = new List<SpriteDirection>(), walk = new List<SpriteDirection>();
    public Sprite GetSprite(int currentSpriteDirection,int indexAnim, STATEANIM currentAnim)
    {
        List<SpriteDirection> currentAnimation =  currentAnim == STATEANIM.IDLE ? idle : walk;
        if(currentSpriteDirection == 0)
            return currentAnimation[indexAnim].south;
        else if (currentSpriteDirection == 1)
            return currentAnimation[indexAnim].south_west;
        else if (currentSpriteDirection == 2)
            return currentAnimation[indexAnim].west;
        else if (currentSpriteDirection == 3)
            return currentAnimation[indexAnim].northWest;
        else if (currentSpriteDirection == 4)
            return currentAnimation[indexAnim].north;

        return null;

    }
}

[Serializable]
public class SpriteDirection
{
    public Sprite south, south_west, west, northWest, north;
}
