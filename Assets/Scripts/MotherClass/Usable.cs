using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : MonoBehaviour
{
    protected UsableSO SO;
    virtual public void Use()
    {

    }
    virtual public void Use(PlayerInfos playerInfos)
    {

    }

    virtual public void AddSO(UsableSO SO)
    {
        if (SO.GetType() == typeof(UsableSO))
            this.SO = SO;
    }
}
