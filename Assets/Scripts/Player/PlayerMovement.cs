using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInfos infos;

    public List<string> canMove = new List<string>();
    private float currentSpd, currentSpdModifier;


    public AnimationCurve curve;
    public float curveDuration = 0.1f;
    public float helpDir = 0.3f;
    private float timerCurve;
    private Vector2 lastDirection;

    private Vector2 direction;
    private Vector2 saveDir;
    private bool twoActiv;
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

    private void Move()
    {
        if (canMove.Count != 0)
            return;

        float currentSpd = this.currentSpd;
        PlayCurve();

        currentSpd = curve.Evaluate(timerCurve * (1f/curveDuration)) * currentSpd;
        infos.rb.velocity = lastDirection * currentSpd * currentSpdModifier;
    }

    private void LastDirection()
    {
        if (infos.inputSystem.direction != Vector2.zero)
            lastDirection = infos.inputSystem.direction;
        else if (twoActiv)
        {
            twoActiv = false;
            lastDirection = saveDir;
        }
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

        while (timer < 0.2f)
        {
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
            if (timerCurve < curveDuration)
                timerCurve += Time.deltaTime;
        }
        else
            timerCurve = timerCurve - Time.deltaTime > 0f ? timerCurve - Time.deltaTime : 0f;
    }
}
