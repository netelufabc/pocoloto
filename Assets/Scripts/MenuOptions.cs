using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    //GameObject menuOptions;
    SoundManager soundManager;
    public AudioClip background;
    //Button botaoMusica;
    Text textoBotaoMusica;

    private void Awake()
    {
        //botaoMusica = GameObject.Find("Musica").GetComponent<UnityEngine.UI.Button>();
        textoBotaoMusica = GameObject.Find("Musica").GetComponent<UnityEngine.UI.Button>().GetComponentInChildren<UnityEngine.UI.Text>();
        soundManager = SoundManager.instance;
        //menuOptions = Resources.Load("Prefabs/MenuOptions") as GameObject;
    }

    public void Start()
    {
        if (soundManager.IsBackgroundPlaying())
        {
            textoBotaoMusica.text = "Música: Ligado";
        }
        else
        {
            textoBotaoMusica.text = "Música: Desligado";
        }
    }

    public void MuteBackground()
    {
        if (soundManager.IsBackgroundPlaying())
        {
            soundManager.SetMusicOff();
            textoBotaoMusica.text = "Música: Desligado";
        }
        else
        {
            soundManager.SetMusicON(background);
            textoBotaoMusica.text = "Música: Ligado";
        }
    }

    //public void OpenMenu()
    //{
    //    if (GameObject.Find("MenuOptions(Clone)") == null)
    //    {
    //        GameObject newOptionsMenu;
    //        newOptionsMenu = Instantiate(menuOptions);
    //        newOptionsMenu.transform.SetParent(GameObject.Find("Canvas").transform, false);
    //    }
    //}

    /// <summary>
    /// Utilizado para destruir o pai do botão de fechar
    /// </summary>
    //public void CloseMenu()
    //{
    //    GameObject buttonParent;
    //    buttonParent = this.transform.parent.gameObject;
    //    Destroy(buttonParent);
    //}
}
