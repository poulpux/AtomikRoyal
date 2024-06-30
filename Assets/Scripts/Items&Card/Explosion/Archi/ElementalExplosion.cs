using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalExplosion : MonoBehaviour
{
    [SerializeField] private ElementalBombSO SO;
    List<Vector2Int> pair = new List<Vector2Int>();
    List<Vector2Int> impaire = new List<Vector2Int>();
    void Start()
    {
        if (SO.distOnStart == 0) return;
        Vector2Int posInt = GF.EnterRealPositionInEnviroTab(transform.position);
        EnviroManager.Instance.AddElementEvent.Invoke(posInt.x, posInt.y, SO.type);
        impaire.Add(posInt);    
        for (int i = 1; i < SO.distOnStart; i++)
        {
            if (i % 2 == 1)
            {
                foreach (var item in impaire)
                    InvokeCross(item);
            }
            else
            {
                foreach (var item in pair)
                    InvokeFill(item);
            }
        }
    }

    void Update()
    {
        
    }

    private void InvokeCross(Vector2Int item)
    {
        //C'est ici qu'on fait la sépare les deux patterns
        EnviroManager.Instance.AddElementEvent.Invoke(item.x + 1, item.y, SO.type);
        EnviroManager.Instance.AddElementEvent.Invoke(item.x - 1, item.y, SO.type);
        EnviroManager.Instance.AddElementEvent.Invoke(item.x, item.y + 1, SO.type);
        EnviroManager.Instance.AddElementEvent.Invoke(item.x, item.y - 1, SO.type);
        pair = new List<Vector2Int>
        {
            new Vector2Int(item.x + 1, item.y + 1),
            new Vector2Int(item.x + 1, item.y - 1),
            new Vector2Int(item.x - 1, item.y - 1),
            new Vector2Int(item.x - 1, item.y + 1)
        };

        impaire = new List<Vector2Int>
        {
            new Vector2Int(item.x + 1, item.y),
            new Vector2Int(item.x - 1, item.y),
            new Vector2Int(item.x, item.y - 1),
            new Vector2Int(item.x, item.y + 1)
        };
    }

    private void InvokeFill(Vector2Int item)
    {
        EnviroManager.Instance.AddElementEvent.Invoke(item.x, item.y, SO.type);
    }
}
