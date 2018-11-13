using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public static Timer instance = null;
    private SoundManager soundManager;
    private AudioClip tictac;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            tictac = (AudioClip)Resources.Load("Sounds/sfx/timer");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        soundManager = SoundManager.instance;
    }


    /// <summary>
    /// Função que aciona a barra de tempo
    /// </summary>
    /// <param name="silaba"></param>
    /// <returns></returns>
    public IEnumerator SetTimeIsRunning(AudioClip silaba)//função para set a variavel de contar tempo e barra após a fala da palavra
    {
        yield return new WaitForSeconds(silaba.length);
        soundManager.PlayBackground(tictac);
        LevelController.TimeIsRunning = true;
    }
}
