using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelProgressController : MonoBehaviour {

    private GameObject infoPanel;
    private Text infoText;
    private string previousInfoText = "";
    private GameObject[] planets;
    private bool[] chosenPlanet;
    private Button startPlanet;
    public static PanelProgressController instance = null;
    private LoadScene loadScene;

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
    }

    void Start () {
        // Encontra o painel
        infoPanel = GameObject.Find("Panel Progress");
        
        // Carrega a imagem do avatar para colocar no painel
        Sprite avatar = Resources.Load<Image>("Prefabs/Avatar/Avatar0" + (SaveManager.player.avatarSelecionadoIndex + 1).ToString()).sprite;
        // Encontra a imagem do painel e muda para o avatar selecionado
        GameObject panelImage = infoPanel.transform.GetChild(1).gameObject;
        panelImage.GetComponent<Image>().sprite = avatar;
        // Encontra a caixa de texto do painel e inicializa com o texto contido em previousInfoText
        infoText = infoPanel.transform.GetChild(0).gameObject.GetComponent<Text>();
        infoText.text = previousInfoText;

        // Encontra todos os planetas da tela
        planets = GameObject.FindGameObjectsWithTag("Planetas");
        chosenPlanet = new bool[planets.Length];

        // Completa o vetor chosenPlanet com false (inicialmente nenhum planeta foi escolhido)
        for (int i = 0; i < planets.Length; i++)
        {
            chosenPlanet[i] = false;
        }

        startPlanet = GameObject.Find("StartPlanet").GetComponent<Button>();
        startPlanet.interactable = false;

        loadScene = infoPanel.GetComponent<LoadScene>();
    }

    /// <summary>
    /// Atuializa o texto do Panel Progress para o string fornecida
    /// </summary>
    /// <param name="newText"></param>
    public void UpdateInfoText(string newText)
    {
        infoText.text = newText;
    }

    /// <summary>
    ///  Retorna o texto do Panel Progress para a string guardada na memória
    /// </summary>
    public void RestoreInfoText()
    {
        infoText.text = previousInfoText;
    }

    /// <summary>
    /// Substitui a string guardad na memória pela string fornecida
    /// </summary>
    /// <param name="newText"></param>
    public void ReplaceInfoText(string newText)
    {
        previousInfoText = newText;
    }

    /// <summary>
    /// Marca o planeta (informado pelo planetNumber) como escolhido e desmarca os outros, parando suas animações (se estiverem tocando)
    /// </summary>
    /// <param name="planetNumber"></param>
    /// <param name="eventData"></param>
    public void ChoosePlanet(int planetNumber, PointerEventData eventData)
    {
        for (int i = 0; i < planets.Length; i++)
        {
            if (planets[i].GetComponent<StageSelectButtons>().GetPlanetNumber() == planetNumber)
            {
                chosenPlanet[i] = true;
            }
            else
            {
                chosenPlanet[i] = false;
                planets[i].GetComponent<StageSelectButtons>().StopPlanetAnimation();
            }
        }
    }

    /// <summary>
    /// Verifica se o planeta informado pelo planetNumber está escolhido ou não
    /// </summary>
    /// <param name="planetNumber"></param>
    /// <returns></returns>
    public bool IsPlanetChosen(int planetNumber)
    {
        for (int i = 0; i < chosenPlanet.Length; i++)
        {
            if (planets[i].GetComponent<StageSelectButtons>().GetPlanetNumber() == planetNumber)
            {
                return chosenPlanet[i];
            }
        }

        Debug.Log("Planeta não encontrado!");
        return false;
    }

    /// <summary>
    /// Habilita o botão para ir ao planeta
    /// </summary>
    public void ReadyToGo()
    {
        startPlanet.interactable = true;
    }

    /// <summary>
    /// Carrega a scene do planeta selecionado
    /// </summary>
    public void GoToPlanet()
    {
        for (int i = 0; i < chosenPlanet.Length; i++)
        {
            if (chosenPlanet[i])
            {
                loadScene.LoadSceneWithFade(planets[i].GetComponent<StageSelectButtons>().GetPlanetLevel());
            }
        }
    }

    public string ActInfo(int planetNumber)
    {
        string actInfo = "";

        for (int i = 0; i < SaveManager.player.planeta[planetNumber - 1].ato.Length; i++)
        {
            actInfo += "Ato " + (i + 1) + ": \n";
        }
        return actInfo;
    }
}