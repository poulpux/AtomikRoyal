using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroInteractionManager : SingletonMother<EnviroInteractionManager>
{
    public int[,] binaryMaskMap { get; private set; } = new int[5, 5];
    public void Start()
    {
        
    }
}
