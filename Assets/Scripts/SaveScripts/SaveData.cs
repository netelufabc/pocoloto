using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class SaveData: MonoBehaviour {

    public void CreateNewPlayer()
    {
        GameController.player.nome = GameObject.Find("NomeText").GetComponent<Text>().text;
        int.TryParse(GameObject.Find("IdadeText").GetComponent<Text>().text, out GameController.player.idade);
        int.TryParse(GameObject.Find("SerieText").GetComponent<Text>().text, out GameController.player.serie);
        GameController.player.slot = SlotsListManager.SlotGiver(GameController.list);

    }

    public static void SavePlayerData(string path)
    {
        if(GameController.player.slot == -1)
        {
            Debug.Log("Não foi possível criar um save");
        }

        else
        {
            SlotsListManager.RetiraKey(GameController.player.slot);
            string json = JsonUtility.ToJson(GameController.player);

            StreamWriter sw = File.CreateText(path);
            sw.Close();

            File.WriteAllText(path, json);
            Debug.Log("Terminou de escrever");
        }
    }

    public static void LoadPlayerData(string path)
    {
        
    }

}
