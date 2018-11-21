using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    private Text sideBarTip;
    private Button planeta;
    private string toolTipText;

	void Start () {
        sideBarTip = GameObject.Find("Panel Text").GetComponent<UnityEngine.UI.Text>();
        planeta = this.GetComponent<UnityEngine.UI.Button>();
	}
	
	void Update () {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sideBarTip.text = GetText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sideBarTip.text = "";
    }

    public string GetText()
    {
        switch (planeta.name)
        {
            case "Planeta 1":
                return toolTipText = "Clique para entrar no Planeta 1!";

            case "Planeta 2":
                return toolTipText = "Para acessar o Planeta 2, primeiro passe pelo Planeta 1!";

            case "Planeta 3":
                return toolTipText = "Para acessar o Planeta 3, primeiro passe pelo Planeta 2!";

            case "Planeta 4":
                return toolTipText = "Para acessar o Planeta 4, primeiro passe pelo Planeta 3!";

            default:
                return "";
        }
    }
}
