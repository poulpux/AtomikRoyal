using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO_filename", menuName = "SO/CardSO")]
public class CardSO : ScriptableObjectWithScriptMother
{
    [Header("CardSO")]
    [Space(10)]
    public string namee;
    public string description;
    public int cost;
    public Sprite spriteCard;


    private void OnValidate()
    {
        VerifType<CardMother>();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}
