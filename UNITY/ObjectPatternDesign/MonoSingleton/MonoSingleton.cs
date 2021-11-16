using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper to create singleton, public class UneClasse : MonoSingleton<UneClasse>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log(typeof(T).ToString() + " is NULL");
            }
            // Instance requiered for the first time, we look for it
            else
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this as T;
        }

        Init();
    }

    public virtual void Init() { }
}