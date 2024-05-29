using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public PlayerInfos infos;
    public float baseDomage; 
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
