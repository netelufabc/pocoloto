using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public Color correctColor; //Cor que o texto fica quando se acerta a palavra
    public Color mistakeColor; //Cor que o texto fica quando se erra a palavra

    // Indica quantas vezes a mesma palavra pode ser escolhida em sequência
    private int repeatMax = 1;
    // Utilizado para verificar quantas vezes o mesmo número saiu em sequencia no random
    private int repeatCount = 0;
    // Guarda o número random anterior
    private int prevRandom = -1;

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

    public void SilabaSetup(string soundsDirectory, string[] pathActs)
    {
        Object[] newSilabasNivelAtual;

        silabasNivelAtual = Resources.LoadAll(string.Concat(soundsDirectory, pathActs[0]), typeof(AudioClip));

        for (int i = 1; i < pathActs.Length; i++)
        {
            string newSoundsDirectory = string.Concat(soundsDirectory, pathActs[i]);
            newSilabasNivelAtual = Resources.LoadAll(newSoundsDirectory, typeof(AudioClip));//carrega todos áudios dentro de Resources/Sounds/Level_01
            silabasNivelAtual = silabasNivelAtual.Concat(newSilabasNivelAtual).ToArray();
        }

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
        yield return new WaitForSeconds(seconds);
        stageManager.ResetColorSilabaDigitada();
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("PauseMenu") == false); //Se o menu está aberto, espera ele ser fechado

        if (GameObject.Find("Main Camera").GetComponent<StageManager>() != null) //Só toca silaba se ele encontra o componente StageManager na Camera
        {
            TocarSilaba();
        }

        if (GameObject.Find("Distractor Creator")) //Procura se há um Distractor Creator na scene
        {
            DistractorCreator distractorCreator;
            distractorCreator = DistractorCreator.instance;
            StartCoroutine(distractorCreator.StartDistractors());
        }

        StartCoroutine(stageManager.BloquearMenu(wordTime));
    }

    /// <summary>
    /// Escolhe uma silaba aleatoriamente dentro dos arquivos de áudio carregados em "silabasNivelAtual" e a toca
    /// </summary>
    public void TocarSilaba()//escolhe e toca uma sílaba aleatória (random nos arquivos de áudio)
    {
        //randomNumber = Random.Range(0, silabasNivelAtual.Length);
        randomNumber = RandomNotSoRandom();
        LevelController.PalavraSelecionada = silabasNivelAtual[randomNumber].name.ToUpper();//pega a sílaba (nome do arquivo sem a extensão) aleatóriamente
        // Guarda a palavra selecionada para o jogador
        DataManager.statisticsData.palavraSelecionada = LevelController.PalavraSelecionada;

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
            StartCoroutine(WaitAndWriteEmptySlots());
        }

        // Se não for o caso de ter distratores, instancia a seta agora (se tiver distratodres, instancia no script Distractor)
        if (!GameObject.Find("Distractor Creator"))
        {
            SetaIndicadora.SetaSetup();
            //SetaIndicadora.DestroiSeta();
            SetaIndicadora.IndicarPos();
        }
    }

    /// <summary>
    /// Aguarda um tempo antes de completar as lacunas em branco com as letras que não são o foco do planeta
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitAndWriteEmptySlots()
    {
        yield return new WaitForSeconds(1.5f);
        CompleteEmptyTextSlots();
    }

    /// <summary>
    /// Toca a última silaba tocada (usada para o botão de dica)
    /// </summary>
    public void TocarSilabaAtual()
    {
        soundManager.PlaySilaba(silabaAtual);
    }

    /// <summary>
    /// Retorna tempo, em segs, do silabaAtual
    /// </summary>
    /// <returns>Float com o tempo em segundos</returns>
    public float TimeSilabaAtual()
    {
        return silabaAtual.length;
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
            if (!isPlanetLetter[i])
            {
                LevelController.inputText[i] = "     ";
            }
        }
    }

    public void CorrigeSlots()
    {
        bool errou = false;
        for (int i = 0; i<LevelController.textSlots; i++)
        {
            if (LevelController.inputText[i] == null || !LevelController.inputText[i].Equals(LevelController.originalText[i]))
            {
                LevelController.inputText[i] = LevelController.originalText[i];
                errou = true;
            }
        }

        if (errou)
        {
            stageManager.ChangeColorTelaSilabaDigitada(mistakeColor);
        }
        else
        {
            stageManager.ChangeColorTelaSilabaDigitada(correctColor);
        }
    }

    /// <summary>
    /// Sorteia um número aleatório, mas respeitando um número máximo de repetições do mesmo número
    /// </summary>
    /// <returns></returns>
    public int RandomNotSoRandom()
    {
        int tempRandom;
        do
        {
            tempRandom = Random.Range(0, silabasNivelAtual.Length);
            if (prevRandom == tempRandom)
            {
                if (repeatCount < repeatMax)
                {
                    repeatCount++;
                    break;
                }
            }
            else
            {
                prevRandom = tempRandom;
                repeatCount = 0;
                break;
            }
        } while (true);

        return tempRandom;
    }
}
