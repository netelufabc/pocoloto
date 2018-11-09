using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilabaControl : MonoBehaviour {

    private static SilabaControl single_instance;

    private int randomNumber;

    //Fazendo um AudioSource

    /*
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

    */
}
