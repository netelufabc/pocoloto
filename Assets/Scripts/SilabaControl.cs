using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilabaControl : MonoBehaviour {

    private SoundManager soundManager;
    private static SilabaControl single_instance;
    private Object[] silabasNivelAtual;
    private string soundsDirectory;

    private int randomNumber;

    private void Awake()
    {
        soundManager = SoundManager.GetInstance();
    }

   /* IEnumerator CallSilaba(float seconds)//espera seconds e chama tocar silaba
    {
        yield return new WaitForSeconds(seconds);
        TocarSilaba();
        BotaoDicaAudio.interactable = true;//ativa botoes de ajuda após tocar nova silaba
        BotaoDicaVisual.interactable = true;//ativa botoes de ajuda após tocar nova silaba
    }*/


    public void SilabaSetup (string soundsDirectory)
    {
        this.soundsDirectory = soundsDirectory;
        silabasNivelAtual = Resources.LoadAll(soundsDirectory, typeof(AudioClip));//carrega todos áudios dentro de Resources/Sounds/Level_01       
    }

    public void TocarSilaba()//escolhe e toca uma sílaba aleatória (random nos arquivos de áudio)
    {
        randomNumber = Random.Range(0, silabasNivelAtual.Length);
        LevelController.PalavraSelecionada = silabasNivelAtual[randomNumber].name.ToUpper();//pega a sílaba (nome do arquivo sem a extensão) aleatóriamente        
        LevelController.SeparaSilabas();
        soundManager.PlaySilaba(silabasNivelAtual[randomNumber] as AudioClip);
    }

}
