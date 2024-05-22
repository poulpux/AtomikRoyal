using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuel : MonoBehaviour
{
    public enum DIRECTION
    {
        SOUTH,
        SOUTH_EAST,
        EAST,
        NORTH_EAST,
        NORTH,
        NORTH_WEST,
        WEST,
        SOUTH_WEST
    }

    public enum STATEANIM
    {
        IDLE,
        WALK
    }

    private PlayerInfos infos;
    private SkinSO currentSkin;
    private int currentSpriteDirection;
    private DIRECTION direction;

    private float timerAnim;
    private int indexAnim;
    private STATEANIM currentAnim;

    void Start()
    {
        infos = GetComponent<PlayerInfos>();
        currentSkin = _StaticSkins.allSkins[PlayerPrefs.GetInt("skin")];
    }

    void Update()
    {
        SetDirection();
        SetSpriteDirection();
        AnimateSprite();
    }

    private void SetSpriteDirection()
    {
        if (direction == DIRECTION.SOUTH)
            currentSpriteDirection = 0;
        else if(direction == DIRECTION.SOUTH_WEST || direction == DIRECTION.SOUTH_EAST)
            currentSpriteDirection = 1;
        else if (direction == DIRECTION.WEST || direction == DIRECTION.EAST)
            currentSpriteDirection = 2;
        else if (direction == DIRECTION.NORTH_WEST || direction == DIRECTION.NORTH_EAST)
            currentSpriteDirection = 3;
        else if(direction == DIRECTION.NORTH)
            currentSpriteDirection = 4;

        if(direction == DIRECTION.SOUTH_EAST || direction == DIRECTION.EAST || direction == DIRECTION.NORTH_EAST)
            infos.spriteRenderer.flipX = true;
        else
            infos.spriteRenderer.flipX = false;
    }

    private void AnimateSprite()
    {
        timerAnim += Time.deltaTime;

        STATEANIM lastAnim = currentAnim;

        if (infos.inputSystem.direction == Vector2.zero)
            currentAnim = STATEANIM.IDLE;
        else
            currentAnim = STATEANIM.WALK;

        if (lastAnim != currentAnim)
        {
            indexAnim = 0;
            timerAnim = 0f;
        }

        if(currentAnim == STATEANIM.IDLE && timerAnim > _StaticSkins.idleCldAnim)
        {
            timerAnim = 0f;
            indexAnim = indexAnim + 1 == 3 ? 0 : indexAnim + 1;
        }
        else if(currentAnim == STATEANIM.WALK && timerAnim > _StaticSkins.walkCldAnim)
        {
            timerAnim = 0f;
            indexAnim = indexAnim + 1 == 3 ? 0 : indexAnim + 1;
        }

        //En attendant

        //if (currentSpriteDirection == 0)
        //    infos.spriteRenderer.sprite = currentSkin.idle[indexAnim].south;
        //else if(currentSpriteDirection == 1)
        //    infos.spriteRenderer.sprite = currentSkin.idle[indexAnim].south_west;
        //else if (currentSpriteDirection == 2)
        //    infos.spriteRenderer.sprite = currentSkin.idle[indexAnim].west;
        //else if (currentSpriteDirection == 3)
        //    infos.spriteRenderer.sprite = currentSkin.idle[indexAnim].northWest;
        //else if (currentSpriteDirection == 4)
        //    infos.spriteRenderer.sprite = currentSkin.idle[indexAnim].north;

        print("index anim : "+indexAnim);
        print("CurrentSpriteDirection : "+currentSpriteDirection);
        print("currentAnim : " + currentAnim);
    }

    private void SetDirection()
    {
        if(infos.inputSystem.direction.x > 0)
        {
            if (infos.inputSystem.direction.y > 0)
                direction = DIRECTION.NORTH_EAST;
            else if (infos.inputSystem.direction.y < 0)
                direction = DIRECTION.SOUTH_EAST;
            else
                direction = DIRECTION.EAST;
        }
        else if(infos.inputSystem.direction.x < 0)
        {
            if (infos.inputSystem.direction.y > 0)
                direction = DIRECTION.NORTH_WEST;
            else if (infos.inputSystem.direction.y < 0)
                direction = DIRECTION.SOUTH_WEST;
            else
                direction = DIRECTION.WEST;
        }
        else
        {
            if (infos.inputSystem.direction.y > 0)
                direction = DIRECTION.NORTH;
            else if((infos.inputSystem.direction.y < 0))
                direction = DIRECTION.SOUTH;
        }
    }
}
