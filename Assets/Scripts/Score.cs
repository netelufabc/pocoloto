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
    private Text moneyText;
    private Animator estrelaPositivo;
    private Animator estrelaNegativo;
    private int maxScore; //Número de acertos ou erros necessário para passar de fase

    SilabaControl silabaControl;
    StageManager stageManager;

    private float timePlaying = 0; //Quanto tempo total até o ponto X que o jogador gastou
    private int numberErrors; //Quantos erros foram feitos pelo jogador.
    private Timer timer;
    private int stars; //Numero de estrelas que irão ser ganhas

    [Tooltip("Se gastou mais que essa porcentagem de tempo, ganha uma estrela")]
    public float timeLimit1 = 60; //Se gastou mais do que essa porcentagem
    [Tooltip("Se gastou mais que essa porcentagem de tempo, ganha duas estrela")]
    public float timeLimit2 = 20; //Se gastou mais do que essa porcentagem
    [Tooltip("Se errou mais que esse número de perguntas, ganha uma estrela")]
    public int errorStar1 = 10; //Se tem mais que 10 erros, ganha uma estrela
    [Tooltip("Se errou mais que esse número de perguntas, ganha duas estrela")]
    public int errorStar2 = 2; //Se tem mais que 2 erros, ganha duas estrelas

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
        this.moneyText = GameObject.Find("Money").GetComponent<Text>();
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
            GiveMoney();
            GiveStars();
            ResetTimePlaying();

            resultado = Resources.Load("Prefabs/Feedback/Level Clear Message") as GameObject;

            resultado = Instantiate(resultado, GameObject.Find("Canvas").transform);

            // Se for o último ato do planeta, libera o próximo planeta.
            if (stageManager.currentAct == 3)
            {
                SaveManager.player.planeta[stageManager.currentPlanet].liberado = true;
            }
            // Caso seja a revisão verifica se é do sistema 0
            if (stageManager.eRevisao)
            {
                // Se for do sistema 0, libera todos os outros
                if (!SaveManager.player.sistemaLiberado)
                {
                    SaveManager.player.sistemaLiberado = true;
                }
                // Se não for o 0, verifica se concluiu todos os sistemas pela primeira vez
                else if (SaveManager.player.CompletouTodosSistemas() && !SaveManager.player.jaConcluiuJogo)
                {
                    SaveManager.player.jaConcluiuJogo = true;
                    Debug.Log("Parabéns!");
                    //NextLevel = nome da scene de conclusão;
                }
            }


            //// Se ganhar uma fase de revisão e for o último ato (caso tenha mais que um), libera o próximo sistema
            //if (stageManager.NextLevel.Contains("stageSelect") && stageManager.eRevisao)
            //{
            //    // Pega o inteiro no final do stageSelect (indica qual sistema está) e libera o proximo sistema (+1)
            //    SaveManager.player.sistemaLiberado[System.Int32.Parse(stageManager.NextLevel.Substring(stageManager.NextLevel.Length - 1)) + 1] = true;
            //}

            SaveManager.Save();
            yield return new WaitForSeconds(2);

            GameObject feedbackEstrelas;

            if (stars == 1)
            {
                feedbackEstrelas = Resources.Load("Prefabs/Feedback/GanhaUmaEstrela") as GameObject;
                Instantiate(feedbackEstrelas, GameObject.Find("Canvas").transform);
                yield return new WaitForSeconds(1);
            }
            else if (stars == 2)
            {
                feedbackEstrelas = Resources.Load("Prefabs/Feedback/GanhaDuasEstrela") as GameObject;
                Instantiate(feedbackEstrelas, GameObject.Find("Canvas").transform);
                yield return new WaitForSeconds(2);
            }
            else if (stars == 3)
            {
                feedbackEstrelas = Resources.Load("Prefabs/Feedback/GanhaTresEstrela") as GameObject;
                Instantiate(feedbackEstrelas, GameObject.Find("Canvas").transform);
                yield return new WaitForSeconds(2);
            }

            StartCoroutine(stageManager.CallAnotherLevel(3, NextLevel, true));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }

        //Caso o resultado esteja errado
        else if (getScoreNegative() == maxScore)
        {
            resultado = Resources.Load("Prefabs/Feedback/Level Failed Message") as GameObject;
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

    public void AddTimePlaying(float time)
    {
        timePlaying += time;
    }

    public void ResetTimePlaying()
    {
        timePlaying = 0;
    }

    public void UpdateTextStars()
    {
        Text starsTotal;
        starsTotal = GameObject.Find("Stars Total").GetComponent<Text>();
        starsTotal.text = SaveManager.player.totalEstrelas.ToString();
    }

    /// <summary>
    /// Ele verifica o tempo e a quantidade erros do jogador, o fator limitante que será dado como resultado
    /// </summary>
    private void GiveStars()
    {
        stars = Mathf.Min(CheckTime(), CheckError());

        int starsHadOnCurrentAct = SaveManager.player.planeta[stageManager.currentPlanet - 1].ato[stageManager.currentAct - 1]; //Estrelas que eu já tinha nesse ato

        if (stars > starsHadOnCurrentAct)
        {
            SaveManager.player.planeta[stageManager.currentPlanet - 1].ato[stageManager.currentAct-1] = stars;
            SaveManager.player.totalEstrelas += stars - starsHadOnCurrentAct; //Só soma as estrelas que eu ainda não tenho.
            GiveStarsForSystem(stars - starsHadOnCurrentAct);
            
        }
    }

    /// <summary>
    /// Coloca as estrelas ganhas no contador de cada um dos sistemas
    /// </summary>
    /// <param name="stars"></param>
    private void GiveStarsForSystem(int stars)
    {
        int system = stageManager.currentPlanet/5;

        /*
        if (stageManager.currentPlanet < 5)
        {
            system = 0;
        }
        else if (stageManager.currentPlanet < 10)
        {
            system = 1;
        }
        else if (stageManager.currentPlanet < 15)
        {
            system = 2;
        }
        else if (stageManager.currentPlanet < 20)
        {
            system = 3;
        }
        else if (stageManager.currentPlanet < 25)
        {
            system = 4;
        }*/
        SaveManager.player.estrelaSistema[system] += stars;
    }

    /// <summary>
    /// Verifica o tempo jogado pelo jogador e o tempo total que ele poderia gastar, a porcentagem disso é verificada para saber quão rápido o jogador foi
    /// </summary>
    /// <returns>Um inteiro com o número de estrelas que ele ganharia só dependendo do tempo</returns>
    private int CheckTime()
    {
        timer = Timer.instance;
        float totalTime = (/*scoreNegative +*/ scorePositive) * timer.totalTime; //Esse é o 100% do tempo gasto do jogador;
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

    #region FunçõesDinheiro

    public void GiveMoney()
    {
        int dinheiroEarned = MoneyCalculation();
        SaveManager.player.dinheiro += dinheiroEarned;
        Dinheiro.CreateGain(dinheiroEarned, moneyText.transform.position);
        UpdateTextMoney();
    }

    private bool ESistema0()
    {
        if (stageManager.currentPlanet < 5)
        {
            return true;
        }
        return false;
    }

    private int MoneyCalculation()
    {

        int dinheiro = LevelController.baseMoney;

        dinheiro += LevelController.bonusMoney * MoneyModifierByPlanet(); //Aplica o bônus pelo planeta

        dinheiro = dinheiro + ((dinheiro * stageManager.currentAct) / 5); //Aplica o bônus pelo ato

        if (ESistema0()) //Divide o dinheiro por 2 caso seja sistema 0
        {
            dinheiro = (dinheiro / 2);
        }

        if (SaveManager.player.CompletouPlaneta(stageManager.currentPlanet))
        {
            dinheiro = (dinheiro / 2);
        }

        return dinheiro;
    }

    private int MoneyModifierByPlanet()
    {
        int planet = stageManager.currentPlanet;

        if (ESistema0())
        {
            return planet;
        }

        while (planet > 9) //Deixa o planeta na faixa do 5 até o 9
        {
            planet -= 5;
        }
        return (planet - 4); //Resultado dá 1, 2, 3, 4, 5, dependendo do planeta
    }

    public void UpdateTextMoney()
    {
        moneyText.text = SaveManager.player.dinheiro.ToString();
    }
    #endregion
}
