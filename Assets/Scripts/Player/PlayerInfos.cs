using Cinemachine;
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
[RequireComponent(typeof(PlayerGetHit))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInteract))]
[RequireComponent(typeof(PlayerVisuel))]
[RequireComponent(typeof(PlayerCard))]
[RequireComponent(typeof(WhenPlayerDied))]

public class PlayerInfos : MonoBehaviour, IActWhenPlayerIsDead, IActWhenPlayerSpawn
{
    //[Header("All Refs")]

    [SerializeField] private Collider2D colliderr;

    [SerializeField] private Camera _cam;

    [HideInInspector] public Camera cam { get { return _cam; } private set { _cam = value; } }


    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;

    [HideInInspector] public CinemachineVirtualCamera cinemachineCam { get { return _cinemachineCam; } private set { _cinemachineCam = value; } }

    [HideInInspector] public PlayerInputSystem inputSystem { get; private set; }
    [HideInInspector] public PlayerMovement movement { get; private set; }
    [HideInInspector] public PlayerInventory inventory { get; private set; }
    [HideInInspector] public PlayerInteract interact { get; private set; }
    [HideInInspector] public Rigidbody2D rb { get; private set; }
    [HideInInspector] public SpriteRenderer spriteRenderer { get; private set; }
    [HideInInspector] public FirebaseProfil_AllMedals_Stats allMedals_Stats { get; private set; } = new FirebaseProfil_AllMedals_Stats();

    [HideInInspector] public UnityEvent UpdateStatsEvent = new UnityEvent();
    [HideInInspector] public UnityEvent GetCancelEvent = new UnityEvent();
    [HideInInspector] public UnityEvent HitEnnemyEvent = new UnityEvent();
    [HideInInspector] public UnityEvent<PlayerInfos> isDeadEvent = new UnityEvent<PlayerInfos>();
    [HideInInspector] public UnityEvent isSpawningEvent = new UnityEvent();

    [HideInInspector] public bool isMoving { get; private set; }
    [HideInInspector] public List<string> cantUpgrade = new List<string>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    //[Header("Main infos")]

    [SerializeField]
    public string pseudo { get; private set; }

    public int nbKill { get; private set; }
    public bool isDead { get; private set; }

    public List<string> isInvincibleList = new List<string>();

    private List<PlayerInfos> team;

    //[Header("Stats")]

    //LIFE
    public int currentLife { get; private set; } 
    public int currentShield { get; private set; }
    public List<float> stats { get; private set; } = new List<float>();
    public List<int> nbUpgrade { get; private set; } = new List<int>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void Awake()
    {
        InstantiateAll();
    }
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            nbUpgrade.Add(0);
            stats.Add(0f);
        }
        SetAllStats();
        currentLife = (int)stats[(int)PLAYERSTATS.PVMAX];
        GameManager.Instance.gameRules.gameEndEvent += EndOfTheGame;

        AllEvents();
        SetAllMedals();

        isSpawningEvent.Invoke();
        GameManager.Instance.SetCurrentPlayer(this);
        //AddTeamate(this);
    }
    public void WhenSpawn()
    {
        isDead = false;
        cantUpgrade = new List<string>();
        colliderr.excludeLayers = _StaticPlayer.excludeCollisionWhenAlife;
    }

    public void WhenDied()
    {
        isDead = true;
        cantUpgrade.Add("isDead");
        colliderr.excludeLayers = _StaticPlayer.excludeCollisionWhenDead;
    }

    private void Update()
    {
        SetIsMoving();
    }


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void TryUpgradeStat(PLAYERSTATS stats)
    {
        if (cantUpgrade.Count > 0 && nbUpgrade[(int)stats]<9)
            return;

        int price = _StaticPlayer.GetPrice(stats, nbUpgrade[(int)stats]);
        if (inventory.nbCoins >= price)
        {
            inventory.nbCoins -= price;
            nbUpgrade[(int)stats]++;
            SetAllStats();
        }
    }

    public void IncreaseLife(int heal)
    {
        currentLife = Mathf.Min(currentLife + heal, (int)stats[(int)PLAYERSTATS.PVMAX]);
    }
    
    public void IncreaseShield(int shield)
    {
        currentShield = Mathf.Min(currentShield + shield, _StaticPlayer.maxShield);
    }

    public void DecreaseLife(int damage, PlayerInfos offenser)
    {
        if (isInvincibleList.Count>=1 || Mathf.Sign(damage) ==  -1f)
            return;

        currentLife = Mathf.Max(currentLife - damage, 0);
        if (currentLife <= 0)
            isDeadEvent.Invoke(offenser);
    }

    public void TakeDomage(int damage, PlayerInfos offenser)
    {
        if (isInvincibleList.Count >= 1)
            return;

        DecreaseLife(Mathf.Max(0, damage - currentShield), offenser); //Set damage after decrease shield
        currentShield = Mathf.Max(currentShield - damage, 0);
    }

    public void AddTeamate(PlayerInfos playerInfos)
    {
        team.Add(playerInfos);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void SetIsMoving()
    {
        isMoving = movement.canMove.Count == 0 && inputSystem.direction != Vector2.zero;
    }

    private void GetAKill()
    {
        nbKill++;
    }

    private void SetAllStats()
    {
        for (int i = 0; i < 8; i++)
            stats[i] = _StaticPlayer.GetValue((PLAYERSTATS)i, nbUpgrade[i]); ;

        UpdateStatsEvent.Invoke();
    }

    private void SetAllMedals()
    {  
        foreach(MedalsSO medal in _StaticMedals.allMedals)
            GF.SetScripts<MedalsMother>(medal.script, gameObject);
    }

    private void EndOfTheGame()
    {
        isInvincibleList.Add("EndGame");
    }

    private void InstantiateAll()
    {
        inputSystem = GetComponent<PlayerInputSystem>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        inventory = GetComponent<PlayerInventory>();
        interact = GetComponent<PlayerInteract>();
    }

    private void AllEvents()
    {
        for (int i = 0; i < inputSystem.upgradeStatEvent.Count; i++)
        {
            int index = i;
            inputSystem.upgradeStatEvent[index].AddListener(() => TryUpgradeStat((PLAYERSTATS)index));
        }

        UpdateStatsEvent.AddListener(() => _cinemachineCam.m_Lens.OrthographicSize = stats[(int)PLAYERSTATS.RANGE]);
    }
}
