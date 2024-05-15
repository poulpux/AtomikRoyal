using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfos : MonoBehaviour
{
    public string pseudo;
    public int nbKill;
    public bool isDead;
    [SerializeField] private Collider2D colliderr;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void GetAKill()
    {
        nbKill++;
    }
}
