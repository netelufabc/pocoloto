using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilabaControl : MonoBehaviour {

    public static SilabaControl instance = null;
    private SoundManager soundManager;
    private static SilabaControl single_instance;
    private Object[] silabasNivelAtual;
    private string soundsDirectory;

    private int randomNumber;
    private AudioClip silabaAtual;

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
        soundManager = SoundManager.instance;
    }


    public IEnumerator CallSilaba(float seconds)//espera seconds e chama tocar silaba
    {
        yield return new WaitForSeconds(seconds);
        TocarSilaba();
        //BotaoDicaAudio.interactable = true;//ativa botoes de ajuda após tocar nova silaba Colocar no som
        //BotaoDicaVisual.interactable = true;//ativa botoes de ajuda após tocar nova silaba Colocar no som
    }

    /// <summary>
    /// Prepara a classe SilabaControl carregando todos os arquivos das silabas nelas
    /// </summary>
    /// <param name="soundsDirectory">Diretório de sons das silabas</param>
    public void SilabaSetup(string soundsDirectory)
    {
        this.soundsDirectory = soundsDirectory;
        silabasNivelAtual = Resources.LoadAll(soundsDirectory, typeof(AudioClip));//carrega todos áudios dentro de Resources/Sounds/Level_01
    }

    /// <summary>
    /// Escolhe uma silaba aleatoriamente dentro dos arquivos de áudio carregados em "silabasNivelAtual" e a toca
    /// </summary>
    public void TocarSilaba()//escolhe e toca uma sílaba aleatória (random nos arquivos de áudio)
    {
        randomNumber = Random.Range(0, silabasNivelAtual.Length);
        LevelController.PalavraSelecionada = silabasNivelAtual[randomNumber].name.ToUpper();//pega a sílaba (nome do arquivo sem a extensão) aleatóriamente        
        LevelController.SeparaSilabas();
        silabaAtual = silabasNivelAtual[randomNumber] as AudioClip;
        soundManager.PlaySilaba(silabasNivelAtual[randomNumber] as AudioClip);
    }

    /// <summary>
    /// Toca a última silaba tocada (usada para o botão de dica)
    /// </summary>
    public void TocarSilabaAtual()
    {
        soundManager.PlaySilaba(silabaAtual);
    }
}
