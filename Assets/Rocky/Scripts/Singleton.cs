using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    public virtual void Awake()
    {
        instance = this as T;
    }
}
