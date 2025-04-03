using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<T> de T is een Generic.  
public class SingletonBase<T> : MonoBehaviour where T : Component
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<T>();
                if(m_Instance == null)
                {
                    GameObject _obj = new GameObject();
                    _obj.name = typeof(T).Name;
                    m_Instance = _obj.AddComponent<T>();
                }
            }
            return m_Instance;
        }
    }

    public virtual void Awake()
    {
        if (m_Instance == null) 
        {
            m_Instance = this as T;

        }

        else 
        {
            Destroy(gameObject);
        }
    }
}
