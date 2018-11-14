using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonDicaAudio : MonoBehaviour {

    public static ButtonDicaAudio instance = null;
    private Button botaoDicaAudio;
    private SilabaControl silabaControl;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        silabaControl = SilabaControl.instance;
        botaoDicaAudio = GameObject.FindGameObjectWithTag("Button Sound").GetComponent<UnityEngine.UI.Button>();
    }

    private void DesactiveButton()
    {
        botaoDicaAudio.interactable = false;
    }

    public void ActiveButton()
    {
        botaoDicaAudio.interactable = true;
    }

    public void AcionaDicaAudio()//botao dica audio
    {
        silabaControl.TocarSilabaAtual();//toca silaba atual
        DesactiveButton();
    }
}
