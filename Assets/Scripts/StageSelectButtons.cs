using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    private Text sideBarTip;
    private Button planeta;
    private string toolTipText;
    private string proximoPlaneta;
    private Animator planetaMouseOverAnimation;
    private Text planetText;
    private int planetNumber;

	void Start () {
        sideBarTip = GameObject.Find("Panel Text").GetComponent<UnityEngine.UI.Text>();
        planeta = this.GetComponent<UnityEngine.UI.Button>();
        planetaMouseOverAnimation = GameObject.Find(planeta.name).GetComponent<Animator>();
        planetText = GetComponentInChildren<Text>();
    }
	
	void Update () {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sideBarTip.text = GetText();
        if (planeta.interactable == true)
        {
            planetText.enabled = true;
            planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOn");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sideBarTip.text = "";
        if (planeta.interactable == true)
        {
            planetText.enabled = false;
            planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOff");
        }
    }

    public string GetText()
    {
        if (planeta.interactable == true)
        {
            proximoPlaneta = "Clique para entrar no " + planeta.name + "!";
            return proximoPlaneta;
        }
        else
        {
            planetNumber = System.Int32.Parse(planeta.name.Substring(planeta.name.Length - 1));            
            return toolTipText = "Para acessar o Planeta " + planetNumber + ", primeiro passe pelo Planeta " + (planetNumber - 1) + "!";
        }
    }
}
