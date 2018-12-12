using System.Collections;
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

    SilabaControl silabaControl;
    Blinker blinker;

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
        blinker = Blinker.instance;
        maxScore = LevelController.MaxScoreGlobal;
    }

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
    }

    /// <summary>
    /// Soma ao Score Negative o número de pontos colocado.
    /// </summary>
    /// <param name="pontos"></param>
    public void UpdateNegativeScore(int pontos)
    {
        scoreNegative += pontos;
        scoreNegativeText.text = scoreNegative.ToString();
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
    /// <param name="LevelClearMsg"></param>
    /// <param name="GameOver"></param>
    /// <param name="acerto"></param>
    /// <param name="erro"></param>
    /// <param name="audioFile"></param>
    /// <param name="NextLevel"></param>
    /// <param name="PreviousLevel"></param>
    /// <returns></returns>
    public IEnumerator CheckScore(float seconds, GameObject LevelClearMsg, GameObject GameOver, AudioClip acerto, AudioClip erro, string NextLevel, string PreviousLevel)
    {
        GameObject resultado;

        yield return new WaitForSeconds(seconds + 0.2f);
        if (getScorePositive() == maxScore)
        {
            resultado = Resources.Load("Prefabs/Level Clear Message") as GameObject; 
            resultado = Instantiate(resultado);
            resultado.transform.SetParent(GameObject.Find("Canvas").transform);
            yield return new WaitForSeconds(seconds);
            StartCoroutine(StageManager.CallAnotherLevel(3, NextLevel));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }
        else if (getScoreNegative() == maxScore)
        {
            resultado = Resources.Load("Prefabs/Level Failed Message") as GameObject;
            resultado = Instantiate(resultado);
            resultado.transform.SetParent(GameObject.Find("Canvas").transform);
            resultado.transform.position = new Vector3(1, -1, -20);
            yield return new WaitForSeconds(seconds);
            StartCoroutine(StageManager.CallAnotherLevel(3, PreviousLevel));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }
        else
        {
            StartCoroutine(silabaControl.CallSilaba(0));//chama nova sílaba            
        }
    }
}
