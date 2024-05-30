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
[RequireComponent(typeof(PlayerCard))]
[RequireComponent(typeof(WhenPlayerDied))]

public class PlayerInfos : MonoBehaviour
{
    [SerializeField] private Collider2D colliderr;

    [SerializeField] private Camera _cam;

    [HideInInspector] public Camera cam
    {
        get { return _cam; }
        private set { _cam = value; }
    }

    [HideInInspector] public PlayerInputSystem inputSystem { get; private set; }
    [HideInInspector] public PlayerMovement movement { get; private set; }
    [HideInInspector] public PlayerInventory inventory { get; private set; }
    [HideInInspector] public Rigidbody2D rb { get; private set; }
    [HideInInspector] public SpriteRenderer spriteRenderer { get; private set; }

    [HideInInspector] public UnityEvent UpdateStatsEvent = new UnityEvent();
    [HideInInspector] public UnityEvent GetCancelEvent = new UnityEvent();
    [HideInInspector] public UnityEvent<PlayerInfos> isDeadEvent = new UnityEvent<PlayerInfos>();

    [HideInInspector] public bool isMoving { get; private set; }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    //[Header("Main infos")]
    //[Space(10)]
    [SerializeField]
    public string pseudo { get; private set; }

    public int nbKill { get; private set; }
    public bool isDead { get; private set; }

    public List<string> isInvincibleList = new List<string>();

    private List<PlayerInfos> team;

    //[Header("Stats")]
    //[Space(10)]

    //LIFE
    public int currentLife { get; private set; } 
    public int currentShield { get; private set; } 
    public int maxLife { get; private set; }
    //DAMAGES
    public int dmgCAC { get; private set; } 
    public int dmgBomb { get; private set; }
    //OTHERS
    public float spd { get; private set; }
    public float cdwThrow { get; private set; }  
    public float throwForce { get; private set; }  
    public float range { get; private set; } 
    public float  exploSize { get; private set; }

    //[Header("Stats points")]
    //[Space(10)]
    public int spd_Stat { get; private set; }
    public int maxLife_Stat { get; private set; }
    public int exploSize_Stat { get; private set; }
    public int dmgCAC_Stat { get; private set; }
    public int dmgBomb_Stat { get; private set; }
    public int cdwThrow_Stat { get; private set; }
    public int throwForce_Stat { get; private set; }
    public int range_Stat { get; private set; }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    ////// Pourquoi pas ajouter les médailles ici aussi
    /// 
    /// 
    /// 
    /// 
    /// 
    //////

    private void Awake()
    {
        InstantiateAll();
    }
    void Start()
    {
        SetAllStats();
        currentLife = maxLife;
        GameManager.Instance.gameRules.gameEndEvent += EndOfTheGame;

        //AddTeamate(this);
    }

    private void Update()
    {
        SetIsMoving();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void SetIsMoving()
    {
        isMoving = movement.canMove.Count == 0 && inputSystem.direction != Vector2.zero;
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
    
    public void IncreaseShield(int shield)
    {
        currentShield = currentShield + shield <= _StaticPlayer.maxShield ? currentShield + shield : _StaticPlayer.maxShield;
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

    public void TakeDomage(int damage)
    {
        if (isInvincibleList.Count >= 1)
            return;

        int currentDamage = damage;
        if(currentShield >=currentDamage) 
            currentShield -= currentDamage;
        else
        {
            currentDamage -= currentShield;
            currentShield = 0;
            currentLife -= currentDamage;
        }
    }

    public void AddTeamate(PlayerInfos playerInfos)
    {
        team.Add(playerInfos);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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

    private void EndOfTheGame()
    {
        isInvincibleList.Add("EndGame");
    }

    private void InstantiateAll()
    {
        inputSystem = GetComponent<PlayerInputSystem>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        inventory = GetComponent<PlayerInventory>();
    }
}
