using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UsableMother : MonoBehaviour
{
    [HideInInspector] public UsableSOMother SO;
    [HideInInspector] public PlayerInfos infos;
    [HideInInspector] public UnityEvent UseEvent = new UnityEvent();

    protected virtual void Start()
    {
        infos = GetComponent<PlayerInfos>();
    }

    virtual public void TryUse()
    {
        if (UseCondition())
            Use();
    }
    virtual protected void Use()
    {
        UseEvent.Invoke();
    }

    virtual protected bool UseCondition()
    {
        return true;
    }

    virtual public void AddSO(UsableSOMother SO)
    {
        if (SO.GetType() == typeof(UsableSOMother))
            this.SO = SO;
        else
            Debug.LogError("Not good type");
    }
}
