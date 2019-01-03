﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonConfirmar : MonoBehaviour
{

    public static ButtonConfirmar instance = null;
    private Button buttonConfirmaResposta;
    private StageManager stageManager;
    private SoundManager soundManager;
    private Timer timer;
    private Score score;
    private Text[] telaSilabaDigitada;
    private GameObject respostaCertaFeedback;
    private GameObject respostaErradaFeedback;

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
        buttonConfirmaResposta = this.GetComponent<UnityEngine.UI.Button>();
        stageManager = StageManager.instance;
        soundManager = SoundManager.instance;
        timer = Timer.instance;
        score = Score.instance;

        telaSilabaDigitada = stageManager.GetTelaSilabaDigitada();
        respostaCertaFeedback = Resources.Load("Prefabs/RespostaCertaFeedback") as GameObject;
        respostaErradaFeedback = Resources.Load("Prefabs/RespostaErradaFeedback") as GameObject;
    }

    private void Update()
    {
        if (timer.endOfTime) //Verifica se o timer já chegou ao final
        {
            ConfirmaResposta();
        }
    }

    public void ConfirmaResposta()
    {
        LevelController.bloqueiaBotao = true; // Iniciando a verificação da resposta

        for (int i = 0; i < LevelController.NumeroDeSilabasDaPalavra; i++)
        {
            StartCoroutine(VerificaRespostaCertaOuErrada(LevelController.silabasDigitadas[i], LevelController.silabas[i], i, i * 1.5f)); //Para cada silaba, verifica se ela está certa ou errada
        }

        LevelController.BotaoConfirmaResposta = false;//disable button after click

        for (int i = 0; i < LevelController.NumeroDeSilabasDaPalavra; i++)
        {
            LevelController.silabasDigitadas[i] = "";//reset var after confirm button is clicked
        }

        StartCoroutine(score.SetScore(1.5f * LevelController.NumeroDeSilabasDaPalavra)); //Pontua o resultado
        timer.ResetTimeProgressBar(); //reset var para parar timer e barra de tempo
        StartCoroutine(score.CheckScore(1.5f * LevelController.NumeroDeSilabasDaPalavra, stageManager.NextLevel, stageManager.PreviousLevel)); //Verifica se o resultado atual é o suficiente para avançar ou retroceder
    }



    public IEnumerator VerificaRespostaCertaOuErrada(string silabaSelecionada, string silabaDigitada, int BlockIndex, float segundos)
    {

        GameObject respostaFeedbackTemp;

        timer.endOfTime = false;
        yield return new WaitForSeconds(segundos);
        soundManager.StopBackground();

        if (silabaDigitada.Equals(silabaSelecionada))//verifica se o que foi digitado é o mesmo que foi escolhido pelo sistema (falado para o usuário)
        {
            soundManager.StopBackground();
            //Instancia o objeto e o coloca no lugar certo
            respostaFeedbackTemp = Instantiate(respostaCertaFeedback); 
            respostaFeedbackTemp.transform.SetParent(GameObject.Find("Canvas").transform);
            respostaFeedbackTemp.transform.position = telaSilabaDigitada[BlockIndex].transform.position;
            respostaFeedbackTemp.transform.rotation = telaSilabaDigitada[BlockIndex].transform.rotation;             
        }

        else//caso a resposta esteja errada...
        {
            LevelController.AlgumaSilabaErrada = true;

            //Instancia o objeto e o coloca no lugar certo
            respostaFeedbackTemp = Instantiate(respostaErradaFeedback);
            respostaFeedbackTemp.transform.SetParent(GameObject.Find("Canvas").transform);
            respostaFeedbackTemp.transform.position = telaSilabaDigitada[BlockIndex].transform.position;
            if (BlockIndex%2 == 0) //Necessário para rotacionar os sinais de erros pares
            {
                respostaFeedbackTemp.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f)); ;
            }

            else
            {
                respostaFeedbackTemp.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,-10.87f));
            }
            respostaFeedbackTemp.transform.localScale = new Vector3(1, 1, 1);                
        }
    }

    public void ActiveButton()
    {
        buttonConfirmaResposta.interactable = true;
    }

    public void DeactiveButton()
    {
        buttonConfirmaResposta.interactable = false;
    }
}