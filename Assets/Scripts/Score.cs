using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score: MonoBehaviour {

    private static Score single_instance = null;
    private int scorePositive;
    private int scoreNegative;
    private Text scorePositiveText;
    private Text scoreNegativeText;
    private int maxScore;

    //Construtores e função para manter o Singleton

    public Score(int scorePositive, int scoreNegative, string scorePositiveText, string scoreNegativeText)
    {
        this.scorePositiveText = GameObject.Find(scorePositiveText).GetComponent<UnityEngine.UI.Text>();
        this.scoreNegativeText = GameObject.Find(scoreNegativeText).GetComponent<UnityEngine.UI.Text>();
        UpdateScorePositive(scorePositive);
        UpdateNegativeScore(scoreNegative);
        maxScore = LevelController.MaxScoreGlobal;
    }

    public Score(string scorePositiveText, string scoreNegativeText)
    {
        this.scorePositiveText = GameObject.Find(scorePositiveText).GetComponent<UnityEngine.UI.Text>();
        this.scoreNegativeText = GameObject.Find(scoreNegativeText).GetComponent<UnityEngine.UI.Text>();
        UpdateScorePositive(0);
        UpdateNegativeScore(0);
        maxScore = LevelController.MaxScoreGlobal;
    }

    public static Score GetInstance(int scorePositive, int scoreNegative, string scorePositiveText, string scoreNegativeText)
    {
        if (single_instance == null)
        {
            single_instance = new Score(scorePositive, scoreNegative, scorePositiveText, scoreNegativeText);
        }

        return single_instance;
    }

    public static Score GetInstance(string scorePositiveText, string scoreNegativeText)
    {
        if (single_instance == null)
        {
            single_instance = new Score(scorePositiveText, scoreNegativeText);
        }

        return single_instance;
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

    //Atualiza o ScorePositive:

    public void UpdateScorePositive(int pontos)
    {
        scorePositive += pontos;
        scorePositiveText.text = scorePositive.ToString();
    }

    //Atualiza o ScoreNegative:

    public void UpdateNegativeScore(int pontos)
    {
        scoreNegative += pontos;
        scoreNegativeText.text = scoreNegative.ToString();
    }

    //Reseta os dois Scores:

    public void ResetScore()
    {
        UpdateScorePositive(0);
        UpdateNegativeScore(0);
    }

    //Espera o tempo para atualizar o Score

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
    
    //Ainda implementando
    /*
    public IEnumerator CheckScore(float seconds, GameObject LevelClearMsg, GameObject GameOver, AudioClip acerto, AudioClip erro, AudioSource audioFile, string NextLevel, string PreviousLevel)
    {
        yield return new WaitForSeconds(seconds + 0.2f);
        if (getScorePositive() == MaxScore)
        {
            StartCoroutine(Blinker.DoBlinksGameObject(acerto, 0, LevelClearMsg, 2f, 0.2f, audioFile, LevelClearMsg));
            StartCoroutine(StageManager.CallAnotherLevel(3, NextLevel));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }
        else if (getScoreNegative() == MaxScore)
        {
            StartCoroutine(Blinker.DoBlinksGameObject(erro, 0, GameOver, 2f, 0.2f, audioFile, LevelClearMsg));
            StartCoroutine(StageManager.CallAnotherLevel(3, PreviousLevel));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }
        else
        {
            StartCoroutine(CallSilaba(0));//chama nova sílaba            
        }
    }
    */
}
