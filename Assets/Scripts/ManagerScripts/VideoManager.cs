using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine;

public class VideoManager : MonoBehaviour {

    public static VideoManager instance = null;
    public VideoPlayer videoPlayer;
    private SoundManager soundManager;

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

    public IEnumerator PlayVideo(VideoClip video, AudioClip audio, RawImage rawImage)
    {
        videoPlayer.clip = video;
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(2);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        soundManager.PlaySilaba(audio);
    }

    public IEnumerator PlayVideo(VideoClip video, AudioClip audio, RawImage rawImage, float pitch)
    {
        videoPlayer.clip = video;
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(2);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.enabled = true;
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        StartCoroutine(soundManager.PlaySilaba(audio, pitch));
    }


    public void StopVideo()
    {
        videoPlayer.Stop();
        
        soundManager.StopSilaba();
    }
}
