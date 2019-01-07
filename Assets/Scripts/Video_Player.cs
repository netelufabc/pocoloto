using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video_Player : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    private SoundManager soundManager;
    public AudioClip explicacao;
    private Button botaoComecar;

    void Start()
    {
        botaoComecar = GameObject.Find("Button - Comecar").GetComponent<UnityEngine.UI.Button>();
        //botaoComecar.interactable = false;
        rawImage.enabled = false;
        soundManager = SoundManager.instance;
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(2);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.enabled = true;
        rawImage.texture = videoPlayer.texture;
        soundManager.ChangePitch(1.13f);
        soundManager.PlaySilaba(explicacao);
        videoPlayer.Play();
        yield return new WaitForSeconds(explicacao.length - explicacao.length * 0.13f);
        soundManager.ChangePitch(1);
        botaoComecar.interactable = true;
        rawImage.enabled = false;
    }
}