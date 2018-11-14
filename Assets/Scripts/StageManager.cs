using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [Header("Detalhes do nível")]
    [Tooltip("Nível atual (int)")]
    public int currentLevel;//Nível atual
    [Tooltip("Total de caracteres juntas nas sílabas deste nível")]
    public int CharLimitForThisLevel;//total de caracteres das silabas deste nivel juntas
    [Tooltip("Número de sílabas diferentes")]
    public int NumeroDeSilabasDaPalavra;
    [Tooltip("Nome da scene do próximo nível")]
    public string NextLevel;
    [Tooltip("Nome da scene do nível anterior")]
    public string PreviousLevel;
    [Tooltip("Diretório de sons do nível")]
    public string soundsDirectory;

    //private SoundManager SM; //Testando
    public static StageManager instance = null;
    private SoundManager soundManager;
    private Score score;
    private Blinker blinker;
    private Timer timer;
    private ButtonDicaAudio buttonDicaAudio;
    private GameObject LevelClearMsg;//gameobject da imagem de proximo nivel ou nivel anterior
    private GameObject GameOver;//gameobject da imagem de gameover    
    private Button BotaoConfirmaResposta;//botão para conferir a resposta
    private Button BotaoDicaVisual;
    private AudioClip erro;//audio do X vermelho de erro
    private AudioClip acerto;//audio das estrelas de acerto
    private Text[] TelaSilabaDigitada;//caixa onde vão as letras digitadas pelo usuário
    private Image[] RespostaCerta;//imagem quando acerta a resposta
    private Image[] RespostaErrada;//imagem quando erra a resposta
    private SilabaControl silabaControl;

    private Object[] PalavrasNivelAtual;//array de objetos par armazenar os áudios (sílabas)
    private int randomNumber;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        LevelController.currentLevel = currentLevel;

        TelaSilabaDigitada = new Text[NumeroDeSilabasDaPalavra];
        for (int i =0; i < NumeroDeSilabasDaPalavra; i++)
        {
            TelaSilabaDigitada[i] = GameObject.Find(string.Concat("Silaba Digitada ", i.ToString())).GetComponent <UnityEngine.UI.Text>();
        }

       
        RespostaCerta = new Image[NumeroDeSilabasDaPalavra];
        for (int i = 0; i < NumeroDeSilabasDaPalavra; i++)
        {
            RespostaCerta[i] = GameObject.Find(string.Concat("RespostaCerta", i.ToString())).GetComponent<UnityEngine.UI.Image>();
        }

        RespostaErrada = new Image[NumeroDeSilabasDaPalavra];
        for (int i = 0; i < NumeroDeSilabasDaPalavra; i++)
        {
            RespostaErrada[i] = GameObject.Find(string.Concat("RespostaErrada", i.ToString())).GetComponent<UnityEngine.UI.Image>();
        }

        LevelClearMsg = GameObject.Find("Level Clear");
        GameOver = GameObject.Find("Level Failed");
        BotaoConfirmaResposta = GameObject.Find("Button Confirma Resposta").GetComponent<UnityEngine.UI.Button>();

        erro = (AudioClip)Resources.Load("Sounds/sfx/erro_slot01");         
        acerto = (AudioClip)Resources.Load("Sounds/sfx/acerto_slot01");


        LevelController.NumeroDeSilabasDaPalavra = NumeroDeSilabasDaPalavra;
        LevelController.InitializeVars();
    }

    void Start()
    {
        silabaControl = SilabaControl.instance;
        silabaControl.SilabaSetup(soundsDirectory);

        score = Score.instance;
        score.ScoreSetup();

        soundManager = SoundManager.instance;

        blinker = Blinker.instance;

        timer = Timer.instance;

        buttonDicaAudio = ButtonDicaAudio.instance;

        LevelController.CharLimitForLevel = CharLimitForThisLevel;//define limite de caracteres para o nível atual
        for (int i = 0; i < LevelController.NumeroDeSilabasDaPalavra; i++)//inicializa imagens de resposta certa e errada para que não apareça a princípio
        {
            RespostaCerta[i].enabled = false;
            RespostaErrada[i].enabled = false;
        }
        LevelClearMsg.SetActive(false);
        GameOver.SetActive(false);
        StartCoroutine(silabaControl.CallSilaba(1f));
    }

    void Update()
    {
        if (!LevelController.DicaVisualAtiva)
        {
            for (int i = 0; i<NumeroDeSilabasDaPalavra; i++)
            {
                TelaSilabaDigitada[i].text = LevelController.silabasDigitadas[i];
            }
        }

        if (LevelController.BotaoConfirmaResposta == true)//verifica se pode ativar o botao de confirmar resposta (só ativa quando foram digitados todos os caracteres das sílabas)
        {
            BotaoConfirmaResposta.interactable = true;//ativa botao confirma resposta
        }
        else
        {
            BotaoConfirmaResposta.interactable = false;//desativa botao confirma resposta
        }

        if (timer.EndOfTime)
        {
            ConfirmaResposta();
        }  
    }

     public Text[] GetTelaSilabaDigitada()
    {
        return TelaSilabaDigitada;
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
        timer.ResetTimeProgressBar();


        StartCoroutine(score.CheckScore(1.5f * LevelController.NumeroDeSilabasDaPalavra, LevelClearMsg, GameOver, acerto, erro, NextLevel, PreviousLevel));
        BotaoDicaVisual.interactable = true;//ativa botoes de ajuda após tocar nova silaba Colocar no som
    }

    public IEnumerator VerificaRespostaCertaOuErrada(string silabaSelecionada, string silabaDigitada, int BlockIndex, float segundos)
    {
        timer.EndOfTime = false;
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
        
    }
    
    public static IEnumerator CallAnotherLevel(float secondsBefore, string levelName)//espera seconds e chama outro nivel
    {
        yield return new WaitForSeconds(secondsBefore);
        SceneManager.LoadScene(levelName);
    }

}