using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveInfo(bool gameMode, bool moveControls, bool interactControls)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/LunchTimeRushData.GameData";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameMode, moveControls, interactControls);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/LunchTimeRushData.GameData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Data not found in " + path);
            return null;
        }
    }

    /*
    public static void SaveTime(string level1Time, string level2Time, string level3Time)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/LunchTimeRushData.TimeData";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new TimeData(level1Time, level2Time, level3Time);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadTime()
    {
        string path = Application.persistentDataPath + "/LunchTimeRushData.TimeData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Data not found in " + path);
            return null;
        }
    }
    */
}
