using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableObjectWithScript : ScriptableObject
{
    public TextAsset script;

    protected void VerifType<T>()
    {
        if (script == null)
            throw new System.Exception("No reference on " + typeof(T) + " script");

        System.Type t = System.Type.GetType(script.name.Replace(".cs", ""));

        if (t == null)
        {
            script = null;
            throw new System.Exception("The referenced asset is not a script");
        }

        if (t.BaseType != typeof(T))
        {
            script = null;
            throw new System.Exception("The referenced script is not an " + typeof(T));
        }
    }
}
