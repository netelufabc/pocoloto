using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : LoadScene
{

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
        }
    }

    void FocusButton()
    {
        if (GameObject.Find("Botao Novo Jogo") != null)
        {
            GameObject.Find("Botao Novo Jogo").GetComponent<UnityEngine.UI.Button>().Select();
        }
    }


    //public void FecharAplicacao()
    //{
    //    //Application.LoadLevel(sceneName);
    //    Application.Quit();
    //}

    public void OpenMenu()
    {
        if (GameObject.Find("CloseMenu(Clone)") == null)
        {
            GameObject newOptionsMenu;
            newOptionsMenu = Instantiate(optionsMenu);
            newOptionsMenu.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
    }

    /// <summary>
    /// Utilizado para destruir o pai do botão de fechar
    /// </summary>
    //public void CloseMenu()
    //{
    //    GameObject buttonParent;
    //    buttonParent = this.transform.parent.gameObject;
    //    Destroy(buttonParent);
    //    FocusButton();
    //}
}
