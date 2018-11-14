using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonConfirmar : MonoBehaviour {
/*
    private SoundManager soundManager;
    private Score score;
    private AudioClip acerto;
    private AudioClip erro;
    private GameObject LevelClearMsg;//gameobject da imagem de proximo nivel ou nivel anterior
    private GameObject GameOver;//gameobject da imagem de gameover
    private Image[] RespostaCerta;//imagem quando acerta a resposta
    private Image[] RespostaErrada;//imagem quando erra a resposta
    private string NextLevel = StageManager.NextLevel;
    private string PreviousLevel = StageManager.PreviousLevel;

    Blinker blinker;

    private void Awake()
    {
        RespostaCerta = new Image[StageManager.NumeroDeSilabasDaPalavra];
        for (int i = 0; i < StageManager.NumeroDeSilabasDaPalavra; i++)
        {
            RespostaCerta[i] = GameObject.Find(string.Concat("RespostaCerta", i.ToString())).GetComponent<UnityEngine.UI.Image>();
        }

        RespostaErrada = new Image[StageManager.NumeroDeSilabasDaPalavra];
        for (int i = 0; i < StageManager.NumeroDeSilabasDaPalavra; i++)
        {
            RespostaErrada[i] = GameObject.Find(string.Concat("RespostaErrada", i.ToString())).GetComponent<UnityEngine.UI.Image>();
        }

        LevelController.CharLimitForLevel = StageManager.CharLimitForThisLevel;//define limite de caracteres para o nível atual
        for (int i = 0; i < LevelController.NumeroDeSilabasDaPalavra; i++)//inicializa imagens de resposta certa e errada para que não apareça a princípio
        {
            RespostaCerta[i].enabled = false;
            RespostaErrada[i].enabled = false;
        }

        LevelClearMsg = GameObject.Find("Level Clear");
        GameOver = GameObject.Find("Level Failed");
        erro = (AudioClip)Resources.Load("Sounds/sfx/erro_slot01");
        acerto = (AudioClip)Resources.Load("Sounds/sfx/acerto_slot01");
        LevelClearMsg.SetActive(false);
        GameOver.SetActive(false);
    }

    private void Start()
    {
        soundManager = SoundManager.instance;
        score = Score.instance;
        blinker = Blinker.instance;

        for (int i = 0; i < LevelController.NumeroDeSilabasDaPalavra; i++)//inicializa imagens de resposta certa e errada para que não apareça a princípio
        {
            RespostaCerta[i].enabled = false;
            RespostaErrada[i].enabled = false;
        }
    }

    public void ConfirmaResposta()
    {
        for (int i = 0; i < LevelController.NumeroDeSilabasDaPalavra; i++)
        {
            StartCoroutine(VerificaRespostaCertaOuErrada(LevelController.silabasDigitadas[i], LevelController.silabas[i], i, i * 1.5f));
        }

        LevelController.BotaoConfirmaResposta = false;//disable button after click

        for (int i = 0; i < LevelController.NumeroDeSilabasDaPalavra; i++)
        {
            LevelController.silabasDigitadas[i] = "";//reset var after confirm button is clicked
        }

        StartCoroutine(score.SetScore(1.5f * LevelController.NumeroDeSilabasDaPalavra));

        LevelController.TimeIsRunning = false;//reset var para parar timer e barra de tempo
        //TimeProgressBar.fillAmount = 0;//reset barra de tempo para começar vazia
        //ProgressBarTime = 0;//reset timer


        StartCoroutine(score.CheckScore(1.5f * LevelController.NumeroDeSilabasDaPalavra, LevelClearMsg, GameOver, acerto, erro, NextLevel, PreviousLevel));
        //BotaoDicaVisual.interactable = true;//ativa botoes de ajuda após tocar nova silaba Colocar no som
        //BotaoDicaAudio.interactable = true;//ativa botoes de ajuda após tocar nova silaba Colocar no som
    }

    public IEnumerator VerificaRespostaCertaOuErrada(string silabaSelecionada, string silabaDigitada, int BlockIndex, float segundos)
    {
        if (silabaDigitada.Equals(silabaSelecionada))//verifica se o que foi digitado é o mesmo que foi escolhido pelo sistema (falado para o usuário)
        {
            yield return new WaitForSeconds(segundos);
            soundManager.StopBackground();
            soundManager.PlaySfx(acerto);//toca som de acerto
            StartCoroutine(blinker.DoBlinks(RespostaCerta[BlockIndex], 1f, 0.2f, RespostaCerta, RespostaErrada));//pisca estrelas de acerto                  
        }
        else//caso a resposta esteja errada...
        {
            LevelController.AlgumaSilabaErrada = true;
            yield return new WaitForSeconds(segundos);
            soundManager.StopBackground();
            soundManager.PlaySfx(erro); //toca som de erro
            StartCoroutine(blinker.DoBlinks(RespostaErrada[BlockIndex], 1f, 0.2f, RespostaCerta, RespostaErrada));//pisca estrelas de acerto                   
        }
    }*/

}
