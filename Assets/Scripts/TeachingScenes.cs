﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine;

public class TeachingScenes : MonoBehaviour {

    public RawImage rawImage;
    public VideoClip videoClip;
    public AudioClip explicacao;
    public Button button;
    public float pitch;
    private VideoManager videoManager;
    public static string nextScene;

    private void Start()
    {
        videoManager = VideoManager.instance;
        StartCoroutine(BlockButtonPlayVideo());
    }

    private IEnumerator BlockButtonPlayVideo()
    {
        rawImage.enabled = false;
        button.interactable = false;
        StartCoroutine(videoManager.PlayVideo(rawImage));
        yield return new WaitForSeconds(videoManager.VideoLength());
        rawImage.enabled = false;
        button.interactable = true;
    }

    private void OnDestroy()
    {
        videoManager.StopVideo();
    }
}
