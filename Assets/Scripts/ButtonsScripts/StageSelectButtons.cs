using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class StageSelectButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{

    public VideoClip video;
    private Button planeta;
    private string toolTipText;
    private Animator planetaMouseOverAnimation;
    private Text planetNameText;
    private int planetNumber;
    private bool rotate;
    private GameObject levelDoneFlag;
    public string textoPlanetaLiberado;

    private PanelProgressController panelProgressController;
    public string planetFirstScene;

    public bool goToPlanet;

    private void Awake()
    {
        levelDoneFlag = Resources.Load("Prefabs/LevelDoneFlag") as GameObject;
    }

    void Start () {
        planeta = this.GetComponent<UnityEngine.UI.Button>();
        planetaMouseOverAnimation = GameObject.Find(planeta.name).GetComponent<Animator>();
        planetNameText = GameObject.Find("Nome Planeta").GetComponent<UnityEngine.UI.Text>();

        planetNumber = System.Int32.Parse(planeta.name.Substring(7)); //planeta.name.Length - 1));

        panelProgressController = PanelProgressController.instance;
        goToPlanet = false;
        if (SaveManager.player.planeta[planetNumber - 1].liberado)
        {
            this.GetComponent<Button>().interactable = true;
        }

        if (SaveManager.player.CompletouPlaneta(planetNumber))
        {
            GameObject newLevelDoneFlag;
            newLevelDoneFlag = Instantiate(levelDoneFlag, GameObject.FindGameObjectWithTag("Canvas").transform); //Instancia o distrador como filho do canvas
            newLevelDoneFlag.transform.position = new Vector3(this.transform.position.x + 0.9f, this.transform.position.y + 0.55f, this.transform.position.z); //E na posição do planeta
            if (SaveManager.player.ZerouPlaneta(planetNumber))
            {
                newLevelDoneFlag.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
	
	void Update () {
        if (rotate)
        {
            transform.Rotate(new Vector3(0, 0, -5));
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!panelProgressController.IsPlanetChosen(planetNumber))
        {
            panelProgressController.DestroyStars();
            if (planeta.interactable == false)
            {
                panelProgressController.UpdateInfoText(GetText());
            }
            else
            {
                panelProgressController.UpdateInfoText(GetText(), planetNumber);
                if (planeta.interactable == true)
                {
                    StartPlanetAnimation();
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!panelProgressController.IsPlanetChosen(planetNumber))
        {
            panelProgressController.RestoreInfoText();
            if (planeta.interactable == true)
            {
                StopPlanetAnimation();
            }
        }
    }
    
    public string GetText()
    {
        if (planeta.interactable == true)
        {
            return textoPlanetaLiberado;
        }
        else
        {
            planetNumber = System.Int32.Parse(planeta.name.Substring(7));            
            return "Para acessar o Planeta " + planetNumber + ", primeiro passe pelo Planeta " + (planetNumber - 1) + "!";           
            //panelProgressController.DestroyStars();
            //return "";
        }
    }

    /// <summary>
    /// Se o planeta foi selecionado (click), marca como selecionado e permite entrar na fase
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick (PointerEventData eventData)
    {
        if (planeta.interactable == true)
        {
            if (goToPlanet)
            {
                if (video == null)
                {
                    panelProgressController.GoToPlanet();
                }
                else
                {
                    panelProgressController.GoToPlanet(video);
                }
            }
            else
            {
                string tempText = GetText();
                panelProgressController.DestroyStars();
                panelProgressController.UpdateInfoText(tempText, planetNumber);
                panelProgressController.ReplaceInfoText(tempText);
                panelProgressController.ChoosePlanet(planetNumber, eventData);
                panelProgressController.ReadyToGo();
                ShowPlanetName();
            }
        }
    }

    /// <summary>
    /// Inicia a animação de rotação do planeta
    /// </summary>
    public void StartPlanetAnimation()
    {
        rotate = true;
        planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOn");
    }

    public void ShowPlanetName()
    {
        planetNameText.text = planeta.name;
    }
    public void HidePlanetName()
    {
        planetNameText.text = "";
    }

    /// <summary>
    /// Para a animação de rotação do planeta
    /// </summary>
    public void StopPlanetAnimation()
    {
        rotate = false;       
        planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOff");
    }

    /// <summary>
    /// Informa o planetNumber
    /// </summary>
    /// <returns></returns>
    public int GetPlanetNumber()
    {
        return planetNumber;
    }

    /// <summary>
    /// Informa qual a primeira scene que deve ser carregada para o planeta
    /// </summary>
    /// <returns></returns>
    public string GetPlanetLevel()
    {
        return planetFirstScene;
    }
}
