using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOrStopMusic : MonoBehaviour {

    private SoundManager soundManager;
    [Tooltip("Qual é a música que vai tocar?")]
    public AudioClip song; //Qual música você quer que toque ao iniciar a tela?
    [Tooltip("Só quer parar a música de fundo?")]
    public bool Stop;


    void Start()
    {
        soundManager = SoundManager.instance;
        if (Stop)
        {
            soundManager.StopBackground();
            soundManager.StopSfxLoop();
        }
        else
        {
            soundManager.PlayBackground(song);
        }
    }
}
