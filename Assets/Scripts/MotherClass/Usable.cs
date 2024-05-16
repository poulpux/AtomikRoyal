using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : MonoBehaviour
{
    [HideInInspector] public UsableSO SO;
    [HideInInspector] public PlayerInfos playerInfos;

    virtual public void Use()
    {

    }

    virtual public void AddSO(UsableSO SO)
    {
        if (SO.GetType() == typeof(UsableSO))
            this.SO = SO;
        else
            Debug.LogError("Not good type");
    }
}
