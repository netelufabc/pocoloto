using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{

    //private Text sideBarTip;
    private Button planeta;
    private string toolTipText;
    private Animator planetaMouseOverAnimation;
    private Text planetNameText;
    private int planetNumber;
    private bool rotate;

    private PanelProgressController panelProgressController;
    public string planetLevel;

	void Start () {
        //sideBarTip = GameObject.Find("Panel Text").GetComponent<UnityEngine.UI.Text>();
        planeta = this.GetComponent<UnityEngine.UI.Button>();
        planetaMouseOverAnimation = GameObject.Find(planeta.name).GetComponent<Animator>();
        planetNameText = GameObject.Find("Nome Planeta").GetComponent<UnityEngine.UI.Text>();

        planetNumber = System.Int32.Parse(planeta.name.Substring(planeta.name.Length - 1));

        panelProgressController = PanelProgressController.instance;
    }
	
	void Update () {
        if (rotate)
        {
            transform.Rotate(new Vector3(0, 0, -5));
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //sideBarTip.text = GetText();
        panelProgressController.UpdateInfoText(GetText());
        if (planeta.interactable == true)
        {
            StartPlanetAnimation();
            //rotate = true;
            //planetNameText.text = planeta.name;
            //planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOn");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Se o planeta não é o escolhido, para a nimação
        if (!panelProgressController.IsPlanetChosen(planetNumber))
        {
            //sideBarTip.text = "";
            panelProgressController.RestoreInfoText();
            if (planeta.interactable == true)
            {
                StopPlanetAnimation();
                //rotate = false;
                //planetNameText.text = "";
                //planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOff");
            }
        }
    }
    
    public string GetText()
    {
        string tempText = "";
        //tempText = panelProgressController.ActInfo(planetNumber);

        if (planeta.interactable == true)
        {
            return tempText + "Clique para entrar no " + planeta.name + "!";
        }
        else
        {
            planetNumber = System.Int32.Parse(planeta.name.Substring(planeta.name.Length - 1));            
            return tempText + "Para acessar o Planeta " + planetNumber + ", primeiro passe pelo Planeta " + (planetNumber - 1) + "!";
        }
    }

    /// <summary>
    /// Se o planeta foi selecionado (click), marca como selecionado e permite entrar na fase
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick (PointerEventData eventData)
    {
        //sideBarTip.text = GetText();
        string tempText = GetText();
        panelProgressController.UpdateInfoText(tempText);
        panelProgressController.ReplaceInfoText(tempText);
        panelProgressController.ChoosePlanet(planetNumber, eventData);
        if (planeta.interactable == true)
        {
            StartPlanetAnimation();
            //rotate = true;
            //planetNameText.text = planeta.name;
            //planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOn");
        }
        panelProgressController.ReadyToGo();
    }

    /// <summary>
    /// Inicia a animação de rotação do planeta
    /// </summary>
    public void StartPlanetAnimation()
    {
        rotate = true;
        planetNameText.text = planeta.name;
        planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOn");
    }

    /// <summary>
    /// Para a animação de rotação do planeta
    /// </summary>
    public void StopPlanetAnimation()
    {
        rotate = false;
        planetNameText.text = "";
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

    public string GetPlanetLevel()
    {
        return planetLevel;
    }
}
