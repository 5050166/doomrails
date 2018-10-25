using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{

    private static object lockItem = new object();

    private static T instance;

    public static T Instance
    {
        get
        {
            lock (lockItem)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (FindObjectsOfType<T>().Length > 1)
                        Debug.LogError(typeof(T).Name + "的实例在当前场景不唯一");
                    if (instance == null)
                        instance = new GameObject(typeof(T).Name).AddComponent<T>();

                }
                return instance;
            }
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void OnDestroy()
    {
        instance = null;
    }

}