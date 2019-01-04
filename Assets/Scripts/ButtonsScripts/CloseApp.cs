using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : LoadScene {

    GameObject optionsMenu;

    private void Awake()
    {
        optionsMenu = Resources.Load("Prefabs/OptionsMenu") as GameObject;
    }

    public void FecharAplicacao()
    {
        //Application.LoadLevel(sceneName);
        Application.Quit();
    }

    public void OpenMenu()
    {
        if (GameObject.Find("OptionsMenu(Clone)") == null)
        {
            GameObject newOptionsMenu;
            newOptionsMenu = Instantiate(optionsMenu);
            newOptionsMenu.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
    }

    /// <summary>
    /// Utilizado para destruir o pai do botão de fechar
    /// </summary>
    public void CloseMenu()
    {
        GameObject buttonParent;
        buttonParent = this.transform.parent.gameObject;
        Destroy(buttonParent);
    }
}
