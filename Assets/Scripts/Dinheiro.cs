using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dinheiro : EfeitosDeAcertoErro {

    public static void CreateGain(int valor, Vector3 local)
    {
        GameObject dinheiro = Resources.Load("Prefabs/Feedback/Dinheiro") as GameObject;
        GameObject newDinheiro = Instantiate(dinheiro, GameObject.Find("Canvas").transform);
        newDinheiro.transform.position = local;
        newDinheiro.GetComponentInChildren<Text>().text = string.Concat("+",valor.ToString());
    }

    public static void CreateLose(int valor, Vector3 local)
    {
        GameObject dinheiro = Resources.Load("Prefabs/Feedback/Dinheiro") as GameObject;
        GameObject newDinheiro = Instantiate(dinheiro, GameObject.Find("Canvas").transform);
        newDinheiro.transform.position = local;
        newDinheiro.GetComponentInChildren<Text>().text = string.Concat("-", valor.ToString());
        newDinheiro.GetComponentInChildren<Text>().color = new Color(0.8962264f, 0.03822445f, 0f, 1f);
    }
}