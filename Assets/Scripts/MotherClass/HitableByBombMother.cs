using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitableByBombMother : MonoBehaviour
{
    public void GetHit(int damage)
    {
        if (HitCondtion())
            HitEffect(damage);
    }

    protected virtual bool HitCondtion() 
    {
        return true;
    }
    
    protected virtual void HitEffect(int damage) 
    {
        
    }
}
