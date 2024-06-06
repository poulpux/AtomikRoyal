using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MedalsSO_filename", menuName = "SO/MedalsSO")]
public class MedalsSO : ScriptableObjectWithScriptMother
{
    [Header("MedalsSO")]
    [Space(10)]
    public string namee;
    public string description;
    public Sprite spriteMedla;


    private void OnValidate()
    {
        VerifType<MedalsMother>();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}
