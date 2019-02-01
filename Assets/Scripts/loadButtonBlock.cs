using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Utilizando eventos - IPointerClickHandler - para verificar ocorrência de clicks do mouse na tela
public class loadButtonBlock : MonoBehaviour, IPointerClickHandler {

    /// <summary>
    /// Bloqueia o acesso ao botão confirma e deleta quando clicar em qualquer outro local da tela
    /// Chama a função sempre que ocorrer um click no objeto cujo script está
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("Confirma").GetComponent<UnityEngine.UI.Button>().interactable = false;
        GameObject.Find("Delete").GetComponent<UnityEngine.UI.Button>().interactable = false;
        foreach (Selectable button in Selectable.allSelectables)
        {
            if (button.CompareTag("Button"))
            {
                button.colors = ToggleButton.defaultColor;
            }
        }
    }
}
