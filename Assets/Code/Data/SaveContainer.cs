using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Sirenix.OdinInspector;

public class SaveContainer : Singleton<SaveContainer>
{
    [SerializeField]
    public static SaveData saveData;


    public class SaveWrapper
    {
        public SaveData save;
    }

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

    [Button]
    public void ClearData()
    {
        if (File.Exists(savePath))
        {
            saveData = new();
            File.Delete(savePath);
            SetDirty();
            Debug.Log("Save deleted successfully");
        }
        else
        {
            Debug.Log("No save file found");
        }
    }
}
