using UnityEngine;

/// <summary>
/// Базовый класс реализации паттерна Singleton.
/// </summary>
/// <typeparam name="T"> Тип класса. </typeparam>
[DisallowMultipleComponent]
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Флаг уничтожения объекта.
    /// </summary>
    [Header("Singleton")]
    [SerializeField] private bool m_DontDestroyOnLoad;

    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already exists, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        Instance = this as T;

        if (m_DontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
 }
