using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonConfirmar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public static ButtonConfirmar instance = null;
    private Button buttonConfirmaResposta;
    private StageManager stageManager;
    private SoundManager soundManager;
    private Timer timer;
    private Score score;
    private Blinker blinker;
    private GameObject LevelClearMsg;
    private GameObject GameOver;
    private AudioClip acerto;
    private AudioClip erro;
    private Image[] RespostaCerta;
    private Image[] RespostaErrada;
    private Texture2D cursor;

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
        buttonConfirmaResposta = this.GetComponent<UnityEngine.UI.Button>();
        stageManager = StageManager.instance;
        soundManager = SoundManager.instance;
        timer = Timer.instance;
        score = Score.instance;
        blinker = Blinker.instance;

        cursor = Resources.Load<Texture2D>("Images/cursor-edit-th");
        LevelClearMsg = stageManager.GetLevelClearMsg();
        GameOver = stageManager.GetGameOver();
        acerto = stageManager.GetAcerto();
        erro = stageManager.GetErro();
        RespostaCerta = stageManager.GetRespostaCerta();
        RespostaErrada = stageManager.GetRespostaErrada();
    }

    private void Update()
    {
        if (LevelController.BotaoConfirmaResposta == true)//verifica se pode ativar o botao de confirmar resposta (só ativa quando foram digitados todos os caracteres das sílabas)
        {
            buttonConfirmaResposta.interactable = true;//ativa botao confirma resposta
        }
        else
        {
            buttonConfirmaResposta.interactable = false;//desativa botao confirma resposta
        }

        if (timer.endOfTime)
        {
            ConfirmaResposta();
        }
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
        timer.ResetTimeProgressBar(); //reset var para parar timer e barra de tempo
        StartCoroutine(score.CheckScore(1.5f * LevelController.NumeroDeSilabasDaPalavra, LevelClearMsg, GameOver, acerto, erro, stageManager.NextLevel, stageManager.PreviousLevel));
    }

    public IEnumerator VerificaRespostaCertaOuErrada(string silabaSelecionada, string silabaDigitada, int BlockIndex, float segundos)
    {
        timer.endOfTime = false;
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

    public void ActiveButton()
    {
        buttonConfirmaResposta.interactable = true;
    }

    public void DeactiveButton()
    {
        buttonConfirmaResposta.interactable = false;
    }

    /// Parte para o cursor mudar quando está em cima do ícone, porque

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonConfirmaResposta.interactable)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
