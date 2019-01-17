using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [Header("Detalhes do nível")]
    [Tooltip("Nível atual")]
    public int currentLevel;//Nível atual
    [Tooltip("Ato atual")]
    public int currentAct;
    /*[Tooltip("Quantidade de atos desse planeta")]
    public int maxAct;*/
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

    public static StageManager instance = null;

    private Text[] TelaSilabaDigitada;//caixa onde vão as letras digitadas pelo usuário
    private Score score;
    private ButtonConfirmar buttonConfirmar;
    private SilabaControl silabaControl;
    //private AnimationController levelChangerAnimController;

    // Para testes
    public string palavraSelecionada;

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
        LevelController.currentAct = currentAct;
        TelaSilabaDigitada = new Text[textSlots];
        for (int i = 0; i < textSlots; i++)
        {
            TelaSilabaDigitada[i] = GameObject.Find(string.Concat("Silaba Digitada ", i.ToString())).GetComponent <UnityEngine.UI.Text>();
        }

        LevelController.textSlots = textSlots;

        // Define se a palavra deve ser separada em sílabas ou letras
        LevelController.eSilaba = eSilaba;
        
        /*
        LevelController.CharLimitForLevel = CharLimitForThisLevel;//define limite de caracteres para o nível atual
        LevelController.NumeroDeSilabasDaPalavra = NumeroDeSilabasDaPalavra;
        */

        LevelController.InitializeVars();
    }

    void Start()
    {
        silabaControl = SilabaControl.instance;
        silabaControl.SilabaSetup(soundsDirectory);

        score = Score.instance;
        score.ScoreSetup();

        buttonConfirmar = ButtonConfirmar.instance;

        //AnimationController = LevelChangerAnimController.control;

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
        if (levelName == "06_stageSelect")
        {
            animTime = 2.5f;
        }
        else
        {
            animTime = 0.5f;
        }

        yield return new WaitForSeconds(animTime);

        SceneManager.LoadScene(levelName);
    }

    private void OnDestroy()
    {
        LevelController.TimeIsRunning = false;
        LevelController.TimePause = false;
    }
}