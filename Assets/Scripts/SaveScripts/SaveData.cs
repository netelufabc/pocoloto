using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class SaveData: MonoBehaviour {

    public static void SavePlayerData(string path)
    {
        if(SaveManager.player.slot == -1) //O número -1 é dado caso não haja mais espaços livres para salvar o jogo.
        {
            Debug.Log("Não foi possível criar um save");
        }

        else
        {
            SlotsListManager.RetiraKey(SaveManager.player.slot);
            string json = JsonUtility.ToJson(SaveManager.player);

            StreamWriter sw = File.CreateText(path);
            sw.Close();

            File.WriteAllText(path, json);
        }
    }

    public static void LoadPlayerData(string path)
    {
        
    }

    public void CreateNewPlayer()
    {
        SaveManager.player.nome = GameObject.Find("NomeText").GetComponent<Text>().text;
        int.TryParse(GameObject.Find("IdadeText").GetComponent<Text>().text, out SaveManager.player.idade);
        int.TryParse(GameObject.Find("SerieText").GetComponent<Text>().text, out SaveManager.player.serie);
        SaveManager.player.slot = SlotsListManager.SlotGiver(SaveManager.list);

    }

    public void DeletePlayer(int slot)
    {
        //public int slot; //Ainda temos que saber como iremos pegar o slot que queremos deletar. 

        string stringSlot = (slot.ToString() + ".json");

        System.IO.File.Delete(System.IO.Path.Combine(SaveManager.dataPath, stringSlot));
        SlotsListManager.ReturnSlot(slot);
        
    } 
}
