﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{
    
    private Button planeta;
    private string toolTipText;
    private Animator planetaMouseOverAnimation;
    private Text planetNameText;
    private int planetNumber;
    private bool rotate;

    private PanelProgressController panelProgressController;
    public string planetFirstScene;

    public bool goToPlanet;

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
            if (planeta.interactable == false)
            {
                panelProgressController.UpdateInfoText(GetText());
            }
            else
            {
                //Debug.Log(planetNumber);
                panelProgressController.DestroyStars();
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
            return "Clique para entrar no " + planeta.name + "!";
        }
        else
        {
            //planetNumber = System.Int32.Parse(planeta.name.Substring(7));            
            return "Para acessar o Planeta " + planetNumber + ", primeiro passe pelo Planeta " + (planetNumber - 1) + "!";
        }
    }

    /// <summary>
    /// Se o planeta foi selecionado (click), marca como selecionado e permite entrar na fase
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick (PointerEventData eventData)
    {
        if (goToPlanet)
        {
            panelProgressController.GoToPlanet();
        }
        else
        {
            string tempText = GetText();
            panelProgressController.UpdateInfoText(tempText, planetNumber);
            panelProgressController.ReplaceInfoText(tempText);
            panelProgressController.ChoosePlanet(planetNumber, eventData);
            panelProgressController.ReadyToGo();
        }
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

    /// <summary>
    /// Informa qual a primeira scene que deve ser carregada para o planeta
    /// </summary>
    /// <returns></returns>
    public string GetPlanetLevel()
    {
        return planetFirstScene;
    }
}
