using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Blinker : MonoBehaviour {


    //inicio pisca Imagem(UI) por duration à taxa de blinkTime
    public static IEnumerator DoBlinks(Image resposta, float duration, float blinkTime, Image[] RespostaCerta, Image[] RespostaErrada)
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

    public static void ToggleState(Image resposta)//muda o estado ligado/desligado de uma imagem (UI)
    {
        resposta.enabled = !resposta.enabled;
    }

    //inicio pisca GameObject por duration à taxa de blinkTime, toca áudio e espera tempo antes caso necessário
    public static IEnumerator DoBlinksGameObject(AudioClip audioclip, float secondsBeforeBlink, GameObject GameObjectToBlink, float duration, float blinkTime, AudioSource audioFile, GameObject LevelClearMsg)
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

    public static void ToggleStateGameObject(GameObject resposta)//muda o estado ligado/desligado de algum GameObject
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
