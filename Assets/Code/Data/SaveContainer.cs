using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Sirenix.OdinInspector;

public class SaveContainer : Singleton<SaveContainer>
{
    [SerializeField]
    public static SaveData saveData;

    public static bool IsDirty = false;
    string savePath => Application.persistentDataPath.ToString() + "/playerData.json";

    protected override void Awake()
    {
        base.Awake();
        LoadSaveData();
    }

    private void Update()
    {
        if(IsDirty)
        {
            IsDirty = false;
            SavePlayerData();
        }
    }

    public SaveData CreateNewPlayerData()
    {
        return new SaveData();
    }

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);

        Debug.Log("Save successfully");
    }

    public void LoadSaveData()
    {
        Debug.Log($"Loading data at path {savePath}...");

        if(File.Exists(savePath))
        {
            try
            {
                var json = File.ReadAllText(savePath);
                saveData = JsonUtility.FromJson<SaveData>(json);
                Debug.Log("Data loaded successfully...");
                return;
            }
            catch
            {
                File.Delete(savePath);
                Debug.Log("Failed to load save..Deleting corrupted save..");
                saveData = CreateNewPlayerData();
            }
           
        }
        else
        {
            saveData = CreateNewPlayerData();
            SetDirty();
        }
    }

    public static void SetDirty()
    {
        IsDirty = true;
    }


#if UNITY_EDITOR

    [Button]
    public void AddPlayer()
    {
        saveData.AddPlayer("test", "Jenz");
        IsDirty = true;
    }

    [Button]
    public void ViewPlayerList()
    {
        saveData.playerList.ForEach(x => Debug.Log(x.name));
    }

    [Button]
    public void LoadPlayer(string name)
    {
        if(saveData == null)
        {
            Debug.LogError("Save data does not exist.");
            return;
        }

        var player = saveData.GetPlayerByName(name);

        if (player == null)
        {
            Debug.LogError("Player not found.");
            return;
        }
    }

    [Button]
    public void DeleteSaveData()
    {
        if(File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save deleted successfully");
        }
        else
        {
            Debug.Log("No save file found");
        }
    }
#endif
}
