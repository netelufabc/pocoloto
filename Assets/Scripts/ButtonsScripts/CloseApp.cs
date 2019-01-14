using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : LoadScene {

    GameObject optionsMenu;

    private void Awake()
    {
        optionsMenu = Resources.Load("Prefabs/CloseMenu") as GameObject;
    }

    private void Start()
    {
        FocusButton();
        if (GameObject.Find("Menu Principal") != null)
        {
            GameObject.Find("Menu Principal").GetComponent<UnityEngine.UI.Button>().Select();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            OpenMenu();
            PauseGame();          
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
       // optionsMenu.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
       // optionsMenu.SetActive(false);
        //enable the scripts again
    }

    void FocusButton()
    {
        if (GameObject.Find("Botao Novo Jogo") != null)
        {
            GameObject.Find("Botao Novo Jogo").GetComponent<UnityEngine.UI.Button>().Select();
        }
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
        FocusButton();
    }
}
