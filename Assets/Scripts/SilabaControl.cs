using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SilabaControl : MonoBehaviour {

    public static SilabaControl instance = null;
    private SoundManager soundManager;
    private ButtonDicaAudio buttonDicaAudio;
    private ButtonDicaVisual buttonDicaVisual;
    private Timer timer;
    private DistractorCreator distractorCreator;
    private Object[] silabasNivelAtual;

    private int randomNumber;
    private AudioClip silabaAtual;

    private StageManager stageManager;

    public float wordTime;

    // Indica se o textSlot contém uma planetLetter
    public bool[] isPlanetLetter;
    public int numberOfValidSlots;
    

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

    /// <summary>
    /// Prepara a classe SilabaControl carregando todos os arquivos das silabas nelas
    /// </summary>
    /// <param name="soundsDirectory">Diretório de sons das silabas</param>
    public void SilabaSetup(string soundsDirectory)
    {
        silabasNivelAtual = Resources.LoadAll(soundsDirectory, typeof(AudioClip));//carrega todos áudios dentro de Resources/Sounds/Level_01

        soundManager = SoundManager.instance;
        buttonDicaAudio = ButtonDicaAudio.instance;
        buttonDicaVisual = ButtonDicaVisual.instance;
        timer = Timer.instance;
        stageManager = StageManager.instance;

        isPlanetLetter = new bool[stageManager.textSlots];
    }

    /// <summary>
    /// Procura se a fase possui um criador de distratores, depois chama o Tocar Silaba. 
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public IEnumerator CallSilaba(float seconds)
    {
        if (GameObject.Find("Distractor Creator"))
        {
            DistractorCreator distractorCreator;
            distractorCreator = DistractorCreator.instance;
            StartCoroutine(distractorCreator.StartDistractors());
        }

        yield return new WaitForSeconds(seconds);
        TocarSilaba();
    }

    /// <summary>
    /// Escolhe uma silaba aleatoriamente dentro dos arquivos de áudio carregados em "silabasNivelAtual" e a toca
    /// </summary>
    public void TocarSilaba()//escolhe e toca uma sílaba aleatória (random nos arquivos de áudio)
    {
        randomNumber = Random.Range(0, silabasNivelAtual.Length);
        LevelController.PalavraSelecionada = silabasNivelAtual[randomNumber].name.ToUpper();//pega a sílaba (nome do arquivo sem a extensão) aleatóriamente

        // Verifica se a PalavraSelecionada deve ser separada em sílabas ou letras
        if (LevelController.eSilaba)
        {
            LevelController.SeparaSilabas();
        }
        else
        {
            LevelController.SeparaLetras();
        }

        IsPlanetLetterSetup();

        silabaAtual = silabasNivelAtual[randomNumber] as AudioClip;
        wordTime = silabaAtual.length;
        soundManager.PlaySilaba(silabaAtual);
        StartCoroutine(WaitForSound(silabaAtual.length));
        StartCoroutine(timer.SetTimeIsRunning(silabaAtual));

        buttonDicaAudio.ActiveButton();
        buttonDicaVisual.ActiveButton();

        if (stageManager.blockTextSlot)
        {
            BloqueiaEmptyTextSlots();
        }
    }

    /// <summary>
    /// Toca a última silaba tocada (usada para o botão de dica)
    /// </summary>
    public void TocarSilabaAtual()
    {
        soundManager.PlaySilaba(silabaAtual);
    }

    /// <summary>
    /// Espera o término da sílaba que está sendo tocada para liberar os botões do teclado virtual
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator WaitForSound(float duration)
    {
        yield return new WaitForSeconds(duration);

        LevelController.bloqueiaBotao = false; // Libera o uso do teclado virtual
    }

    /// <summary>
    /// Pega as vogais da PalavraSelecionada e as retorna em uma string
    /// </summary>
    /// <returns></returns>
    public string VogaisDaPalavra()
    {
        string vogaisDaPalavra = "";
        string vogais = "AEIOU";

        for (int i = 0; i < LevelController.textSlots; i++)
        {
            if (vogais.IndexOf(LevelController.PalavraSelecionada[i]) != -1)
            {
                vogaisDaPalavra = string.Concat(vogaisDaPalavra, LevelController.PalavraSelecionada[i]);
            }
        }

        return vogaisDaPalavra;
    }

    /// <summary>
    /// Preenche o vetor isPlanetLetter
    /// </summary>
    public void IsPlanetLetterSetup()
    {
        numberOfValidSlots = 0;
        for (int i = 0; i < LevelController.textSlots; i++)
        {
            if (stageManager.planetLetters.Length != 0)
            {
                isPlanetLetter[i] = false;

                for (int j = 0; j < stageManager.planetLetters.Length; j++)
                {
                    if (LevelController.originalText[i].IndexOf(stageManager.planetLetters[j]) != -1)
                    {
                        isPlanetLetter[i] = true;
                        numberOfValidSlots++;
                        break;
                    }
                }
            }
            else
            {
                isPlanetLetter[i] = true;
                numberOfValidSlots = LevelController.textSlots;
            }
        }
    }

    /// <summary>
    /// Completa as lacunas com as letras que não estão em foco de estudo
    /// </summary>
    public void CompleteEmptyTextSlots()
    {
        for (int i = 0; i < LevelController.textSlots; i++)
        {
            /*bool focoEncontrado = false;

            for (int j = 0; j < stageManager.planetLetters.Length; j++)
            {
                //int a = LevelController.originalText[i].IndexOf(stageManager.planetLetters[j]);
                //Debug.Log(stageManager.planetLetters[j] + " " + LevelController.originalText[i] + " " + a);
                if (LevelController.originalText[i].IndexOf(stageManager.planetLetters[j]) != -1)
                {
                    focoEncontrado = true;
                    break;
                }
            }

            if (!focoEncontrado)
            {
                LevelController.inputText[i] = LevelController.originalText[i];
            }*/
            if (!isPlanetLetter[i])
            {
                LevelController.inputText[i] = LevelController.originalText[i];
            }
        }
    }

    /// <summary>
    /// Bloqueia os slots que não devem ser preenchidos (serão preenchidos automaticamente depois)
    /// </summary>
    public void BloqueiaEmptyTextSlots()
    {
        for (int i = 0; i < LevelController.textSlots; i++)
        {
            /*bool focoEncontrado = false;

            for (int j = 0; j < stageManager.planetLetters.Length; j++)
            {
                //int a = LevelController.originalText[i].IndexOf(stageManager.planetLetters[j]);
                //Debug.Log(stageManager.planetLetters[j] + " " + LevelController.originalText[i] + " " + a);
                if (LevelController.originalText[i].IndexOf(stageManager.planetLetters[j]) != -1)
                {
                    focoEncontrado = true;
                    break;
                }
            }

            if (!focoEncontrado)
            {
                LevelController.inputText[i] = "     ";
            }*/
            if (!isPlanetLetter[i])
            {
                LevelController.inputText[i] = "     ";
            }
        }
    }

    public void CorrigeSlots()
    {
        for (int i = 0; i<LevelController.textSlots; i++)
        {
            if (!LevelController.inputText[i].Equals(LevelController.originalText[i]))
            {
                ///Toca animação bolada
                LevelController.inputText[i] = LevelController.originalText[i];
            }
        }
    }
}
