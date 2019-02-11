using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Utilizando eventos - IPointerClickHandler - para verificar ocorrência de clicks do mouse na tela
public class avatarButtonBlock : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Bloqueia o acesso ao botão confirma quando clicar em qualquer outro local da tela
    /// Chama a função sempre que ocorrer um click no objeto cujo script está
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.tag != "AvatarButton" || !eventData.pointerPress.GetComponent<UnityEngine.UI.Button>().interactable)
        {

            GameObject.Find("Confirma").GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
}
