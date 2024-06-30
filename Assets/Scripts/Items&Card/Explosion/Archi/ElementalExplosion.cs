using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public enum ATTACKRANGE
{
    DAWN,
    UP,
    RIGHT,
    LEFT
}

public class ElementalExplosion : MonoBehaviour
{
    [SerializeField] private ElementalBombSO SO;
    List<Vector2Int> pair = new List<Vector2Int>();
    List<Vector2Int> impaire = new List<Vector2Int>();

    Dictionary<string, bool> allInstantiatePrefab = new Dictionary<string, bool>();
    void Start()
    {
        InstanitateElement();
        //EnviroManager.Instance.RemoveElementEvent.AddListener((x, y, element) => AFireIsRemoved(new Vector2Int(x, y), element));
        StartCoroutine(DispertionCoroutine());
    }

    void Update()
    {
        
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private IEnumerator DispertionCoroutine()
    {
        float timer = 0.0f;
        int counter = 1;
        while (timer < SO.lifeTime)
        {
            timer += Time.deltaTime;    
            if(timer > counter + 1 * _StaticEnvironement.CDWFireFlammable)
            {
                foreach (var item in allInstantiatePrefab)
                {
                    Vector2Int pos = DecodeTagToCoordinates(item.Key);
                    print("pos : " + item.Key + " currentPos : "+pos );
                    ATTACKRANGE range = (ATTACKRANGE)Random.Range(0, (int)GF.GetMaxValue<ATTACKRANGE>());
                    if(range == ATTACKRANGE.DAWN)
                        EnviroManager.Instance.HitByFire(new Vector2Int(pos.x, pos.y - 1));
                    else if(range == ATTACKRANGE.UP)
                        EnviroManager.Instance.HitByFire(new Vector2Int(pos.x, pos.y + 1));
                    else if(range == ATTACKRANGE.LEFT)
                        EnviroManager.Instance.HitByFire(new Vector2Int(pos.x - 1, pos.y));
                    else if(range == ATTACKRANGE.RIGHT)
                        EnviroManager.Instance.HitByFire(new Vector2Int(pos.x + 1, pos.y));
                }
                counter++;
            }
            yield return new WaitForEndOfFrame();
        }

        foreach (var item in allInstantiatePrefab)
        {
            Vector2Int pos = DecodeTagToCoordinates(item.Key);
            EnviroManager.Instance.RemoveElementEvent.Invoke(pos.x, pos.y, ELEMENTS.FIRE);
        }
        Destroy(gameObject);
    }

    //private void AFireIsRemoved(Vector2Int pos, ELEMENTS element)
    //{
    //    string tag = CodateTagToDictionnary(pos.x, pos.y);
    //    if (allInstantiatePrefab.ContainsKey(tag))
    //        allInstantiatePrefab.Remove(tag);
    //}

    private string CodateTagToDictionnary(int x, int y)
    {
        return x.ToString() + y.ToString();
    }

    private Vector2Int DecodeTagToCoordinates(string tag)
    {
        int halfLength = tag.Length / 2;
        string xStr = tag.Substring(0, halfLength);
        string yStr = tag.Substring(halfLength, halfLength);

        int x = int.Parse(xStr);
        int y = int.Parse(yStr);

        return new Vector2Int(x, y);
    }

    private void InstanitateElement()
    {
        if (SO.distOnStart == 0) return;
        Vector2Int posInt = GF.EnterRealPositionInEnviroTab(transform.position);
        EnviroManager.Instance.AddElementEvent.Invoke(posInt.x, posInt.y, SO.type);
        impaire.Add(posInt);
        allInstantiatePrefab.Add(CodateTagToDictionnary(posInt.x, posInt.y), false);
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

        pair = new List<Vector2Int>();
        impaire = new List<Vector2Int>();
    }

    private void InvokeCross(Vector2Int item)
    {
        //C'est ici qu'on fait la sépare les deux patterns
        EnviroManager.Instance.AddElementEvent.Invoke(item.x + 1, item.y, SO.type);
        EnviroManager.Instance.AddElementEvent.Invoke(item.x - 1, item.y, SO.type);
        EnviroManager.Instance.AddElementEvent.Invoke(item.x, item.y + 1, SO.type);
        EnviroManager.Instance.AddElementEvent.Invoke(item.x, item.y - 1, SO.type);
        allInstantiatePrefab.Add(CodateTagToDictionnary(item.x + 1, item.y), false);
        allInstantiatePrefab.Add(CodateTagToDictionnary(item.x - 1, item.y), false);
        allInstantiatePrefab.Add(CodateTagToDictionnary(item.x, item.y + 1), false);
        allInstantiatePrefab.Add(CodateTagToDictionnary(item.x, item.y - 1), false);

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
        allInstantiatePrefab.Add(CodateTagToDictionnary(item.x, item.y), false);
    }
}
