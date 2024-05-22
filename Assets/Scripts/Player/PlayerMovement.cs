using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInfos infos;
    PlayerInputSystem inputSystem;

    public List<string> canMove = new List<string>();
    [SerializeField] private float glueSpdModifier, waterSpdModifier, deadSpd;
    private float currentSpd, currentSpdModifier;
    void Start()
    {
        infos = GetComponent<PlayerInfos>();
        inputSystem = GetComponent<PlayerInputSystem>();

        currentSpdModifier = 1f;

        infos.UpdateStatsEvent.AddListener(() => currentSpd = infos.spd);
        infos.isDeadEvent.AddListener((infos) => currentSpd = deadSpd);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        infos.rb.velocity = inputSystem.direction * currentSpd * currentSpdModifier;
    }
}
