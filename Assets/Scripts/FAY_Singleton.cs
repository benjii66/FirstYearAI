using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAY_Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance = default(T);

    public static T Instance => instance;

    public virtual bool IsValid => instance;

    private void Awake()
    {
        if (Instance != null && this != instance)
        {
            Destroy(this);
            return;
        }
        instance = this as T;
        name += $"[ {typeof(T).Name} ]";

    }
}
