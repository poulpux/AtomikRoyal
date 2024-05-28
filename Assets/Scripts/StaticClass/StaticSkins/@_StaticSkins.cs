using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticSkins 
{
    static public IReadOnlyList<SkinSO> AllSkins => _allSkins.AsReadOnly();
    static private List<SkinSO> _allSkins = new List<SkinSO>();
    static public float idleCldAnim {  get; private set; }
    static public float walkCldAnim {  get; private set; }

    static public void Init(StaticSkinsSO SO)
    {
        _allSkins = SO.allSkins;
        idleCldAnim = SO.idleCldAnim;
        walkCldAnim = SO.walkCldAnim;
    }
}
