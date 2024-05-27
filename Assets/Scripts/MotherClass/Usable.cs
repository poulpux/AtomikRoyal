using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : MonoBehaviour
{
    [HideInInspector] public UsableSO SO;
    [HideInInspector] public PlayerInfos playerInfos;


    virtual public void TryUse()
    {
        if (UseCondition())
            Use();
    }
    virtual protected void Use()
    {

    }

    virtual protected bool UseCondition()
    {
        return true;
    }

    virtual public void AddSO(UsableSO SO)
    {
        if (SO.GetType() == typeof(UsableSO))
            this.SO = SO;
        else
            Debug.LogError("Not good type");
    }
}
