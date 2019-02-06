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

    public void TakeVideo(VideoClip video)
    {
        videoPlayer.clip = video;
    }

    public float VideoLength()
    {
        return (float)videoPlayer.clip.length;
    }

    public IEnumerator PlayVideo(RawImage rawImage)
    {

        WaitForSeconds waitForSeconds = new WaitForSeconds(2);
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        soundManager.ChangeVolumeAudioBackground(0.5f);
        videoPlayer.loopPointReached += VolumeBackToNormal;
        rawImage.enabled = true;
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
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
        soundManager.ChangeVolumeAudioBackground(0.5f);
        videoPlayer.loopPointReached += VolumeBackToNormal;
        rawImage.enabled = true;
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
        soundManager.ChangeVolumeAudioBackground(0.5f);
        videoPlayer.loopPointReached += VolumeBackToNormal;
        rawImage.enabled = true;
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        StartCoroutine(soundManager.PlaySilaba(audio, pitch));
    }

    void VolumeBackToNormal(VideoPlayer vp)
    {
        soundManager.ChangeVolumeAudioBackground(1);
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        soundManager.ChangeVolumeAudioBackground(1);
        soundManager.StopSilaba();
    }
}
