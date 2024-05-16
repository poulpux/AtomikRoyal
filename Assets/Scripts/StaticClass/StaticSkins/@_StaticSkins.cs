using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticSkins 
{
    static public List<SkinSO> allSkins = new List<SkinSO>();

    static public void Init(StaticSkinsSO SO)
    {
        allSkins = SO.allSkins;
    }
}
