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

    private Text[] TelaSilabaDigitada;//caixa onde vão as letras digitadas pelo usuário
    private Score score;
    private ButtonConfirmar buttonConfirmar;
    private SilabaControl silabaControl;

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
        GameObject transitionBetweenScene = Resources.Load("Prefabs/Hyperspace_2") as GameObject;

        yield return new WaitForSeconds(secondsBefore);
        transitionBetweenScene = Instantiate(transitionBetweenScene);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelName);
    }

}