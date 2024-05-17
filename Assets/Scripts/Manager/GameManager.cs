using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public void SetGameMod(StaticRoundsSO roundSO)
    {
        System.Type t = System.Type.GetType(roundSO.script.name.Replace(".cs", ""));
        gameObject.AddComponent(t);
    }
}
