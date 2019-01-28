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
    public Image starPoint;
    [Tooltip("Número do planeta correspondente a fase de revisão do sistema anterior")]
    public int numPlanetaRSAnterior = 0;
    [Tooltip("Imagem que aparece quando se seleciona o planeta pela primeira vez")]
    public GameObject imagemIniciarPlaneta;
    // Var para instanciar a imagem
    private GameObject initImage;

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

        // Desabilita o botão que está no painel para entrar no planeta
        startPlanet = GameObject.Find("StartPlanet").GetComponent<Button>();
        startPlanet.interactable = false;
        
        // Exibe o total de estrelas 
        loadScene = infoPanel.GetComponent<LoadScene>();
        GameObject.Find("TotalStars").GetComponent<Text>().text = "x" + SaveManager.player.totalEstrelas.ToString();
    }

    /// <summary>
    /// Atuializa o texto do Panel Progress para o string fornecida e com a informação extra do planeta
    /// </summary>
    /// <param name="newText"></param>
    public void UpdateInfoText(string newText, int planetNumber)
    {
        //Debug.Log(planetNumber);
        infoText.text = ActInfo(planetNumber) + newText;
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
    ///  Retorna a informação ao Panel Progress, dependeno do estado anterior
    /// </summary>
    public void RestoreInfoText()
    {
        string tempInfoText = "";
        int chosenPlanetNumber = FindChosenPlanet();
        DestroyStars();

        if (chosenPlanetNumber != -1)
        {
            tempInfoText = ActInfo(chosenPlanetNumber + numPlanetaRSAnterior);
        }
        infoText.text = tempInfoText + previousInfoText;
    }

    /// <summary>
    /// Função que retorna a posição no vetor planets do planeta que está selecionado
    /// </summary>
    /// <returns></returns>
    public int FindChosenPlanet()
    {
        for (int i = 1; i <= planets.Length; i++)
        {
            if (IsPlanetChosen(i + numPlanetaRSAnterior))
            {
                return i;
            }
        }

        //Debug.LogError("Nenhum planeta escolhido!");
        return -1;
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
            // Verifica se o número do planeta no objeto é igual ao número passado a função
            // Se for, coloca true no vetor chosenPlanet
            if (planets[i].GetComponent<StageSelectButtons>().GetPlanetNumber() == planetNumber)
            {
                chosenPlanet[i] = true;
                planets[i].GetComponent<StageSelectButtons>().goToPlanet = true;
                Destroy(initImage);
                initImage = Instantiate(imagemIniciarPlaneta, GameObject.Find("Canvas").transform);
                initImage.transform.position = planets[i].transform.position;
            }
            // Caso contrário coloca false
            else
            {
                chosenPlanet[i] = false;
                planets[i].GetComponent<StageSelectButtons>().StopPlanetAnimation();
                planets[i].GetComponent<StageSelectButtons>().goToPlanet = false;
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
            //Debug.Log(planetNumber + " " + planets[i].GetComponent<StageSelectButtons>().GetPlanetNumber() + " IsPlanetChosen: " + i);
            // Verifica se o número do planeta no objeto é igual ao número passado a função
            if (planets[i].GetComponent<StageSelectButtons>().GetPlanetNumber() == planetNumber)
            {
                return chosenPlanet[i];
            }
        }

        Debug.LogError("Planeta não encontrado!");
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

    /// <summary>
    /// Destroi as estrelas criadas para indicar a pontuação
    /// </summary>
    public void DestroyStars()
    {
        GameObject[] starsToDestroy = GameObject.FindGameObjectsWithTag("Star");

        for (int i = 0; i < starsToDestroy.Length; i++)
        {
            Destroy(starsToDestroy[i]);
        }
    }

    /// <summary>
    /// Retorna a informação de cada ato (pontuação) para o planeta selecionado
    /// </summary>
    /// <param name="planetNumber"></param>
    /// <returns></returns>
    public string ActInfo(int planetNumber)
    {
        //Debug.Log(planetNumber);
        Image tempStarPoint;
        string actInfo = "";

        for (int i = 0; i < SaveManager.player.planeta[planetNumber - 1].ato.Length; i++)
        {
            actInfo = actInfo + "Ato " + (i + 1) + ": \n\n";
            //Debug.Log("Planeta " + planetNumber + " Ato " + i);
            for (int j = 0; j < SaveManager.player.planeta[planetNumber - 1].ato[i]; j++)
            {
                tempStarPoint = Instantiate(starPoint, transform);
                tempStarPoint.transform.localPosition = new Vector3 (12f*j - 12f, -22.5f*i + 7f, 0);
                tempStarPoint.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
            }
        }
        return actInfo;
    }
}