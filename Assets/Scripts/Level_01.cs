using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_01 : MonoBehaviour {

    private int CharLimitForThisLevel = 2;//total de caracteres das silabas deste nivel juntas
    private int MaxScore = LevelController.MaxScoreGlobal;//pontuação objetivo para progredir ou regredir
    public GameObject LevelClearMsg;//gameobject da imagem de proximo nivel ou nivel anterior
    public GameObject GameOver;//gameobject da imagem de gameover
    private Object[] SilabasNivel01;//array de objetos par armazenar os áudios (sílabas)
    private AudioSource audioFile;
    public AudioClip erro;//audio do X vermelho de erro
    public AudioClip acerto;//audio das estrelas de acerto
    public AudioClip timer;//audio do relógio
    private int randomNumber;
    public Text TelaSilabaDigitada;//caixa onde vão as letras digitadas pelo usuário
    public Text scorePositive;//pontuação positiva
    public Text ScoreNegative;//pontuação negativa
    public Button BotaoConfirmaResposta;//botão para conferir a resposta
    public Button BotaoDicaAudio;
    public Button BotaoDicaVisual;
    public Image RespostaErrada;//imagem quando erra a resposta
    public Image RespostaCerta;//imagem quando acerta a resposta
    public Image TimeProgressBar;//imagem da barra de tempo
    private float ProgressBarTime;//controle barra de tempo
    private float TimeProgressBarSpeed = 0.5f;//velocidade que a barra de tempo enche

    void Awake()
    {        
        audioFile = GetComponent<AudioSource>();
        SilabasNivel01 = Resources.LoadAll("Sounds/Level_01", typeof(AudioClip));//carrega todos áudios dentro de Resources/Sounds/Level_01       
        audioFile.clip = SilabasNivel01[0] as AudioClip;
    }

    void Start () {
        LevelController.CharLimitForLevel = CharLimitForThisLevel;//define limite de caracteres para o nível atual
        RespostaErrada.enabled = false;//inicializa imagem de resposta errada para que não apareça a princípio
        RespostaCerta.enabled = false;//inicializa imagem de resposta certa para que não apareça a princípio
        TimeProgressBar.fillAmount = 0;//inicializa barra de tempo para começar vazia
        LevelClearMsg.SetActive(false);
        GameOver.SetActive(false);
        StartCoroutine(CallSilaba(1.5f));
    }

	void Update () {        
        scorePositive.text = LevelController.scorePositive.ToString();
        ScoreNegative.text = LevelController.NegativeScore.ToString();

        if (!LevelController.DicaVisualAtiva)
        {
            TelaSilabaDigitada.text = LevelController.SilabaDigitada;
        }

        if (LevelController.BotaoConfirmaResposta == true)//verifica se pode ativar o botao de confirmar resposta (só ativa quando foram digitados todos os caracteres das sílabas)
        {
            BotaoConfirmaResposta.interactable = true;//ativa botao confirma resposta
        }
        else
        {
            BotaoConfirmaResposta.interactable = false;//desativa botao confirma resposta
        }

        if (LevelController.TimeIsRunning)//bloco da barra de tempo inicio
        {
            if (!audioFile.isPlaying)//se já não está tocando...
            {
                audioFile.PlayOneShot(timer);//toca tick do timer
            }
            if (ProgressBarTime < 10)
            {
                ProgressBarTime += TimeProgressBarSpeed * Time.deltaTime;
                TimeProgressBar.fillAmount = ProgressBarTime / 10;
            } else
            {
                ConfirmaResposta();
            }
        }
    }

    public void TocarSilaba()//escolhe e toca uma sílaba aleatória (random nos arquivos de áudio)
    {
        randomNumber = Random.Range(0, SilabasNivel01.Length);
        LevelController.SilabaSelecionada = SilabasNivel01[randomNumber].name.ToUpper();//pega a sílaba (nome do arquivo sem a extensão) aleatóriamente
        audioFile.clip = SilabasNivel01[randomNumber] as AudioClip;
        audioFile.Play();//toca o áudio escolhido
        LevelController.TimeIsRunning = true;//set flag para iniciar a contagem do tempo e barra de tempo
    }

    IEnumerator CallSilaba(float seconds)//espera seconds e chama tocar silaba
    {
        yield return new WaitForSeconds(seconds);
        TocarSilaba();
        BotaoDicaAudio.interactable = true;//ativa botoes de ajuda após tocar nova silaba
        BotaoDicaVisual.interactable = true;//ativa botoes de ajuda após tocar nova silaba
    }

    public void ConfirmaResposta()
    {
        Debug.Log("Silaba Selecionada: "+LevelController.SilabaSelecionada);
        Debug.Log("Silaba Digitada: "+LevelController.SilabaDigitada);
        if (LevelController.SilabaDigitada.Equals(LevelController.SilabaSelecionada))//verifica se o que foi digitado é o mesmo que foi escolhido pelo sistema (falado para o usuário)
        {
            LevelController.scorePositive++;//set mais um no score positivo
            if (audioFile.isPlaying) { audioFile.Stop(); }//para o som do timer
            audioFile.PlayOneShot(acerto);//toca som de acerto
            StartCoroutine(DoBlinks(RespostaCerta, 1f, 0.2f));//pisca estrelas de acerto
        }
        else//caso a resposta esteja errada...
        {
            LevelController.NegativeScore++;//set mais um no score negativo
            if (audioFile.isPlaying) { audioFile.Stop(); }//para o som do timer
            audioFile.PlayOneShot(erro); //toca som de erro
            StartCoroutine(DoBlinks(RespostaErrada, 1f, 0.2f));//pisca X de erro

        }
        LevelController.BotaoConfirmaResposta = false;//disable button after click
        LevelController.SilabaDigitada = "";//reset var after confirm button is clicked
        LevelController.TimeIsRunning = false;//reset var para parar timer e barra de tempo
        TimeProgressBar.fillAmount = 0;//reset barra de tempo para começar vazia
        ProgressBarTime = 0;//reset timer
        if (LevelController.scorePositive >= MaxScore)
        {
            StartCoroutine(DoBlinksGameObject(acerto, 3, LevelClearMsg, 2f, 0.2f));
            StartCoroutine(CallAnotherLevel(6,"06_nivel02"));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }
        else if (LevelController.NegativeScore >= MaxScore) {
            StartCoroutine(DoBlinksGameObject(erro, 3, GameOver, 2f, 0.2f));
            StartCoroutine(CallAnotherLevel(6, "01_abertura"));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }
        else
        {
            StartCoroutine(CallSilaba(1.7f));//chama nova sílaba            
        }
    }

    IEnumerator CallAnotherLevel(float secondsBefore, string levelName)//espera seconds e chama outro nivel
    {
        yield return new WaitForSeconds(secondsBefore);       
        SceneManager.LoadScene(levelName);
        LevelController.scorePositive = 0;
        LevelController.NegativeScore = 0;
    }

    public void AcionaDicaAudio()//botao dica audio
    {
        audioFile.Play();//toca silaba atual
        BotaoDicaAudio.interactable = false;//desabilita botao dica audio
    }

    public void AcionaDicaVisual()//bota dica visual
    {
        BotaoDicaVisual.interactable = false;//desabilita dica visual
        StartCoroutine(MostraDica());
    }

    IEnumerator MostraDica()
    {
        LevelController.DicaVisualAtiva = true;
        TelaSilabaDigitada.text = LevelController.SilabaSelecionada;
        yield return new WaitForSeconds(1);
        TelaSilabaDigitada.text = LevelController.SilabaDigitada;
        LevelController.DicaVisualAtiva = false;
    }

    //inicio pisca Imagem(UI) por duration à taxa de blinkTime
    IEnumerator DoBlinks(Image resposta, float duration, float blinkTime)
    {
        while (duration > 0f)
        {
            duration -= 0.3f;
            ToggleState(resposta);
            yield return new WaitForSeconds(blinkTime);
        }
        RespostaErrada.enabled = false;//garantir que o estado final seja desligado, para a imagem sumir da tela
        RespostaCerta.enabled = false;
    }

    public void ToggleState(Image resposta)//muda o estado ligado/desligado de uma imagem (UI)
    {
        resposta.enabled = !resposta.enabled;
    }
    //fim pisca Imagem(UI) por duration à taxa de blinkTime

    //inicio pisca GameObject por duration à taxa de blinkTime, toca áudio e espera tempo antes caso necessário
    IEnumerator DoBlinksGameObject(AudioClip audioclip, float secondsBeforeBlink,  GameObject GameObjectToBlink, float duration, float blinkTime) 
    {
        yield return new WaitForSeconds(secondsBeforeBlink);
        audioFile.PlayOneShot(audioclip);
        while (duration > 0f)
        {
            duration -= 0.3f;
            ToggleStateGameObject(GameObjectToBlink);
            yield return new WaitForSeconds(blinkTime);
        }
       LevelClearMsg.SetActive(false);//garantir que o estado final seja desligado, para a imagem sumir da tela
    }

    public void ToggleStateGameObject(GameObject resposta)//muda o estado ligado/desligado de algum GameObject
    {
        if (resposta.activeSelf == true)
        {
            resposta.SetActive(false);
        }
        else
        {
            resposta.SetActive(true);
        }
    }
    //fim pisca GameObject por duration à taxa de blinkTime
}