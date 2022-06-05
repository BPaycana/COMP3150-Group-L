using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveInfo(bool gameMode, bool moveControls, bool interactControls, int level1TimeMin, int level1TimeSec, int level1TimeMs, int level2TimeMin,
        int level2TimeSec, int level2TimeMs, int level3TimeMin, int level3TimeSec, int level3TimeMs)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/LunchTimeRushData.GameData";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameMode, moveControls, interactControls, level1TimeMin, level1TimeSec, level1TimeMs, level2TimeMin,
        level2TimeSec, level2TimeMs, level3TimeMin, level3TimeSec, level3TimeMs);

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
    public static void SaveInfo(bool gameMode, bool moveControls, bool interactControls, string level1Time, string level2Time, string level3Time)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/LunchTimeRushData.GameDataBoogaloo";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameMode, moveControls, interactControls, level1Time, level2Time, level3Time);

        formatter.Serialize(stream, data);
        stream.Close();
    }

        public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/LunchTimeRushData.GameDataBoogaloo";
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
