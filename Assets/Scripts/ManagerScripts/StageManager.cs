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
    public static StageManager instance = null;

    private Text[] TelaSilabaDigitada;//caixa onde vão as letras digitadas pelo usuário
    private Score score;
    private ButtonConfirmar buttonConfirmar;
    private SilabaControl silabaControl;
    //private AnimationController levelChangerAnimController;

        // Para testes
    public string palavraSelecionada;
    public int tamsilaba;
    public int tamsildig;
    public int tampalselecionada;
    public int tamtelasildig;

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
        tamsilaba = LevelController.originalText.Length;
        tampalselecionada = LevelController.PalavraSelecionada.Length;
        tamsildig = LevelController.inputText.Length;
        tamtelasildig = TelaSilabaDigitada.Length;

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

        //AnimationController.PlayTransitionSceneAnimation(levelClear);
        AnimationManager.instance.PlayTransitionSceneAnimation(levelClear);

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(levelName);
    }

}