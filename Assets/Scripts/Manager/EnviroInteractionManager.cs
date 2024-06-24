using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroInteractionManager : SingletonMother<EnviroInteractionManager>
{
    public int[,] binaryMaskMap { get; private set; }
    public void Start()
    {
        binaryMaskMap = new int[_StaticEnvironement.mapLenght / _StaticEnvironement.tabResolution, _StaticEnvironement.mapLenght / _StaticEnvironement.tabResolution];
        print(binaryMaskMap[5, 5]);
    }
}
