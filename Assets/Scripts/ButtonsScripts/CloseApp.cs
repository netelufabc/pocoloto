using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CloseApp : LoadScene {

    GameObject optionsMenu;

    private void Awake()
    {
        optionsMenu = Resources.Load("Prefabs/Sub Menus/CloseMenu") as GameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (GameObject.FindGameObjectWithTag("PauseMenu") == null)
            {
                OpenMenu();
            }
            else if(this.name == "Exit")
            {                
                CloseMenu();                                
            }
        }

        if (LevelController.ButtonFecharBloqueado)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.GetComponent<Button>().interactable = true;
        }
    }

    public void FecharAplicacao()
    {
        Application.Quit();
    }

    /// <summary>
    /// Verifica se já há um menu aberto, caso não, instância um novo e aciona o PauseGame
    /// </summary>
    public void OpenMenu()
    {
        if (GameObject.Find("CloseMenu(Clone)") == null)
        {
            PauseGame();
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
        ContinueGame();
        GameObject buttonParent;
        buttonParent = this.transform.parent.gameObject;
        Destroy(buttonParent);
    }

    /// <summary>
    /// Muta o tictac e pausa o contador de tempo.
    /// </summary>
    private void PauseGame()
    {        
        soundManager.MuteSfxLoop();
        LevelController.TimePause = true;
    }

    /// <summary>
    /// Desmuta o titac e faz o contador rodar de onde parou
    /// </summary>
    private void ContinueGame()
    {
        soundManager.UnmuteSfxLoop();
        LevelController.TimePause = false;
    }

    public void CloseMenuNBlocker()
    {
        ContinueGame();
        GameObject buttonGrandParent;
        buttonGrandParent = this.transform.parent.transform.parent.gameObject;
        Destroy(buttonGrandParent);
    }
}
