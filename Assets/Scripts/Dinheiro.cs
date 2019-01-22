using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dinheiro : EfeitosDeAcertoErro {

    public static void Create(int valor, Vector3 local)
    {
        GameObject dinheiro = Resources.Load("Prefabs/Feedback/Dinheiro") as GameObject;
        GameObject newDinheiro = Instantiate(dinheiro, GameObject.Find("Canvas").transform);
        newDinheiro.transform.position = local;
        newDinheiro.GetComponentInChildren<Text>().text = valor.ToString();
    }
}