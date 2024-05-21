using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerInfos : MonoBehaviour
{
    [Header("Main infos")]
    [Space(10)]
    public string pseudo;
    public int nbKill;
    public bool isDead, isInvincible;
    [SerializeField] private Collider2D colliderr;
    [HideInInspector] public Action<PlayerInfos> isDeadEvent;
    private List<PlayerInfos> team;

    [Header("Stats")]
    [Space(10)]
    //LIFE
    public int currentLife;
    public int maxLife;
    //DAMAGES
    public int dmgCAC, dmgBomb;
    //OTHERS
    public float spd, cdwThrow, throwForce, range, exploSize;

    [Header("Stats points")]
    [Space(10)]
    public int spd_Stat;
    public int maxLife_Stat, exploSize_Stat, dmgCAC_Stat, dmgBomb_Stat, cdwThrow_Stat, throwForce_Stat, range_Stat;
    ////// Pourquoi pas ajouter les médailles ici aussi
    /// 
    /// 
    /// 
    /// 
    /// 
    //////

    void Start()
    {
        SetAllStats();
        GameManager.Instance.gameRules.gameEndEvent += EndOfTheGame;

        //AddTeamate(this);
    }

    
    void Update()
    {
        
    }

    public void UpgradeStat(PLAYERSTATS stats)
    {
        //TODO
    }

    public void IncreaseLife(int heal)
    {
        currentLife = currentLife + heal <= maxLife ? currentLife + heal : maxLife;
    }

    public void DecreaseLife(int damage)
    {
        if (isInvincible)
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
    }

    public void AddTeamate(PlayerInfos playerInfos)
    {
        team.Add(playerInfos);
    }

    private void EndOfTheGame()
    {
        isInvincible = true;
    }
}
