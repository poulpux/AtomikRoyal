using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class EnviroPrefabSpawner : MonoBehaviour
{
    private List<Dictionary<string, GameObject>> allInstantiatePrefab = new List<Dictionary<string, GameObject>>();
    void Start()
    {
        StartCoroutine(SecondStartCoroutine());
    }

    private IEnumerator SecondStartCoroutine()
    {
        yield return new WaitForEndOfFrame();
        InstantiateAll();
        EnviroManager.Instance.AddElementEvent.Invoke(5, 5, ELEMENTS.FIRE);
    }

    private void InstantiateAll()
    {
        for (int i = 0; i < (int)GF.GetMaxValue<ELEMENTS>(); i++)
            allInstantiatePrefab.Add(new Dictionary<string, GameObject>());

        EnviroManager.Instance.AddElementEvent.AddListener((x, y, element) => InstantiatePrefab(x, y, element));
        EnviroManager.Instance.RemoveElementEvent.AddListener((x, y, element) => RemovePrefab(x, y, element));
    }

    private void InstantiatePrefab(int x,int y, ELEMENTS element)
    {
        string tag = CodateTagToDictionnary(x,y);
        print(tag);
        if (allInstantiatePrefab[(int)element].ContainsKey(tag)) return;
        GameObject currentPrefab = Instantiate(GetGoodPrefab((int)element));
        currentPrefab.transform.position = new Vector2(x, y);
        allInstantiatePrefab[(int)element].Add(tag, currentPrefab);
    }

    private void RemovePrefab(int x, int y, ELEMENTS element)
    {
        string tag = CodateTagToDictionnary(x, y);
        if (!allInstantiatePrefab[(int)element].ContainsKey(tag)) return;
        Destroy(allInstantiatePrefab[(int)element][tag]);
        allInstantiatePrefab[(int)element].Remove(tag);
    }

    private string CodateTagToDictionnary(int x, int y)
    {
        return x.ToString() + y.ToString();
    }

    private GameObject GetGoodPrefab(int enumNumber)
    {
        if (enumNumber == 0)
            return _StaticEnvironement.wallPrefab;
        else if (enumNumber == 1)
            return _StaticEnvironement.flammableWallPrefab;
        else if (enumNumber == 2)
            return _StaticEnvironement.waterPrefab;
        else if (enumNumber == 3)
            return _StaticEnvironement.bushPrefab;
        else if (enumNumber == 4)
            return _StaticEnvironement.gluePrefab;
        else if (enumNumber == 5)
            return _StaticEnvironement.smokePrefab;
        else if (enumNumber == 6)
            return _StaticEnvironement.gazPrefab;
        else if (enumNumber == 7)
            return _StaticEnvironement.firePrefab;
        else if (enumNumber == 8)
            return _StaticEnvironement.explosionPrefab;

        return _StaticEnvironement.waterPrefab;
    }
}
