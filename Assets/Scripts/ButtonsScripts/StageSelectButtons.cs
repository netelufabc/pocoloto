using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    private Text sideBarTip;
    private Button planeta;
    private string toolTipText;
    private Animator planetaMouseOverAnimation;
    private Text planetNameText;
    private int planetNumber;
    private bool rotate;
    
    private bool[] planetList;

	void Start () {
        sideBarTip = GameObject.Find("Panel Text").GetComponent<UnityEngine.UI.Text>();
        planeta = this.GetComponent<UnityEngine.UI.Button>();
        planetaMouseOverAnimation = GameObject.Find(planeta.name).GetComponent<Animator>();
        planetNameText = GameObject.Find("Nome Planeta").GetComponent<UnityEngine.UI.Text>();

        planetList = SaveManager.player.planetaLiberado;
        planetNumber = System.Int32.Parse(planeta.name.Substring(planeta.name.Length - 1));
        if (planetList[planetNumber - 1])
        {
            planeta.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            planeta.GetComponent<UnityEngine.UI.Button>().interactable = false;
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
        sideBarTip.text = GetText();
        if (planeta.interactable == true)
        {
            rotate = true;
            planetNameText.text = planeta.name;
            planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOn");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sideBarTip.text = "";
        if (planeta.interactable == true)
        {
            rotate = false;
            planetNameText.text = "";
            planetaMouseOverAnimation.Play("PlanetaSelecaoMouseOff");
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
            planetNumber = System.Int32.Parse(planeta.name.Substring(planeta.name.Length - 1));            
            return toolTipText = "Para acessar o Planeta " + planetNumber + ", primeiro passe pelo Planeta " + (planetNumber - 1) + "!";
        }
    }
}
