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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            soundManager = SoundManager.instance;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

   public IEnumerator CallSilaba(float seconds)//espera seconds e chama tocar silaba
    {
        Debug.Log("Entrou no CallSilaba");
        yield return new WaitForSeconds(seconds);
        TocarSilaba();
        //BotaoDicaAudio.interactable = true;//ativa botoes de ajuda após tocar nova silaba Colocar no som
        //BotaoDicaVisual.interactable = true;//ativa botoes de ajuda após tocar nova silaba Colocar no som
    }


    public void SilabaSetup(string soundsDirectory)
    {
        Debug.Log("Entrou no SilabaSetup");
        this.soundsDirectory = soundsDirectory;
        silabasNivelAtual = Resources.LoadAll(soundsDirectory, typeof(AudioClip));//carrega todos áudios dentro de Resources/Sounds/Level_01       
    }

    public void TocarSilaba()//escolhe e toca uma sílaba aleatória (random nos arquivos de áudio)
    {
        Debug.Log("Entrou no TocarSilaba()");
        randomNumber = Random.Range(0, silabasNivelAtual.Length);
        LevelController.PalavraSelecionada = silabasNivelAtual[randomNumber].name.ToUpper();//pega a sílaba (nome do arquivo sem a extensão) aleatóriamente        
        LevelController.SeparaSilabas();
        soundManager.PlaySilaba(silabasNivelAtual[randomNumber] as AudioClip);
    }

}
