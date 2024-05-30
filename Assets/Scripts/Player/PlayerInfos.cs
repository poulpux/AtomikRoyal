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

    [HideInInspector] public Camera cam { get { return _cam; } private set { _cam = value; } }

    [HideInInspector] public PlayerInputSystem inputSystem { get; private set; }
    [HideInInspector] public PlayerMovement movement { get; private set; }
    [HideInInspector] public PlayerInventory inventory { get; private set; }
    [HideInInspector] public Rigidbody2D rb { get; private set; }
    [HideInInspector] public SpriteRenderer spriteRenderer { get; private set; }

    [HideInInspector] public UnityEvent UpdateStatsEvent = new UnityEvent();
    [HideInInspector] public UnityEvent GetCancelEvent = new UnityEvent();
    [HideInInspector] public UnityEvent<PlayerInfos> isDeadEvent = new UnityEvent<PlayerInfos>();

    [HideInInspector] public bool isMoving { get; private set; }
    [HideInInspector] public List<string> cantUpgrade = new List<string>();

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

    private List<float> _stats = new List<float>();
    public List<float> stats { get { return _stats; } private set { _stats = value; } }

    private List<int> _nbStats = new List<int>();
    public List<int> nbStats { get { return nbStats; } private set { nbStats = value; } }

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
        for (int i = 0; i < 8; i++)
        {
            _nbStats.Add(0);
            _stats.Add(0f);
        }
        SetAllStats();
        currentLife = (int)_stats[(int)PLAYERSTATS.PVMAX];
        GameManager.Instance.gameRules.gameEndEvent += EndOfTheGame;

        AllEvents();

        //AddTeamate(this);
    }

    private void Update()
    {
        SetIsMoving();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


    private void WhenDead()
    {
        isDead = true;
        cantUpgrade.Add("isDead");
    }

    private void SetIsMoving()
    {
        isMoving = movement.canMove.Count == 0 && inputSystem.direction != Vector2.zero;
    }

    public void TryUpgradeStat(PLAYERSTATS stats)
    {
        if (cantUpgrade.Count > 0)
            return;

        int price = _StaticPlayer.GetPrice(stats, _nbStats[(int)stats]);
        if (inventory.nbCoins >= price)
        {
            inventory.nbCoins -= price;
            _nbStats[(int)stats]++;
            SetAllStats();
        }
    }

    public void IncreaseLife(int heal)
    {
        currentLife = currentLife + heal <= (int)_stats[(int)PLAYERSTATS.PVMAX] ? currentLife + heal : (int)_stats[(int)PLAYERSTATS.PVMAX];
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
            isDeadEvent.Invoke(this);
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
        for (int i = 0; i < 8; i++)
            _stats[i] = _StaticPlayer.GetValue((PLAYERSTATS)i, _nbStats[i]); ;

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

    private void AllEvents()
    {
        for (int i = 0; i < inputSystem.upgradeStatEvent.Count; i++)
        {
            int index = i;
            inputSystem.upgradeStatEvent[index].AddListener(() => TryUpgradeStat((PLAYERSTATS)index));
        }

        isDeadEvent.AddListener((infos) => WhenDead());
    }
}
