using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {
    public static Timer instance = null;
    private SoundManager soundManager;
    private AudioClip tictac;

    private float ProgressBarTime;
    private float TimeProgressBarSpeed = 0.5f;
    private Image TimeProgressBar;
    public bool endOfTime = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            tictac = (AudioClip)Resources.Load("Sounds/sfx/timer");
            TimeProgressBar = GameObject.Find("Progress Time Bar").GetComponent<UnityEngine.UI.Image>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        soundManager = SoundManager.instance;
        TimeProgressBar.fillAmount = 0;
    }
    
    private void Update()
    {
        if (!LevelController.TimePause) //Serve para pausar o timer quando abre o menu
        {
            if (LevelController.TimeIsRunning)//bloco da barra de tempo inicio
            {
                if (ProgressBarTime < 10)
                {
                    ProgressBarTime += TimeProgressBarSpeed * Time.deltaTime;
                    TimeProgressBar.fillAmount = ProgressBarTime / 10;
                }
                else
                {
                    endOfTime = true;
                }
            }
        }
    }
    
    public void ResetTimeProgressBar()
    {
        ProgressBarTime = 0;
        TimeProgressBar.fillAmount = 0;
        LevelController.TimeIsRunning = false;
    }

    /// <summary>
    /// Função que aciona a barra de tempo
    /// </summary>
    /// <param name="silaba"></param>
    /// <returns></returns>
    public IEnumerator SetTimeIsRunning(AudioClip silaba)//função para set a variavel de contar tempo e barra após a fala da palavra
    {
        yield return new WaitForSeconds(silaba.length);
        ResetTimeProgressBar();
        LevelController.TimeIsRunning = true;
        soundManager.PlayBackground(tictac);
    }
}
