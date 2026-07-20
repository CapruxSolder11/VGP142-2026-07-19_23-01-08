using System;
using System.Linq.Expressions;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {typeof(T)} is needed in the scene, but there is none.");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning($"An instance of {typeof(T)} already exists. Destroying the new instance.");
            Destroy(gameObject);
            return;
        }

        _instance = this as T;
        if (_instance == null)
        {
            throw new InvalidCastException($"The instance of {typeof(T)} could not be cast to the correct type.");
        }
    
        DontDestroyOnLoad(gameObject);
    }
}
