using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [Header("Detalhes do nível")]
    [Tooltip("Nível atual")]
    public int currentPlanet;//Nível atual
    [Tooltip("Ato atual")]
    public int currentAct;
    public int moneyModifier; //Discutir como modificar isso
    [Tooltip("Letras foco para aprendizado no nível (se for 0, todas as letras serão as letras foco)")]
    public string[] planetLetters;

    /*
    [Tooltip("Total de caracteres juntas nas sílabas deste nível")]
    public int CharLimitForThisLevel;//total de caracteres das silabas deste nivel juntas
    [Tooltip("Número de sílabas diferentes")]
    public int NumeroDeSilabasDaPalavra;
    */
    [Tooltip("Quantidade de 'quadrados brancos' a serem preenchidos")]
    public int textSlots;

    [Tooltip("Nome da scene do próximo nível")]
    public string NextLevel;
    [Tooltip("Nome da scene do nível anterior")]
    public string PreviousLevel;
    [Tooltip("Diretório de sons do nível")]
    public string soundsDirectory;
    [Tooltip("A fase é sílabas ou letras")]
    public bool eSilaba = true;
    [Tooltip("Algum campo deve ser bloqueado (i.e. não ser digitado)?")]
    public bool blockTextSlot = false;
    [Tooltip("Indica se a fase é de revisão")]
    public bool eRevisao = false;
    [Tooltip("Indicar quantos planetas tem no size, e os caminhos a partir do Sistema X de cada um")]
    public string[] pathAct;
    public static StageManager instance = null;

    private Text[] TelaSilabaDigitada;//caixa onde vão as letras digitadas pelo usuário
    private Color telaSilabaDigitadaDefaulColor;
    private Score score;
    private ButtonConfirmar buttonConfirmar;
    private GameObject closeButton;
    private SilabaControl silabaControl;
    //private AnimationController levelChangerAnimController;

    // Para testes
    public string palavraSelecionada;
    public Player player;
    public VideoClip video;

    public Text[] GetTelaSilabaDigitada()
    {
        return TelaSilabaDigitada;
    }

    public void ChangeColorTelaSilibaDigitada(int index, Color color)
    {
        TelaSilabaDigitada[index].color = color;
    }

    public void ChangeColorTelaSilabaDigitada(Color color)
    {
        foreach (Text silabaDigitada in TelaSilabaDigitada)
        {
            silabaDigitada.color = color;
        }
    }

    public void ResetColorSilabaDigitada()
    {
        foreach(Text silabaDigitada in TelaSilabaDigitada)
        {
            silabaDigitada.color = telaSilabaDigitadaDefaulColor;
        }
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

        LevelController.TimePause = false;
        LevelController.TimeIsRunning = false;

        LevelController.currentPlanet = currentPlanet;
        LevelController.currentAct = currentAct;
        TelaSilabaDigitada = new Text[textSlots];
        for (int i = 0; i < textSlots; i++)
        {
            TelaSilabaDigitada[i] = GameObject.Find(string.Concat("Silaba Digitada ", i.ToString())).GetComponent <UnityEngine.UI.Text>();
        }
        telaSilabaDigitadaDefaulColor = TelaSilabaDigitada[0].color;
        LevelController.textSlots = textSlots;

        // Define se a palavra deve ser separada em sílabas ou letras
        LevelController.eSilaba = eSilaba;
        
        LevelController.InitializeVars();

        GameObject itensInfoList = Resources.Load("Prefabs/ItensInfoList") as GameObject;
        GameObject tecladoHolografico = GameObject.Find("teclado_holografico");
        tecladoHolografico.GetComponent<SpriteRenderer>().sprite = itensInfoList.GetComponent<ItensInfoList>().colorsToSell[SaveManager.player.colorSelecionadoIndex].itemSprite;
        tecladoHolografico.GetComponent<SpriteRenderer>().color = itensInfoList.GetComponent<ItensInfoList>().colorsToSell[SaveManager.player.colorSelecionadoIndex].itemColor;
    }

    void Start()
    {
        LevelController.ButtonFecharBloqueado = true;

        silabaControl = SilabaControl.instance;

        if (eRevisao)
        {
            silabaControl.SilabaSetup(soundsDirectory, pathAct);
        }
        else
        {
            silabaControl.SilabaSetup(soundsDirectory);
        }

        score = Score.instance;
        score.ScoreSetup();
        score.UpdateTextStars();
        score.UpdateTextMoney();

        buttonConfirmar = ButtonConfirmar.instance;
        closeButton = GameObject.Find("Button - Fechar");

        //AnimationController = LevelChangerAnimController.control;

        // Pega a informação do sistema, planeta e do ato
        DataManager.statisticsData.sistema = (currentPlanet / 5).ToString();
        DataManager.statisticsData.planeta = currentPlanet.ToString();
        DataManager.statisticsData.ato = currentAct.ToString();

        StartCoroutine(silabaControl.CallSilaba(1f)); //Começa a chamar as silabas

    }

    void Update()
    {
        // para testes
        palavraSelecionada = LevelController.PalavraSelecionada;

        if (!LevelController.DicaVisualAtiva)
        {
            for (int i = 0; i < textSlots; i++)
            {
                TelaSilabaDigitada[i].text = LevelController.inputText[i];
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

    public IEnumerator BloquearMenu(float tempo)
    {
        LevelController.ButtonFecharBloqueado = true;
        yield return new WaitForSeconds(tempo);
        LevelController.ButtonFecharBloqueado = false;
    }

    /// <summary>
    /// Chama o proximo nível e informa o AnimatorManager a animação que deve ser tocada
    /// </summary>
    /// <param name="secondsBefore"></param>
    /// <param name="levelName"></param>
    /// <param name="levelClear"></param>
    /// <returns></returns>
    public IEnumerator CallAnotherLevel(float secondsBefore, string levelName, bool levelClear)//espera seconds e chama outro nivel
    {
        yield return new WaitForSeconds(secondsBefore);
        float animTime;

        //AnimationController.PlayTransitionSceneAnimation(levelClear);
        AnimationManager.instance.PlayTransitionSceneAnimation(levelClear, levelName);
        if (levelName.Contains("stageSelect"))
        {
            animTime = 2.5f;
        }
        else
        {
            animTime = 0.5f;
        }

        yield return new WaitForSeconds(animTime);

        if (video != null)
        {
            VideoManager videoManager;
            videoManager = VideoManager.instance;
            videoManager.TakeVideo(video);
            TeachingScenes.nextScene = levelName;
            SceneManager.LoadScene("09_explicacao");
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
    }
}