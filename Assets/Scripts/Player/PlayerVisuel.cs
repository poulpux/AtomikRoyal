using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuel : MonoBehaviour, IActWhenPlayerIsDead, IActWhenPlayerSpawn
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

    private PlayerInfos infos;
    private SkinSO currentSkin;
    private int currentSpriteDirection;
    private DIRECTION direction;

    private float timerAnim;
    private int indexAnim;
    private STATEANIM currentAnim;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void Awake()
    {
        infos = GetComponent<PlayerInfos>();
    }

    public void WhenSpawn()
    {
        currentSkin = _StaticSkins.allSkins[PlayerPrefs.GetInt("skin")];
    }
    public void WhenDied()
    {
        currentSkin = _StaticSkins.allSkins[0];
    }

    void FixedUpdate()
    {
        SetDirection();
        SetSpriteDirection();
        AnimateSprite();
    }


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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

        //Gère les changements d'animation
        STATEANIM lastAnim = currentAnim;

        currentAnim = infos.inputSystem.direction == Vector2.zero ? STATEANIM.IDLE : STATEANIM.WALK;

        if (lastAnim != currentAnim)
        {
            indexAnim = 0;
            timerAnim = 0f;
        }

        float animCooldown = currentSkin.GetCDWAnimation(currentAnim);

        //Make the animation faster if you walk fast
        float spdCoef = 1f / (infos.movement.realCurrentSpd / _StaticPlayer.spd.startValue);
        if (currentAnim == STATEANIM.WALK)
            animCooldown *= Mathf.Clamp(spdCoef, _StaticSkins.fasterSpdModif, _StaticSkins.lowerSpdModif);

        if (timerAnim > animCooldown)
        {
            timerAnim = 0f;
            indexAnim = (indexAnim + 1) % GetNbFrameInAnim();
        }
        infos.spriteRenderer.sprite = currentSkin.GetSprite(currentSpriteDirection, indexAnim, currentAnim);
    }

    private int GetNbFrameInAnim()
    {
        if (currentAnim == STATEANIM.IDLE)
            return currentSkin.idle.Count;
        else if (currentAnim == STATEANIM.WALK)
            return currentSkin.walk.Count;

        return currentSkin.idle.Count;
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