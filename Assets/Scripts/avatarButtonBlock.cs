using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class avatarButtonBlock : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("Confirma").GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}
