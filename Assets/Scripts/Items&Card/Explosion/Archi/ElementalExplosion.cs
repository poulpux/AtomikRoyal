using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ElementalExplosion : MonoBehaviour
{
    [SerializeField] private ElementalBombSO SO;
    void Start()
    {
        if (SO.distOnStart == 0) return;
        List<Vector2Int> listPosToExpend = new List<Vector2Int>();
        Vector2Int posInt = GF.EnterRealPositionInEnviroTab(transform.position);
        listPosToExpend.Add(posInt);
        EnviroManager.Instance.AddElementEvent.Invoke(posInt.x, posInt.y, SO.type);
        for (int i = 1; i < SO.distOnStart; i++)
        {
            List<Vector2Int> listPosToExpendTemp = new List<Vector2Int>();
            foreach (var item in listPosToExpend)
            {
                EnviroManager.Instance.AddElementEvent.Invoke(item.x+1, item.y, SO.type);
                listPosToExpendTemp.Add(item + Vector2Int.right);
                EnviroManager.Instance.AddElementEvent.Invoke(item.x-1, item.y, SO.type);
                listPosToExpendTemp.Add(item + Vector2Int.left);
                EnviroManager.Instance.AddElementEvent.Invoke(item.x, item.y +1, SO.type);
                listPosToExpendTemp.Add(item + Vector2Int.up);
                EnviroManager.Instance.AddElementEvent.Invoke(item.x, item.y -1, SO.type);
                listPosToExpendTemp.Add(item + Vector2Int.down);
            }

            listPosToExpend = listPosToExpendTemp;
        }
    }

    void Update()
    {
        
    }
}
