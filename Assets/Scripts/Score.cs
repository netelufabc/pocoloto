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
    private int maxScore;
    private Animator estrelaPositivo;
    private Animator estrelaNegativo;

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
            resultado = Resources.Load("Prefabs/Feedback/Level Clear Message") as GameObject;

            resultado = Instantiate(resultado, GameObject.Find("Canvas").transform);

            if (stageManager.NextLevel.Equals("06_stageSelect"))
            {
                SaveManager.player.planetaLiberado[stageManager.currentLevel] = true;
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
}
