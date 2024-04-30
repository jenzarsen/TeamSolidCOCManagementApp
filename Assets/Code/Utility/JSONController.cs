using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONController : MonoBehaviour
{
    public static T CreateFromJSON<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
