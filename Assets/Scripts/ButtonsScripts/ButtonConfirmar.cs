using System;
using System.Collections;
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
    private SilabaControl silabaControl;
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
    }

    private void Start()
    {
        StopAllCoroutines();
        buttonConfirmaResposta = this.GetComponent<UnityEngine.UI.Button>();
        stageManager = StageManager.instance;
        soundManager = SoundManager.instance;
        silabaControl = SilabaControl.instance;
        timer = Timer.instance;
        score = Score.instance;

        telaSilabaDigitada = stageManager.GetTelaSilabaDigitada();
        respostaCertaFeedback = Resources.Load("Prefabs/Feedback/RespostaCertaFeedback") as GameObject;
        respostaErradaFeedback = Resources.Load("Prefabs/Feedback/RespostaErradaFeedback") as GameObject;
        StartCoroutine(WaitForEndOfTime());
    }

    // Espera o tempo acabar e chama ConfirmaResposta quando isto acontece
    IEnumerator WaitForEndOfTime()
    {
        yield return new WaitUntil(() => timer.endOfTime);
        ConfirmaRespostaButton();
        timer.endOfTime = false;
        yield return new WaitForSeconds(2);
        StartCoroutine(WaitForEndOfTime());
    }

    public void ConfirmaRespostaButton()
    {
        StartCoroutine(ConfirmaResposta());
    }

    public IEnumerator ConfirmaResposta()
    {
        // Pega o tempo utilizado pelo jogador para a última palavra
        DataManager.statisticsData.tempoUtiizado = timer.GetTimeUntilHere();
        // Reseta a palavra escrita
        DataManager.statisticsData.palavraEscrita = "";
        DataManager.statisticsData.dataHora = DateTime.Now;

        score.AddTimePlaying(timer.GetTimeUntilHere()); //Adiciona tempo corrido desde de que o tempo começou até apertar o botão confirma.
        timer.ResetTimeProgressBar();

        LevelController.bloqueiaBotao = true; // Iniciando a verificação da resposta

        // Verifica se havia algum textSlot bloqueado e completa com as letras corretas
        if (stageManager.blockTextSlot)
        {
            silabaControl.CompleteEmptyTextSlots();
        }


        int temp = 0;
        for (int i = 0; i < LevelController.textSlots; i++)
        {
            // Pega a palavra escrita pelo jogador
            DataManager.statisticsData.palavraEscrita += LevelController.inputText[i];

            if (silabaControl.isPlanetLetter[i])
            {
                StartCoroutine(VerificaRespostaCertaOuErrada(LevelController.inputText[i], LevelController.originalText[i], i, temp * 1.5f)); //Para cada silaba, verifica se ela está certa ou errada
                temp++;
            }
        }

        LevelController.BotaoConfirmaResposta = false;//disable button after click
        StartCoroutine(score.SetScore(1.5f * silabaControl.numberOfValidSlots)); //Pontua o resultado
        soundManager.StopSfxLoop();

        yield return new WaitForSeconds(1.5f * silabaControl.numberOfValidSlots); //Espera para começar a tocar a correção
        CorrigeResposta();
        timer.ResetTimeProgressBar(); //reset var para parar timer e barra de tempo
        yield return new WaitForSeconds(silabaControl.TimeSilabaAtual()); //Espera para ver resultado e começar próxima partida

        StartCoroutine(score.CheckScore(silabaControl.numberOfValidSlots, stageManager.NextLevel, stageManager.PreviousLevel)); //Verifica se o resultado atual é o suficiente para avançar ou retroceder
    }

    /// <summary>
    /// Mostra a tela de correção
    /// </summary>
    private void CorrigeResposta()
    {
        silabaControl.CorrigeSlots();
        silabaControl.TocarSilabaAtual();
    }

    public IEnumerator VerificaRespostaCertaOuErrada(string silabaSelecionada, string silabaDigitada, int BlockIndex, float segundos)
    {
        GameObject respostaFeedbackTemp;

        timer.endOfTime = false;
        yield return new WaitForSeconds(segundos);
        //soundManager.StopSfxLoop();
        
        if (silabaDigitada.Equals(silabaSelecionada))//verifica se o que foi digitado é o mesmo que foi escolhido pelo sistema (falado para o usuário)
        {
            soundManager.StopSfxLoop();
            //Instancia o objeto e o coloca no lugar certo
            respostaFeedbackTemp = Instantiate(respostaCertaFeedback, GameObject.Find("Canvas").transform);
            respostaFeedbackTemp.transform.position = telaSilabaDigitada[BlockIndex].transform.position;
            respostaFeedbackTemp.transform.rotation = telaSilabaDigitada[BlockIndex].transform.rotation;  
        }

        else//caso a resposta esteja errada...
        {
            LevelController.AlgumaSilabaErrada = true;

            //Instancia o objeto e o coloca no lugar certo
            respostaFeedbackTemp = Instantiate(respostaErradaFeedback, GameObject.Find("Canvas").transform);
            respostaFeedbackTemp.transform.position = telaSilabaDigitada[BlockIndex].transform.position;
            if (BlockIndex%2 == 0) //Necessário para rotacionar os sinais de erros pares
            {
                respostaFeedbackTemp.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f)); ;
            }

            else
            {
                respostaFeedbackTemp.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,-10.87f));
            }               
        }

        respostaFeedbackTemp.transform.SetSiblingIndex(GameObject.Find("Canvas").transform.childCount - 5);
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
