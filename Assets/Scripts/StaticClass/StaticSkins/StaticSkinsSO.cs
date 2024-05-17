using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StaticSkinsSO", menuName = "Static/StaticSkinsSO")]
public class StaticSkinsSO : ScriptableObject
{
    [Header("All skins")]
    [Space(10)]
    public List<SkinSO> allSkins = new List<SkinSO>();
}
