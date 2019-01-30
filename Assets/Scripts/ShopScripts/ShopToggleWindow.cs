using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class ShopToggleWindow : MonoBehaviour, IPointerClickHandler {

    private ToggleGroup toggleGroup;
    private ShopManager shopManager;

    // Use this for initialization
    void Awake () {
        toggleGroup = FindObjectOfType<ToggleGroup>();
    }
	
    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void OnPointerClick(PointerEventData pointerClick)
    {
        string tempToggleName = toggleGroup.ActiveToggles().FirstOrDefault().name;

        tempToggleName = string.Concat(tempToggleName, "Selected");

        Invoke(tempToggleName, 0);
    }

    private void AvatarToggleSelected()
    {
        shopManager.AvatarToggleShopWindow();
    }

    private void CoresToggleSelected()
    {
        shopManager.CoresToggleShopWindow();
    }

    private void ExtrasToggleSelected()
    {
        shopManager.ExtrasToggleShopWindow();
    }

    public string SelectedToggle()
    {
        return toggleGroup.ActiveToggles().FirstOrDefault().name;
    }
}
