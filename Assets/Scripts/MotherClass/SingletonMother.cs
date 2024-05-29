using UnityEngine;


public abstract class SingletonMother<T> : MonoBehaviour where T : SingletonMother<T>
{
    private static T _instance;
    public static T Instance => _instance;

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = (T)this;
    }
}