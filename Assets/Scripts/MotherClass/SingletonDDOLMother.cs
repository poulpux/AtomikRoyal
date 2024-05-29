using UnityEngine;

public abstract class SingletonDDOLMother<T> : MonoBehaviour where T : SingletonDDOLMother<T>
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

