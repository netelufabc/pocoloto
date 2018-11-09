using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_03 : MonoBehaviour
{
    private int CharLimitForThisLevel = 6;//total de caracteres das silabas deste nivel juntas
    private int NumeroDeSilabasDaPalavra = 3;
    private string NextLevel = "08_nivel04";
    private string PreviousLevel = "06_nivel02";
    private string SoundsDirectory = "Sounds/Level_03";

    private Object[] SilabasNivelAtual;//array de objetos par armazenar os áudios (sílabas)
    private int MaxScore = LevelController.MaxScoreGlobal;//pontuação objetivo para progredir ou regredir
    public GameObject LevelClearMsg;//gameobject da imagem de proximo nivel ou nivel anterior
    public GameObject GameOver;//gameobject da imagem de gameover    
    private AudioSource audioFile;
    public AudioClip erro;//audio do X vermelho de erro
    public AudioClip acerto;//audio das estrelas de acerto
    public AudioClip timer;//audio do relógio
    private int randomNumber;
    public Text[] TelaSilabaDigitada;//caixa onde vão as letras digitadas pelo usuário
    public Text scorePositive;//pontuação positiva
    public Text ScoreNegative;//pontuação negativa
    public Button BotaoConfirmaResposta;//botão para conferir a resposta
    public Button BotaoDicaAudio;
    public Button BotaoDicaVisual;
    public Image[] RespostaCerta;//imagem quando acerta a resposta
    public Image[] RespostaErrada;//imagem quando erra a resposta
    public Image TimeProgressBar;//imagem da barra de tempo
    private float ProgressBarTime;//controle barra de tempo
    private float TimeProgressBarSpeed = 0.5f;//velocidade que a barra de tempo enche

    void Awake()
    {
        audioFile = GetComponent<AudioSource>();
        SilabasNivelAtual = Resources.LoadAll(SoundsDirectory, typeof(AudioClip));//carrega todos áudios dentro de Resources/Sounds/Level_01       
        audioFile.clip = SilabasNivelAtual[0] as AudioClip;
        LevelController.NumeroDeSilabasDaPalavra = NumeroDeSilabasDaPalavra;
        LevelController.InitializeVars();
    }

    void Start()
    {
        LevelController.CharLimitForLevel = CharLimitForThisLevel;//define limite de caracteres para o nível atual
        for (int i = 0; i < LevelController.NumeroDeSilabasDaPalavra; i++)//inicializa imagens de resposta certa e errada para que não apareça a princípio
        {
            RespostaCerta[i].enabled = false;
            RespostaErrada[i].enabled = false;
        }
        TimeProgressBar.fillAmount = 0;//inicializa barra de tempo para começar vazia
        LevelClearMsg.SetActive(false);
        GameOver.SetActive(false);
        StartCoroutine(CallSilaba(1f));
    }

    void Update()
    {
        scorePositive.text = LevelController.scorePositive.ToString();
        ScoreNegative.text = LevelController.NegativeScore.ToString();

        if (!LevelController.DicaVisualAtiva)
        {
            TelaSilabaDigitada[0].text = LevelController.silabasDigitadas[0];
            TelaSilabaDigitada[1].text = LevelController.silabasDigitadas[1];
            TelaSilabaDigitada[2].text = LevelController.silabasDigitadas[2];
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
            }
            else
            {
                ConfirmaResposta();
            }
        }
    }

    public void TocarSilaba()//escolhe e toca uma sílaba aleatória (random nos arquivos de áudio)
    {
        randomNumber = Random.Range(0, SilabasNivelAtual.Length);
        LevelController.SilabaSelecionada = SilabasNivelAtual[randomNumber].name.ToUpper();//pega a sílaba (nome do arquivo sem a extensão) aleatóriamente        
        LevelController.SeparaSilabas();
        audioFile.clip = SilabasNivelAtual[randomNumber] as AudioClip;
        audioFile.Play();//toca o áudio escolhido     
        StartCoroutine(SetTimeIsRunning());
    }

    IEnumerator SetTimeIsRunning()//função para set a variavel de contar tempo e barra após a fala da palavra
    {
        yield return new WaitForSeconds(audioFile.clip.length);
        LevelController.TimeIsRunning = true;
    }

    IEnumerator CallSilaba(float seconds)//espera seconds e chama tocar silaba
    {
        yield return new WaitForSeconds(seconds);
        TocarSilaba();
        BotaoDicaAudio.interactable = true;//ativa botoes de ajuda após tocar nova silaba
        BotaoDicaVisual.interactable = true;//ativa botoes de ajuda após tocar nova silaba
    }

    public void ConfirmaResposta()//botao confirma é acionado
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

        StartCoroutine(SetScore(1.5f * LevelController.NumeroDeSilabasDaPalavra));

        LevelController.TimeIsRunning = false;//reset var para parar timer e barra de tempo
        TimeProgressBar.fillAmount = 0;//reset barra de tempo para começar vazia
        ProgressBarTime = 0;//reset timer

        StartCoroutine(CheckScore(1.5f * LevelController.NumeroDeSilabasDaPalavra));
    }

    IEnumerator CheckScore(float seconds)//verifica se atingiu o limite de pontuação para avançar ou voltar um nivel
    {
        yield return new WaitForSeconds(seconds + 0.2f);
        if (LevelController.scorePositive == MaxScore)
        {
            StartCoroutine(DoBlinksGameObject(acerto, 0, LevelClearMsg, 2f, 0.2f));
            StartCoroutine(CallAnotherLevel(3, NextLevel));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }
        else if (LevelController.NegativeScore == MaxScore)
        {
            StartCoroutine(DoBlinksGameObject(erro, 0, GameOver, 2f, 0.2f));
            StartCoroutine(CallAnotherLevel(3, PreviousLevel));//espera o dobro do tempo pois esta funcao é chamada ao mesmo tempo que a da linha de cima
        }
        else
        {
            StartCoroutine(CallSilaba(0));//chama nova sílaba            
        }
    }

    IEnumerator SetScore(float seconds)//set a pontuação
    {
        yield return new WaitForSeconds(seconds);
        if (LevelController.AlgumaSilabaErrada)
        {
            LevelController.NegativeScore++;
        }
        else
        {
            LevelController.scorePositive++;
        }
        LevelController.AlgumaSilabaErrada = false;
    }

    IEnumerator VerificaRespostaCertaOuErrada(string silabaSelecionada, string silabaDigitada, int BlockIndex, float segundos)
    {
        if (silabaDigitada.Equals(silabaSelecionada))//verifica se o que foi digitado é o mesmo que foi escolhido pelo sistema (falado para o usuário)
        {
            yield return new WaitForSeconds(segundos);
            if (audioFile.isPlaying) { audioFile.Stop(); }//para o som do timer
            audioFile.PlayOneShot(acerto);//toca som de acerto
            StartCoroutine(DoBlinks(RespostaCerta[BlockIndex], 1f, 0.2f));//pisca estrelas de acerto                  
        }
        else//caso a resposta esteja errada...
        {
            LevelController.AlgumaSilabaErrada = true;
            yield return new WaitForSeconds(segundos);
            if (audioFile.isPlaying) { audioFile.Stop(); }//para o som do timer
            audioFile.PlayOneShot(erro); //toca som de erro
            StartCoroutine(DoBlinks(RespostaErrada[BlockIndex], 1f, 0.2f));//pisca estrelas de acerto                   
        }
    }

    IEnumerator CallAnotherLevel(float secondsBefore, string levelName)//espera seconds e chama outro nivel
    {
        yield return new WaitForSeconds(secondsBefore);
        LevelController.scorePositive = 0;
        LevelController.NegativeScore = 0;
        SceneManager.LoadScene(levelName);
    }

    public void AcionaDicaAudio()//botao dica audio
    {
        audioFile.Play();//toca silaba atual
        BotaoDicaAudio.interactable = false;//desabilita botao dica audio
    }

    public void AcionaDicaVisual()//botao dica visual
    {
        BotaoDicaVisual.interactable = false;//desabilita dica visual
        StartCoroutine(MostraDica());
    }

    IEnumerator MostraDica()
    {
        LevelController.DicaVisualAtiva = true;
        randomNumber = Random.Range(0, LevelController.NumeroDeSilabasDaPalavra - 1);
        TelaSilabaDigitada[randomNumber].text = LevelController.silabas[randomNumber];
        yield return new WaitForSeconds(1);
        TelaSilabaDigitada[randomNumber].text = LevelController.silabasDigitadas[randomNumber];
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
        for (int i = 0; i < LevelController.NumeroDeSilabasDaPalavra; i++)//garantir que o estado final seja desligado, para a imagem sumir da tela
        {
            RespostaCerta[i].enabled = false;
            RespostaErrada[i].enabled = false;
        }
    }

    public void ToggleState(Image resposta)//muda o estado ligado/desligado de uma imagem (UI)
    {
        resposta.enabled = !resposta.enabled;
    }
    //fim pisca Imagem(UI) por duration à taxa de blinkTime

    //inicio pisca GameObject por duration à taxa de blinkTime, toca áudio e espera tempo antes caso necessário
    IEnumerator DoBlinksGameObject(AudioClip audioclip, float secondsBeforeBlink, GameObject GameObjectToBlink, float duration, float blinkTime)
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