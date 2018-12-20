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
        GameController.player.slot = 0;
    }

    public static void SavePlayerData(string path)
    {
        string json = JsonUtility.ToJson(GameController.player);

        StreamWriter sw = File.CreateText(path);
        sw.Close();

        File.WriteAllText(path, json);
        Debug.Log("Terminou de escrever");
    }

    public static void LoadPlayerData(string path)
    {
        
    }

}
