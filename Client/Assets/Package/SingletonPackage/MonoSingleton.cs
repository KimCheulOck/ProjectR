using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T instance = null;
    public static T Instance { get { return instance; } }

    public virtual void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        Initialize();
    }

    private void Initialize()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(T)) as T;

            if (instance == null)
                instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
        }
    }
}
