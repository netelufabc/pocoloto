using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorCreator : MonoBehaviour {
    ///Vai estar no Main Camera, coloca na scene em que vc quer que os meteoros apareçam. 
    ///Meteoros devem fazer para de funcionar os outros botões.
    ///

   
    public static DistractorCreator instance = null;
    private StageManager stageManager;
    private GameObject distractor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        distractor = Resources.Load("Prefabs/Distractor") as GameObject;
    }

    private void Start()
    {
        stageManager = StageManager.instance;
        //StartCoroutine(StartDistractors());
    }

    public IEnumerator StartDistractors()
    {
        yield return new WaitUntil(() => LevelController.TimeIsRunning); //Espera até o tempo começar a rodar para instanciar os distradores
        GameObject silabaDigitada;
        for (int i = 0; i<stageManager.textSlots; i++)
        {
            for (int j = 0; j<stageManager.planetLetters.Length; j++)
            {
                if (LevelController.originalText[i].IndexOf(stageManager.planetLetters[j]) != -1)
                {
                    silabaDigitada = GameObject.Find(string.Concat("Silaba Digitada ", i.ToString()));
                    InstatiateDistractor(silabaDigitada);
                }
            }
        }
    }

    private void InstatiateDistractor(GameObject silabaDigitada)
    {
        GameObject newDistractor;
        newDistractor = Instantiate(distractor, GameObject.Find("Canvas").transform); //Instancia o distrador como filho do canvas
        newDistractor.transform.position = silabaDigitada.transform.position; //E na posição do silaba digitada[i]
    }

}
