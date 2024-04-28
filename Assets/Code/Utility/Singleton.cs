using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton <T> : MonoBehaviour
{
    public static T instance;

    public bool dontDestroyOnLoad = false;
    protected virtual void Awake()
    {
        LoadSingletonInstance();
    }
    
    void LoadSingletonInstance()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<T>();
        }
        else
        {
            Destroy(gameObject);
        }

        if(dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
