
using System.IO;
using UnityEngine;

public static class DataManager
{
    public static bool SaveData<T>(T data, string path) where T : DataObject
    {
        string jsonContent = JsonUtility.ToJson(data, true);
        if (File.Exists(path))
        {

            File.WriteAllText(path, jsonContent);
            return true;
        }
        else
        {
            Debug.LogWarning("Not found the file from path: " + path);
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(jsonContent);
            }
            return false;
        }
    }
    public static T LoadData<T>(string path) where T : DataObject
    {
        if (File.Exists(path))
        {
            string jsonContent = File.ReadAllText(path);

            return JsonUtility.FromJson<T>(jsonContent);
        }
        else
        {
            Debug.LogWarning("Not found the file from path: " + path);
            return null;
        }
    }
}