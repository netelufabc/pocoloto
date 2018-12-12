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

    public static StageManager instance = null;
    private Score score;
    private GameObject LevelClearMsg;//gameobject da imagem de proximo nivel ou nivel anterior
    private GameObject GameOver;//gameobject da imagem de gameover    
    private Button BotaoConfirmaResposta;//botão para conferir a resposta

    private Button BotaoDicaVisual;
    private ButtonConfirmar buttonConfirmar;

    private AudioClip erro;//audio do X vermelho de erro
    private AudioClip acerto;//audio das estrelas de acerto
    private Text[] TelaSilabaDigitada;//caixa onde vão as letras digitadas pelo usuário
    private SilabaControl silabaControl;

    private Object[] PalavrasNivelAtual;//array de objetos par armazenar os áudios (sílabas)
    private int randomNumber;

    public GameObject GetLevelClearMsg()
    {
        return LevelClearMsg;
    }

    public GameObject GetGameOver()
    {
        return GameOver;
    }

    public AudioClip GetAcerto()
    {
        return acerto;
    }

    public AudioClip GetErro()
    {
        return erro;
    }

    public Text[] GetTelaSilabaDigitada()
    {
        return TelaSilabaDigitada;
    }

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

        LevelClearMsg = GameObject.Find("Level Clear");
        GameOver = GameObject.Find("Level Failed");

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

        buttonConfirmar = ButtonConfirmar.instance;

        LevelController.CharLimitForLevel = CharLimitForThisLevel;//define limite de caracteres para o nível atual

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
            buttonConfirmar.ActiveButton();//ativa botao confirma resposta
        }
        else
        {
            buttonConfirmar.DeactiveButton();//desativa botao confirma resposta
        }
    }

    
    public static IEnumerator CallAnotherLevel(float secondsBefore, string levelName)//espera seconds e chama outro nivel
    {
        yield return new WaitForSeconds(secondsBefore);
        SceneManager.LoadScene(levelName);
    }

}