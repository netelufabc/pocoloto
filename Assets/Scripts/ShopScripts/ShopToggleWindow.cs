using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class ShopToggleWindow : MonoBehaviour, IPointerClickHandler {

    private ToggleGroup toggleGroup;

    // Use this for initialization
    void Start () {
        toggleGroup = FindObjectOfType<ToggleGroup>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData pointerClick)
    {
        string tempToggleName = toggleGroup.ActiveToggles().FirstOrDefault().name;

        tempToggleName = string.Concat(tempToggleName, "Selected");

        Invoke(tempToggleName, 0);
    }

    private void AvatarToggleSelected()
    {
        Debug.Log("Avatar");
    }

    private void CoresToggleSelected()
    {
        Debug.Log("Cores");
    }

    private void ExtrasToggleSelected()
    {
        Debug.Log("Extras");
    }
}
