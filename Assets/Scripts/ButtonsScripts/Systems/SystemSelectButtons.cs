using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class SystemSelectButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


    private bool sistemasLiberados;
    public int systemNumber;
    private string systemName;
    public bool clicked = false;
    private GameObject anotherSystem;
    private Text textoPainelInferior;
    public string textoSystema;
    private GameObject levelDoneFlag;

    // Libera a seleção do sistema no systemSelect
    void Start () {
        levelDoneFlag = Resources.Load("Prefabs/LevelDoneFlag") as GameObject;
        sistemasLiberados = SaveManager.player.sistemaLiberado;
        systemName = this.GetComponent<UnityEngine.UI.Button>().name;
        systemNumber = System.Int32.Parse(systemName.Substring(systemName.Length - 1));
        textoPainelInferior = GameObject.Find("Panel Text").GetComponent<UnityEngine.UI.Text>();

        if (sistemasLiberados)
        {
            this.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

        if (SaveManager.player.CompletouSistema(systemNumber))
        {            
            GameObject newLevelDoneFlag;
            newLevelDoneFlag = Instantiate(levelDoneFlag, GameObject.Find("Canvas").transform); //Instancia o distrador como filho do canvas
            newLevelDoneFlag.transform.position = new Vector3(this.transform.position.x + 0.9f, this.transform.position.y + 0.55f, this.transform.position.z); //E na posição do planeta
            //Instantiate(levelDoneFlag, new Vector3(this.transform.position.x + 0.9f, this.transform.position.y + 0.55f, this.transform.position.z),new Quaternion(0,0,0,0), GameObject.Find("Canvas").transform);
            if (SaveManager.player.ZerouSistema(systemNumber))
            {
                newLevelDoneFlag.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void OnClick()
    {
        if (clicked)
        {
            this.GetComponent<LoadScene>().LoadSceneWithFade(string.Concat("07_stageSelect ", systemNumber.ToString()));
        }
        else
        {
            for (int i = 0; i < SystemRecognizer.numberOfSystems; i++)
            {
                anotherSystem = GameObject.Find(string.Concat("Sistema ", i.ToString()));
                anotherSystem.GetComponent<SystemSelectButtons>().clicked = false;
                anotherSystem.transform.GetChild(0).gameObject.SetActive(false);
            }
            this.clicked = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
            SystemRecognizer.ReadyToGoToSystem(systemNumber);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().interactable)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            textoPainelInferior.text = textoSystema;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().interactable && !clicked)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            textoPainelInferior.text = "";
        }
    }
}
