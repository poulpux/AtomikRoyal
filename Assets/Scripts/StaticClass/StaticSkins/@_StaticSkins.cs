using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticSkins 
{
    static public List<SkinSO> allSkins { get; private set; } = new List<SkinSO>();
    static public float idleCldAnim {  get; private set; }
    static public float walkCldAnim { get; private set; }
    static public float fasterSpdModif { get; private set; }
    static public float lowerSpdModif { get; private set; }

    static public void Init(StaticSkinsSO SO)
    {
        allSkins = SO.allSkins;
        idleCldAnim = SO.idleCldAnim;
        walkCldAnim = SO.walkCldAnim;

        fasterSpdModif = SO.fasterSpdModif;
        lowerSpdModif = SO.lowerSpdModif;
    }
}
