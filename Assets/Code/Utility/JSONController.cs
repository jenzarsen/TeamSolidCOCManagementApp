using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONController : MonoBehaviour
{
    public static SaveData.Clan CreateClanFromJSON(string json)
    {
        return JsonUtility.FromJson<SaveData.Clan>(json);
    }
}
