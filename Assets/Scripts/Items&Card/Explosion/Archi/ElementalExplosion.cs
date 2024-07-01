using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Vector2Int centralPos;

    Dictionary<string, Vector2Int> allInstantiatePrefab = new Dictionary<string, Vector2Int>();
    float timer = 0f;
    List<Vector2Int> toAdd = new List<Vector2Int>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void Start()
    {
        EnviroManager.Instance.RemoveElementEvent.AddListener((x, y, element) => AnElementIsRemoved(new Vector2Int(x, y), element));
        InstanitateElement();
        StartCoroutine(DestroyCoroutine());

        if (SO.dispertionType == DISPERTIONTYPE.FIRE)
            StartCoroutine(FireDispertionCoroutine());
        else if(SO.dispertionType == DISPERTIONTYPE.GAZ)
            StartCoroutine(GazDispertionCoroutine());

    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InstanitateElement()
    {
        if (SO.distOnStart == 0) return;

        FirstInstantiation();

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

    private IEnumerator FireDispertionCoroutine()
    {
        timer = 0.0f;
        int counter = 1;
        while (true)
        {
            timer += Time.deltaTime;    
            if(timer > (counter + 1) * _StaticEnvironement.CDWFireFlammable)
            {
                foreach (var item in allInstantiatePrefab)
                {
                    ATTACKRANGE range = (ATTACKRANGE)Random.Range(0, (int)GF.GetMaxValue<ATTACKRANGE>()+1);
                    if (range == ATTACKRANGE.DAWN)
                        AttackAndAddFire(new Vector2Int(item.Value.x, item.Value.y - 1));
                    else if (range == ATTACKRANGE.UP)
                        AttackAndAddFire(new Vector2Int(item.Value.x, item.Value.y + 1));
                    else if (range == ATTACKRANGE.LEFT)
                        AttackAndAddFire(new Vector2Int(item.Value.x - 1, item.Value.y));
                    else if (range == ATTACKRANGE.RIGHT)
                        AttackAndAddFire(new Vector2Int(item.Value.x + 1, item.Value.y));
                }

                foreach (var pos in toAdd)
                    TryAddKey(pos);
                toAdd = new List<Vector2Int>();
                counter++;
            }
            yield return null;
        }
    }

    private IEnumerator GazDispertionCoroutine()
    {
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(SO.lifeTime);
        timer = SO.lifeTime;
        foreach (var item in allInstantiatePrefab)
            EnviroManager.Instance.RemoveElementEvent.Invoke(item.Value.x, item.Value.y, SO.type);
        Destroy(gameObject);
    }

    private void AttackAndAddFire(Vector2Int pos)
    {
        if (GF.IsInDistDoubleTab(centralPos, pos, SO.maxDistanceDispertion) && EnviroManager.Instance.HitByFire(pos))
            toAdd.Add(pos);
    }

    private void AnElementIsRemoved(Vector2Int pos, ELEMENTS element)
    {
        if (element != SO.type || timer >= SO.lifeTime) return;
        string tag = CodateTagToDictionnary(pos.x, pos.y);
        if (allInstantiatePrefab.ContainsKey(tag))
            allInstantiatePrefab.Remove(tag);
    }
    private string CodateTagToDictionnary(int x, int y)
    {
        return x.ToString() + " " + y.ToString();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InvokeCross(Vector2Int item)
    {
        //C'est ici qu'on fait la sépare les deux patterns
        TryAddKey(new Vector2Int(item.x + 1, item.y));
        TryAddKey(new Vector2Int(item.x - 1, item.y));
        TryAddKey(new Vector2Int(item.x, item.y - 1));
        TryAddKey(new Vector2Int(item.x, item.y + 1));
       
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

    private void InvokeFill(Vector2Int item)=>
        TryAddKey(item);

    private void FirstInstantiation()
    {
        Vector2Int posInt = GF.EnterRealPositionInEnviroTab(transform.position);
        TryAddKey(posInt);
        impaire.Add(posInt);
        centralPos = posInt;
    }

    private void TryAddKey(Vector2Int item)
    {
        if (allInstantiatePrefab.ContainsKey(CodateTagToDictionnary(item.x, item.y))
            ||GF.IsOnBinaryMask(EnviroManager.Instance.binaryMaskMap[item.x, item.y].binaryMask, (int)SO.type)) return;

        EnviroManager.Instance.AddElementEvent.Invoke(item.x, item.y, SO.type);
        allInstantiatePrefab.Add(CodateTagToDictionnary(item.x, item.y), item);
    }
}
