using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "UsableSO_filename", menuName = "SO/UsableSO")]
public class UsableSO : ScriptableObjectWithScript
{
    [Header("UsableSO")]
    [Space(10)]
    public string nameUsable;
    public string description;
    public int nbMaxInventory, nbRecolted;
    public Sprite sprite;
    public RARITY rarity;

    private void OnValidate()
    {
        VerifType<Usable>();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}
