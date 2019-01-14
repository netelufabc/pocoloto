using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : LoadScene {

    GameObject optionsMenu;

    private void Awake()
    {
        optionsMenu = Resources.Load("Prefabs/CloseMenu") as GameObject;
    }

    public void FecharAplicacao()
    {
        //Application.LoadLevel(sceneName);
        Application.Quit();
    }

    public void OpenMenu()
    {
        if (GameObject.Find("CloseMenu(Clone)") == null)
        {
            soundManager.StopBackground();
            GameObject newOptionsMenu;
            newOptionsMenu = Instantiate(optionsMenu);
            newOptionsMenu.transform.SetParent(GameObject.Find("Canvas").transform, false);
            LevelController.TimePause = true;
        }
    }

    /// <summary>
    /// Utilizado para destruir o pai do botão de fechar
    /// </summary>
    public void CloseMenu()
    {
        
        soundManager.PlayBackground();
        GameObject buttonParent;
        buttonParent = this.transform.parent.gameObject;
        LevelController.TimePause = false;
        Destroy(buttonParent);
    }
}
