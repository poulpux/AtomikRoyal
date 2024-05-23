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

        print("currentSpriteDirection : " + currentSpriteDirection);
        print("indexAnim : " + indexAnim);
        print("currentAnim : " + currentAnim);
    }

    private void SetSpriteDirection()
    {
        switch (direction)
        {
            case DIRECTION.SOUTH:
                currentSpriteDirection = 0;
                break;
            case DIRECTION.SOUTH_WEST:
            case DIRECTION.SOUTH_EAST:
                currentSpriteDirection = 1;
                break;
            case DIRECTION.WEST:
            case DIRECTION.EAST:
                currentSpriteDirection = 2;
                break;
            case DIRECTION.NORTH_WEST:
            case DIRECTION.NORTH_EAST:
                currentSpriteDirection = 3;
                break;
            case DIRECTION.NORTH:
                currentSpriteDirection = 4;
                break;
        }

        infos.spriteRenderer.flipX = direction == DIRECTION.SOUTH_EAST || direction == DIRECTION.EAST || direction == DIRECTION.NORTH_EAST;
    }

    private void AnimateSprite()
    {
        timerAnim += Time.deltaTime;

        STATEANIM lastAnim = currentAnim;

        currentAnim = infos.inputSystem.direction == Vector2.zero ? STATEANIM.IDLE : STATEANIM.WALK;

        if (lastAnim != currentAnim)
        {
            indexAnim = 0;
            timerAnim = 0f;
        }

        float animCooldown = currentAnim == STATEANIM.IDLE ? _StaticSkins.idleCldAnim : _StaticSkins.walkCldAnim;
        if (timerAnim > animCooldown)
        {
            timerAnim = 0f;
            indexAnim = (indexAnim + 1) % 3;
        }

        // En attendant
        // infos.spriteRenderer.sprite = currentSkin.GetSprite(currentSpriteDirection, indexAnim, currentAnim);
    }

    private void SetDirection()
    {
        Vector2 directionInput = infos.inputSystem.direction;

        if (directionInput.x > 0)
        {
            direction = directionInput.y > 0 ? DIRECTION.NORTH_EAST :
                        directionInput.y < 0 ? DIRECTION.SOUTH_EAST : DIRECTION.EAST;
        }
        else if (directionInput.x < 0)
        {
            direction = directionInput.y > 0 ? DIRECTION.NORTH_WEST :
                        directionInput.y < 0 ? DIRECTION.SOUTH_WEST : DIRECTION.WEST;
        }
        else
        {
            direction = directionInput.y > 0 ? DIRECTION.NORTH :
                        directionInput.y < 0 ? DIRECTION.SOUTH : direction;
        }
    }
}