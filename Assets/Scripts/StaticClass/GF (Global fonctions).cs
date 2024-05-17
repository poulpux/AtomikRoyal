using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class GF 
{
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

    static public void SetGameMod<T>(TextAsset roundScript, GameObject target) where T : MonoBehaviour
    {
        System.Type t = System.Type.GetType(roundScript.name.Replace(".cs", ""));
        T component = target.AddComponent(t) as T;

        if (component == null)
        {
            Debug.LogError("Failed to add component of type: " + typeof(T).Name);
            return;
        }
    }
}
