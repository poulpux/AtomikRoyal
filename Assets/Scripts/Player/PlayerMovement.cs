using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInfos infos;

    public List<string> canMove = new List<string>();
    private float currentSpd, currentSpdModifier;

    void Start()
    {
        infos = GetComponent<PlayerInfos>();

        currentSpdModifier = 1f;

        infos.UpdateStatsEvent.AddListener(() => currentSpd = infos.spd);
        infos.isDeadEvent.AddListener((infos) => currentSpd = _StaticPlayer.deadSpd);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        infos.rb.velocity = infos.inputSystem.direction * currentSpd * currentSpdModifier;
    }
}
