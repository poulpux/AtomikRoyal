using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInfos infos;

    public List<string> canMove = new List<string>();
    private float currentSpd, currentSpdModifier, timerCurve;
    private Vector2 lastDirection, saveDir;
    private bool twoActiv, smoothRota;

    void Start()
    {
        infos = GetComponent<PlayerInfos>();

        currentSpdModifier = 1f;

        infos.UpdateStatsEvent.AddListener(() => currentSpd = infos.spd); //Quand tu améliores tes stats
        infos.isDeadEvent.AddListener((infos) => currentSpd = _StaticPlayer.deadSpd);
    }

    void Update()
    {
        Help2DirectionWhenStop();
        LastDirection();
        Move();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void Move()
    {
        if (canMove.Count != 0)
            return;

        float currentSpd = this.currentSpd;
        PlayCurve();

        currentSpd = _StaticPlayer.beginEndMoveCurve.Evaluate(timerCurve * (1f/_StaticPlayer.beginEndMoveCurveDuration)) * currentSpd;
        infos.rb.velocity = lastDirection * currentSpd * currentSpdModifier;
    }

    private void LastDirection()
    {
        if (infos.inputSystem.direction != Vector2.zero && !twoActiv)//Pour quand tu touches pas le clavier
            lastDirection = infos.inputSystem.direction;
        else if (smoothRota) //Pour pas faire d'accout quand tu passes de 2 à 1 directions
            lastDirection = saveDir;
    }

    private void Help2DirectionWhenStop()
    {
        if (infos.inputSystem.direction.y != 0f && infos.inputSystem.direction.x != 0f)
        {
            StopCoroutine(PlaySaveCoroutine());
            StartCoroutine(PlaySaveCoroutine());
            saveDir = infos.inputSystem.direction;
        }
    }

    private IEnumerator PlaySaveCoroutine()
    {
        twoActiv = true;
        float timer = 0f;

        while (timer < 0.3f)
        {
            smoothRota = timer < 0.1f;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        twoActiv = false;

        yield break;
    }

    private void PlayCurve()
    {
        if (infos.inputSystem.direction != Vector2.zero)
        {
            if (timerCurve < _StaticPlayer.beginEndMoveCurveDuration)
                timerCurve += Time.deltaTime;
        }
        else
            timerCurve = timerCurve - Time.deltaTime > 0f ? timerCurve - Time.deltaTime : 0f;
    }
}
