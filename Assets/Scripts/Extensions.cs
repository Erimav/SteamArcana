using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Extensions
{
    public static T GetOrAddComponent<T>(this GameObject go)
            where T : Component
            => go.GetComponent<T>() ?? go.AddComponent<T>();

    public static T GetOrAddComponent<T>(this Component obj)
        where T : Component
        => obj.gameObject.GetOrAddComponent<T>();

    public static Transform FindInChildren(this Transform transform, string name)
    {
        if (transform.name == name)
            return transform;

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var result = FindInChildren(child, name);
            if (result != null)
                return result;
        }

        return null;
    }
}

public static class Vector3Extensions
{
    public static Vector3 WithX(this Vector3 vector, float x)
    {
        vector.x = x;
        return vector;
    }

    public static Vector3 WithY(this Vector3 vector, float y)
    {
        vector.y = y;
        return vector;
    }

    public static Vector3 WithZ(this Vector3 vector, float z)
    {
        vector.z = z;
        return vector;
    }

    public static Vector3 WithXY(this Vector3 vector, float x, float y)
    {
        vector.x = x;
        vector.y = y;
        return vector;
    }

    public static Vector3 WithXZ(this Vector3 vector, float x, float z)
    {
        vector.x = x;
        vector.z = z;
        return vector;
    }

    public static Vector3 WithYZ(this Vector3 vector, float y, float z)
    {
        vector.y = y;
        vector.z = z;
        return vector;
    }

    public static Vector3 Abs(this Vector3 vector)
    {
        vector.x = Mathf.Abs(vector.x);
        vector.y = Mathf.Abs(vector.y);
        vector.z = Mathf.Abs(vector.z);
        return vector;
    }
}



