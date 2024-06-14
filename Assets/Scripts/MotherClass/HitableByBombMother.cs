using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitableByBombMother : MonoBehaviour
{
    public void GetHit(int damage, PlayerInfos offenser)
    {
        print("GetHit");
        if (HitCondtion())
            HitEffect(damage, offenser);
    }

    protected virtual bool HitCondtion() 
    {
        return true;
    }
    
    protected virtual void HitEffect(int damage, PlayerInfos offenser) 
    {
        
    }
}
