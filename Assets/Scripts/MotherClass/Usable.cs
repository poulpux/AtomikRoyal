using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : MonoBehaviour
{
    public int nbMaxInventory, nbRecolted;
    public SpriteRenderer sprite;
    public string nameUsable, description;
    public RARITY rarity;
    
    virtual public void Use()
    {

    }
}
