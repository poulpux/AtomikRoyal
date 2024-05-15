using UnityEngine;

public abstract class SingletonDDOL<T> : MonoBehaviour where T : SingletonDDOL<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

