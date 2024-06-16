using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATEANIM
{
    IDLE,
    WALK
}

public enum TYPEOFANIMECDW
{
    DEFAULT,
    CUSTOMISE
}

[CreateAssetMenu(fileName = "SkinsSO_filename", menuName = "SO/SkinsSO")]
public class SkinSO : ScriptableObject
{
    public string namee;
    public Vector3 position, scale;
    public List<SpriteDirection> idle = new List<SpriteDirection>(), walk = new List<SpriteDirection>();
    [SerializeField] private TYPEOFANIMECDW typeAnimCDW;
    [ConditionalField("typeAnimCDW", TYPEOFANIMECDW.CUSTOMISE, "==")]
    public float idleCldAnim = 0.5f, walkCldAnim = 0.3f;
    
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

    public float GetCDWAnimation(STATEANIM currentAnim)
    {
        if(currentAnim == STATEANIM.IDLE)
        {
            if (typeAnimCDW == TYPEOFANIMECDW.DEFAULT)
                return _StaticSkins.idleCldAnim;
            else
                return idleCldAnim;
        }
        else if(currentAnim == STATEANIM.WALK)
        {
            if(typeAnimCDW == TYPEOFANIMECDW.DEFAULT)
                return _StaticSkins.walkCldAnim;
            else
                return walkCldAnim;
        }

        Debug.LogError("Tu as ajouté un STATEANIM, il faut compléter");
        return 1f;
    }
}

[Serializable]
public class SpriteDirection
{
    public Sprite south, south_west, west, northWest, north;
}
