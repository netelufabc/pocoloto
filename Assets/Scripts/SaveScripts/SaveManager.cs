using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static int slotsListSize = 6;

    public static string dataPath;
    public static string slotsDataPath;
    public static SlotsList list;
    public static Player player = new Player();

    public void Awake()
    {
        dataPath = Application.persistentDataPath;
        slotsDataPath = System.IO.Path.Combine(dataPath, "listaDeSlots.json");
        list = SlotsListManager.StartList(slotsDataPath);
    }

    #region Save&Load

    public void Save()
    {
        string nomeDoJson = string.Concat(player.slot, ".json");
        string newDataPath = System.IO.Path.Combine(dataPath, nomeDoJson);
        SaveData.SavePlayerData(newDataPath);
    }

    public void Load(int i)
    {
        dataPath = i.ToString();
        SaveData.LoadPlayerData(dataPath + ".json");
    }

    #endregion    
}
