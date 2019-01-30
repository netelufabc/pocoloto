using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemRecognizer : MonoBehaviour {

    public static int numberOfSystems;
    public static GameObject[] systems;
    [Tooltip("Imagem que aparece sobre o sistema avisando que pode iniciar ao clicar em cima")]
    public GameObject imagemIniciarSistema;
    // Var para instanciar a imagem
    private static GameObject initImage;
    
    /// <summary>
    /// Verifica o numero de sistemas que há no início da scene para saber até onde as instâncias SystemSelectButton pode ir;
    /// </summary>
    private void Awake()
    {
        numberOfSystems = 0;
        int i = 0;
        while (GameObject.Find(string.Concat("Sistema ", i.ToString())) != null)
        {
            numberOfSystems++;
            i++;
        }
    }

    private void Start()
    {
        FindAndOrganizeSystems();
    }

    /// <summary>
    /// Encontra todos os sistemas e organiza em um vetor de GameObjects de acordo com o systemNumber
    /// </summary>
    private void FindAndOrganizeSystems()
    {
        GameObject[] tempSystems = GameObject.FindGameObjectsWithTag("Sistemas");
        systems = new GameObject[tempSystems.Length];

        for (int i = 0; i < tempSystems.Length; i++)
        {
            systems[tempSystems[i].GetComponent<SystemSelectButtons>().systemNumber] = tempSystems[i];
           // Debug.Log(tempSystems[i].name + " " + tempSystems[i].GetComponent<SystemSelectButtons>().systemNumber + "-" + i);
        }

        /*
        for (int i = 0; i < systems.Length; i++)
        {
            Debug.Log(systems[i].GetComponent<SystemSelectButtons>().systemNumber + "-" + i);
        }*/
    }

    /// <summary>
    /// Instancia a imagem imagemIniciarSistema sobre o sistema escolhido para facilitar a visualização de que deve ser
    /// selecionado novamente para entrar
    /// </summary>
    /// <param name="systemNumber"></param>
    public static void ReadyToGoToSystem(int systemNumber)
    {
        SystemRecognizer systemRecognizer = GameObject.Find("Main Camera").GetComponent<SystemRecognizer>();

        Destroy(initImage);
        initImage = Instantiate(systemRecognizer.imagemIniciarSistema, GameObject.Find("Canvas").transform);
        initImage.transform.position = systems[systemNumber].transform.position;
    }
}
