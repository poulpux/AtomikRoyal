using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardMother : MonoBehaviour
{
    public PlayerInfos owner;
    public CardSO SO;
    public virtual void Interact()
    {

    }

    public virtual void Draw()
    {

    }

    public virtual void AddSO(CardSO SO)
    {
        if (SO.GetType() == typeof(CardSO))
            this.SO = SO;
        else
            Debug.Log("Try to add the SO variable but it's not the right type");
    }
}
