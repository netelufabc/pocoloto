using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour {

    /// <summary>
    /// Será implementado depois no LevelController
    /// </summary>


    private static string dataPath = string.Empty;
    //private 
    public static Player player = new Player();

    public void Awake()
    {
        dataPath = Application.persistentDataPath;
    }

    public void Save()
    {
        string nomeDoJson = string.Concat(player.slot, ".json");
        string newDataPath = System.IO.Path.Combine(dataPath, nomeDoJson);
        SaveData.SavePlayerData(newDataPath);
    }

    public void Load()
    {
        SaveData.SavePlayerData(dataPath);
    }

     public void FuncaoX()
    {
        DirectoryInfo dir = new DirectoryInfo(dataPath);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            Debug.Log(f);
        }
    }
}
