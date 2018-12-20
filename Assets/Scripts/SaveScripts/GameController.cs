using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour {

    /// <summary>
    /// Será implementado depois no LevelController
    /// </summary>


    private static string dataPath = string.Empty;
    public static string slotsDataPath = string.Empty;
    public static SlotsList list = new SlotsList(); 
    public static Player player = new Player();

    public void Awake()
    {
        dataPath = Application.persistentDataPath;
        slotsDataPath = System.IO.Path.Combine(dataPath, "listaDeSlots.json");
        list = SlotsListManager.loadSlotsList(slotsDataPath);
    }

    #region Save&Load

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

    #endregion    
}
