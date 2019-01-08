using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class avatarButtonBlock : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.tag != "AvatarButton" || !eventData.pointerPress.GetComponent<UnityEngine.UI.Button>().interactable)
        {
            GameObject.Find("Confirma").GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
}
