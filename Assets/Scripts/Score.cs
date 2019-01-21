﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score: MonoBehaviour {

    public static Score instance = null;
    private int scorePositive;
    private int scoreNegative;
    private string scorePositiveString = "Score Positive";
    private string scoreNegativeString = "Score Negative";
    private Text scorePositiveText;
    private Text scoreNegativeText;
    private Animator estrelaPositivo;
    private Animator estrelaNegativo;
    private int maxScore; //Número de acertos ou erros necessário para passar de fase

    SilabaControl silabaControl;
    StageManager stageManager;


    //Construtores e função para manter o Singleton

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
        maxScore = LevelController.MaxScoreGlobal;
    }

    /// <summary>
    /// Encontra os componentes de texto e reseta o score   
    /// </summary>
    public void ScoreSetup()
    {
            this.scorePositiveText = GameObject.Find(scorePositiveString).GetComponent<UnityEngine.UI.Text>();
            this.scoreNegativeText = GameObject.Find(scoreNegativeString).GetComponent<UnityEngine.UI.Text>();
            ResetScore();
    }

    //Funções:

    public int getScorePositive()
    {
        return scorePositive;
    }

    public int getScoreNegative()
    {
        return scoreNegative;
    }

    /// <summary>
    /// Soma ao Score Positive o número de pontos colocado.
    /// </summary>
    /// <param name="pontos"></param>
    public void UpdateScorePositive(int pontos)
    {
        scorePositive += pontos;
        scorePositiveText.text = scorePositive.ToString();

        estrelaPositivo = GameObject.Find("icone_pontuacao_acerto").GetComponent<Animator>();
        estrelaPositivo.Play("PontoPositivo");
    }

    /// <summary>
    /// Soma ao Score Negative o número de pontos colocado.
    /// </summary>
    /// <param name="pontos"></param>
    public void UpdateNegativeScore(int pontos)
    {
        scoreNegative += pontos;
        scoreNegativeText.text = scoreNegative.ToString();

        estrelaNegativo = GameObject.Find("icone_pontuacao_erro").GetComponent<Animator>();
        estrelaNegativo.Play("PontoNegativo");
    }

    /// <summary>
    /// Deixa em "0" tanto o Score Positive quanto o Score Negative
    /// </summary>
    public void ResetScore()
    {
        scoreNegative = 0;
        scoreNegativeText.text = scoreNegative.ToString();
        scorePositive = 0;
        scorePositiveText.text = scorePositive.ToString();
    }

    /// <summary>
    /// Função que é chamada para atualizar os Score
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public IEnumerator SetScore(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (LevelController.AlgumaSilabaErrada)
        {
            UpdateNegativeScore(1);
        }
        else
        {
            UpdateScorePositive(1);
        }
        LevelController.AlgumaSilabaErrada = false;
    }
    
    /// <summary>
    /// Verifica se o jogador já está pronto para ir para o próximo nível ou se voltará para o nível anterior
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="NextLevel"></param>
    /// <param name="PreviousLevel"></param>
    /// <returns></returns>
    public IEnumerator CheckScore(float seconds, string NextLevel, string PreviousLevel)
    {
        GameObject resultado;

        yield return new WaitForSeconds(seconds + 0.2f);
        stageManager = StageManager.instance; //Pega a atual instância do Stage Manager

        //Se o resultado estiver correto
        if (getScorePositive() == maxScore)
        {
            GiveStars();
            ResetTimePlaying();

            resultado = Resources.Load("Prefabs/Feedback/Level Clear Message") as GameObject;

            resultado = Instantiate(resultado, GameObject.Find("Canvas").transform);

            // Se ganhar uma fase de revisão e for o último ato (caso tenha mais que um), libera o próximo sistema
            if (stageManager.NextLevel.Contains("stageSelect") && stageManager.eRevisao)
            {
                SaveManager.player.sistemaLiberado[stageManager.currentLevel] = true;
            }

            SaveManager.Save();
            yield return new WaitForSeconds(4);
            StartCoroutine(stageManager.CallAnotherLevel(3, NextLevel, true));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }

        //Caso o resultado esteja errado
        else if (getScoreNegative() == maxScore)
        {
            if (stageManager.currentAct == 1)
            {
                resultado = Resources.Load("Prefabs/Feedback/Game Over") as GameObject;
            }
            else
            {
                resultado = Resources.Load("Prefabs/Feedback/Level Failed Message") as GameObject;
            }

            resultado = Instantiate(resultado, GameObject.Find("Canvas").transform);

            SaveManager.Save();
            yield return new WaitForSeconds(4);
            StartCoroutine(stageManager.CallAnotherLevel(3, PreviousLevel, false));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }
        else
        {
            // Limpa os TextSlots para a proxima palavra
            for (int i = 0; i < LevelController.textSlots; i++)
            {
                LevelController.inputText[i] = "";//reset var after confirm button is clicked
            }

            StartCoroutine(silabaControl.CallSilaba(0));//chama nova sílaba            
        }
    }

    #region FunçõesEstrelas

    private float timePlaying = 0; //Quanto tempo total até o ponto X que o jogador gastou
    private int numberErrors; //Quantos erros foram feitos pelo jogador.
    private Timer timer;

    [Tooltip("Se gastou mais que essa porcentagem de tempo, ganha uma estrela")]
    public float timeLimit1 = 60; //Se gastou mais do que essa porcentagem
    [Tooltip("Se gastou mais que essa porcentagem de tempo, ganha duas estrela")]
    public float timeLimit2 = 20; //Se gastou mais do que essa porcentagem
    [Tooltip("Se errou mais que esse número de perguntas, ganha uma estrela")]
    public int errorStar1 = 10; //Se tem mais que 10 erros, ganha uma estrela
    [Tooltip("Se errou mais que esse número de perguntas, ganha duas estrela")]
    public int errorStar2 = 2; //Se tem mais que 2 erros, ganha duas estrelas

    public void AddTimePlaying(float time)
    {
        timePlaying += time;
    }

    public void ResetTimePlaying()
    {
        timePlaying = 0;
    }

    /// <summary>
    /// Ele verifica o tempo e a quantidade erros do jogador, o fator limitante que será dado como resultado
    /// </summary>
    private void GiveStars()
    {
        int stars = Mathf.Min(CheckTime(), CheckError());

        if (stars > SaveManager.player.planeta[stageManager.currentLevel - 1].ato[stageManager.currentAct-1])
        {
            SaveManager.player.planeta[stageManager.currentLevel - 1].ato[stageManager.currentAct-1] = stars;
        }
    }

    /// <summary>
    /// Verifica o tempo jogado pelo jogador e o tempo total que ele poderia gastar, a porcentagem disso é verificada para saber quão rápido o jogador foi
    /// </summary>
    /// <returns>Um inteiro com o número de estrelas que ele ganharia só dependendo do tempo</returns>
    private int CheckTime()
    {
        timer = Timer.instance;
        float totalTime = (scoreNegative + scorePositive) * timer.totalTime; //Esse é o 100% do tempo gasto do jogador;
        float myTimePorCent = (timePlaying * 100) / totalTime;

        if (timeLimit1 < myTimePorCent)
        {
            return 1;
        }
        else if (timeLimit2 < myTimePorCent)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    /// <summary>
    /// Checa a quantidade de erros e compara eles com os critérios estabelecidos
    /// </summary>
    /// <returns>Um inteiro com o número de estrelas que ele ganharia só dependendo da quantidade de erros</returns>
    private int CheckError()
    {

        if (numberErrors > errorStar1)
        {
            return 1;
        }
        
        else if (numberErrors > errorStar2)
        {
            return 2;
        }

        else
        {
            return 3;
        }
    }
    #endregion
}
