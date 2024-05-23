using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerInputSystem))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInteract))]
[RequireComponent(typeof(PlayerVisuel))]
public class PlayerInfos : MonoBehaviour
{
    [SerializeField] private Collider2D colliderr;
    public Camera cam;

    [HideInInspector] public PlayerInputSystem inputSystem;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    [HideInInspector] public UnityEvent UpdateStatsEvent = new UnityEvent();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [SerializeField] bool seeAll;

    [Header("Main infos")]
    [Space(10)]
    [ConditionalField("seeAll", true)] public string pseudo;

    [ConditionalField("seeAll", true)] public int nbKill;
    [ConditionalField("seeAll", true)] public bool isDead;

    List<string> isInvincibleList = new List<string>();
    [HideInInspector] public UnityEvent<PlayerInfos> isDeadEvent;
    private List<PlayerInfos> team;

    [Header("Stats")]
    [Space(10)]
    //LIFE
    [ConditionalField("seeAll", true)] public int currentLife;
    [ConditionalField("seeAll", true)] public int maxLife;
    //DAMAGES
    [ConditionalField("seeAll", true)] public int dmgCAC, dmgBomb;
    //OTHERS
    [ConditionalField("seeAll", true)] public float spd, cdwThrow, throwForce, range, exploSize;

    [Header("Stats points")]
    [Space(10)]
    [ConditionalField("seeAll", true)] public int spd_Stat;
    [ConditionalField("seeAll", true)] public int maxLife_Stat, exploSize_Stat, dmgCAC_Stat, dmgBomb_Stat, cdwThrow_Stat, throwForce_Stat, range_Stat;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    ////// Pourquoi pas ajouter les médailles ici aussi
    /// 
    /// 
    /// 
    /// 
    /// 
    //////

    void Start()
    {
        inputSystem = GetComponent<PlayerInputSystem>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetAllStats();
        GameManager.Instance.gameRules.gameEndEvent += EndOfTheGame;

        //AddTeamate(this);
    }

    public void UpgradeStat(PLAYERSTATS stats)
    {
        if (isDead)
            return;
        //TODO
    }

    public void IncreaseLife(int heal)
    {
        currentLife = currentLife + heal <= maxLife ? currentLife + heal : maxLife;
    }

    public void DecreaseLife(int damage)
    {
        if (isInvincibleList.Count>=1)
            return;

        currentLife -= damage;
        if (currentLife <= 0)
        {
            isDead = true;
            isDeadEvent.Invoke(this);
        }
    }

    private void GetAKill()
    {
        nbKill++;
    }

    private void SetAllStats()
    {
        spd = _StaticPlayer.GetValue(PLAYERSTATS.SPD, spd_Stat);
        maxLife = (int)_StaticPlayer.GetValue(PLAYERSTATS.PVMAX, maxLife_Stat);
        exploSize = _StaticPlayer.GetValue(PLAYERSTATS.EXPLOSIONSIZE, exploSize_Stat);
        dmgCAC = (int)_StaticPlayer.GetValue(PLAYERSTATS.DMGCAC, dmgCAC_Stat);
        dmgBomb = (int)_StaticPlayer.GetValue(PLAYERSTATS.DMGBOMB, dmgBomb_Stat);
        cdwThrow = _StaticPlayer.GetValue(PLAYERSTATS.COOLDOWNTHROW, cdwThrow_Stat);
        throwForce = _StaticPlayer.GetValue(PLAYERSTATS.THROWFORCE, throwForce_Stat);
        range = _StaticPlayer.GetValue(PLAYERSTATS.RANGE, range_Stat);

        UpdateStatsEvent.Invoke();
    }

    public void AddTeamate(PlayerInfos playerInfos)
    {
        team.Add(playerInfos);
    }

    private void EndOfTheGame()
    {
        isInvincibleList.Add("EndGame");
    }
}
