using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class GF 
{
    public static Vector2Int EnterRealPositionInEnviroTab(Vector2 position)
    {
        //TO TEST
        return new Vector2Int((int)((position.x / _StaticEnvironement.tabResolution) - _StaticEnvironement.originX) +1 , (int)((position.y / _StaticEnvironement.tabResolution) - _StaticEnvironement.originY)+ 1);
    }

    public static bool IsOnBinaryMask(int mask, int position) =>
    (mask & (1 << position)) != 0;

    public static void AddInBinaryMask(ref int mask, int position) 
    {
        mask |= (1 << position);
    }

    public static void RemoveInBinaryMask(ref int mask, int position)
    {
        mask &= ~(1 << position);
    }

    public static T GetMaxValue<T>() where T : Enum =>
        Enum.GetValues(typeof(T)).Cast<T>().Max();

    static public void CopyFields<TSource, TTarget>(TSource source,TTarget target)
    {
        if (source == null || target == null) return;

        FieldInfo[] sourceFields = typeof(TSource).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        FieldInfo[] targetFields = typeof(TTarget).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var sourceField in sourceFields)
        {
            var targetField = System.Array.Find(targetFields, f => f.Name == sourceField.Name);
            if (targetField != null && targetField.FieldType == sourceField.FieldType)
            {
                targetField.SetValue(target, sourceField.GetValue(source));
            }
        }
    }

    static public T SetScripts<T>(TextAsset roundScript, GameObject target) where T : MonoBehaviour
    {
        System.Type t = System.Type.GetType(roundScript.name.Replace(".cs", ""));
        T component = target.AddComponent(t) as T;

        if (component == null)
        {
            Debug.LogError("Failed to add component of type: " + typeof(T).Name);
            return null;
        }

        return component;
    }

    public static T GetScript<T>(TextAsset textAsset) where T : class
    {
        if (textAsset == null)
        {
            Debug.LogError("TextAsset is null.");
            return null;
        }

        Type type = System.Type.GetType(textAsset.name.Replace(".cs", ""));

        if (type == null)
        {
            Debug.LogError("Type not found: " + type);
            return null;
        }

        // Check if the type can be cast to T
        if (!typeof(T).IsAssignableFrom(type))
        {
            Debug.LogError("Type " + type + " is not assignable to " + typeof(T).Name);
            return null;
        }

        // Create an instance of the type
        T instance = Activator.CreateInstance(type) as T;

        if (instance == null)
        {
            Debug.LogError("Failed to create an instance of: " + type);
        }

        return instance;
    }

    static public void RemoveInterdiction(ref List<string> listOfInterdiction, string interdictionName)
    {
        listOfInterdiction.RemoveAll(item => item == interdictionName);
    }

    static public GameObject GetPrefabAdress(string prefabPath)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab == null)
            Debug.LogError("Prefab not found at path: " + prefabPath);
        return prefab;
    }
}
