using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/GameSave";
    private static readonly object fileLock = new object();

    public static void SaveData(GameSaveData data)
    {
        lock (fileLock)
        {
            Debug.Log("Saving game: " + data.ToString());

            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/GameSave";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static GameSaveData LoadData()
    {
        lock (fileLock)
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                if (stream.Length > 0)
                {
                    GameSaveData data = formatter.Deserialize(stream) as GameSaveData;
                    stream.Close();
                    Debug.Log("Loaded game save: " + data.ToString());
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
    }
}
