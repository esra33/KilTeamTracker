using System.IO;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalSaveManager", menuName = "Manager/LocalSaveManager")]
public class LocalSaveManager : SaveManager
{
    public override void LoadJson<T>(string name, Action<T> onComplete)
    {
        T result = default(T);
        string path = GetPath(name);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            result = JsonUtility.FromJson<T>(json);
        }

        if(onComplete != null)
        {
            onComplete(result);
        }
    }

    public override void SaveJson<T>(string name, T inputObject, Action<bool> onComplete)
    {
        string path = GetPath(name);
        string json = JsonUtility.ToJson(inputObject);
        File.WriteAllText(path, json);

        if(onComplete != null)
        {
            onComplete(true);
        }
    }

    private string GetPath(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
#if DEBUG
        Debug.Log(path);
#endif
        return path;
    }
}
