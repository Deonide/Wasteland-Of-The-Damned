using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    //Filename
    private static string fileName = "save.json";

    //Path & Save
    public static void SerializeData(PlayerStats playerdata)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        using (StreamWriter writer = File.CreateText(path))
        {
            string json = JsonUtility.ToJson(playerdata);
            writer.Write(json);
        }
        Debug.Log("saved game to " + path);
    }

    //Load Game
    public static PlayerStats Deserialize()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
        using (StreamReader reader = File.OpenText(path))
        {
            string json = reader.ReadToEnd();
            PlayerStats data = JsonUtility.FromJson<PlayerStats>(json);
            Debug.Log("loaded game from " + path);
            return data;
        }
    }
}