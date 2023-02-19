using System;
using UnityEngine;

public class Util
{

    public static T GetOrAddComponent<T>(GameObject go) where T: UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static GameObject FindChild(GameObject go, String name = null, bool recursive = false)
    {
        Transform transforms = FindChild<Transform>(go, name, recursive);
        if (transforms == null)
            return null;
        
        return transforms.gameObject;
    }
    
    public static T FindChild<T>(GameObject go, String name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) return null;
        if (recursive)
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (String.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (String.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null) return component;
                }
            }
        }
        return null;

    }
    
}
