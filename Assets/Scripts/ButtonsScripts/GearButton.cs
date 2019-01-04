using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearButton : MonoBehaviour {

    GameObject optionsMenu;

    private void Awake()
    {
        optionsMenu = Resources.Load("Prefabs/OptionsMenu") as GameObject;
    }

    
    /// <summary>
    /// Verifica pop-up do menu do botão de engrenagem já está na scene, caso não esteja, ele instância a prefab
    /// </summary>
    public void OpenMenu()
    {
        if (GameObject.Find("OptionsMenu(Clone)") == null)
        {
            GameObject newOptionsMenu;
            newOptionsMenu = Instantiate(optionsMenu);
            newOptionsMenu.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
    }
}
