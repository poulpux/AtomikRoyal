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
    Vector2Int centralPos;

    Dictionary<string, Vector2Int> allInstantiatePrefab = new Dictionary<string, Vector2Int>();
    float timer = 0f;
    int life;
    List<Vector2Int> toAdd = new List<Vector2Int>();
    List<Vector2Int> toAddPair = new List<Vector2Int>();
    List<Vector2Int> toAddImpair = new List<Vector2Int>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void Start()
    {
        EnviroManager.Instance.RemoveElementEvent.AddListener((x, y, element) => AnElementIsRemoved(new Vector2Int(x, y), element));
        InstanitateElement();

        if(SO.dispertionType != DISPERTIONTYPE.GAZ)
            StartCoroutine(DestroyCoroutine());

        if (SO.dispertionType == DISPERTIONTYPE.FIRE)
            StartCoroutine(FireDispertionCoroutine());
        else if(SO.dispertionType == DISPERTIONTYPE.GAZ)
            StartCoroutine(GazDispertionCoroutine());

        life = SO.lifeTime;
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
                
                pair.Clear();
                impaire.Clear();

                foreach (var item in toAddPair)
                    pair.Add(item);
                foreach (var item in toAddImpair)
                    impaire.Add(item);
                foreach (var item in toAdd)
                    TryAddKey(item);

                toAdd.Clear();
                toAddImpair.Clear();
                toAddPair.Clear();

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
            if(timer > (counter + 1) * _StaticEnvironement.CDWFireDispertion)
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
        while (true)
        {
            yield return new WaitForSeconds(_StaticEnvironement.CDWGazDispertion);
            foreach (var item in allInstantiatePrefab)
            {
                ATTACKRANGE range = (ATTACKRANGE)Random.Range(0, (int)GF.GetMaxValue<ATTACKRANGE>() + 1);
                if (range == ATTACKRANGE.DAWN)
                    TryExpendGaz(new Vector2Int(item.Value.x, item.Value.y - 1));
                else if (range == ATTACKRANGE.UP)
                    TryExpendGaz(new Vector2Int(item.Value.x, item.Value.y + 1));
                else if (range == ATTACKRANGE.LEFT)
                    TryExpendGaz(new Vector2Int(item.Value.x - 1, item.Value.y));
                else if (range == ATTACKRANGE.RIGHT)
                    TryExpendGaz(new Vector2Int(item.Value.x + 1, item.Value.y));
            }

            if (life > 0)
            {
                foreach (var pos in toAdd)
                    TryAddKey(pos);
            }
            toAdd = new List<Vector2Int>();
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(SO.lifeTime);
        DestroyWithDictionnary();
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
        toAdd.Add(new Vector2Int(item.x + 1, item.y));
        toAdd.Add(new Vector2Int(item.x - 1, item.y));
        toAdd.Add(new Vector2Int(item.x, item.y - 1));
        toAdd.Add(new Vector2Int(item.x, item.y + 1));
       
        toAddPair.Add(new Vector2Int(item.x + 1, item.y + 1));
        toAddPair.Add(new Vector2Int(item.x + 1, item.y - 1));
        toAddPair.Add(new Vector2Int(item.x - 1, item.y - 1));
        toAddPair.Add(new Vector2Int(item.x - 1, item.y + 1));

        toAddImpair.Add(new Vector2Int(item.x + 1, item.y));
        toAddImpair.Add(new Vector2Int(item.x - 1, item.y));
        toAddImpair.Add(new Vector2Int(item.x, item.y + 1));
        toAddImpair.Add(new Vector2Int(item.x, item.y - 1));
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

        allInstantiatePrefab.Add(CodateTagToDictionnary(item.x, item.y), item);
        EnviroManager.Instance.AddElementEvent.Invoke(item.x, item.y, SO.type);
    }

    private void TryExpendGaz(Vector2Int pos)
    {
        if (GF.IsOnBinaryMask(EnviroManager.Instance.binaryMaskMap[pos.x, pos.y].binaryMask, (int)SO.type)
           || GF.IsOnBinaryMask(EnviroManager.Instance.binaryMaskMap[pos.x, pos.y].binaryMask, (int)ELEMENTS.WALL)
           || GF.IsOnBinaryMask(EnviroManager.Instance.binaryMaskMap[pos.x, pos.y].binaryMask, (int)ELEMENTS.FLAMMABLEWALL))
            return;

        life--;
        if (life == 0)
            DestroyWithDictionnary();
        toAdd.Add(pos);
    }

    private void DestroyWithDictionnary()
    {
        timer = SO.lifeTime;
        foreach (var item in allInstantiatePrefab)
            EnviroManager.Instance.RemoveElementEvent.Invoke(item.Value.x, item.Value.y, SO.type);
        Destroy(gameObject);
    }
}
