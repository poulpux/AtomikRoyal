using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public int damage;
    PlayerInfos playerInfos;    
    List<PlayerInfos> touchedPlayer = new List<PlayerInfos>();

    private void OnCollisionEnter(Collision collision)
    {
        //TUDO
    }

    private void DontPassThrowWall()
    {
        //TUDO
    }
}
