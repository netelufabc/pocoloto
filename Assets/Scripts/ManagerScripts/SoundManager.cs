﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    [SerializeField]
    private AudioSource audioFala;
    [SerializeField]
    private AudioSource audioBackground;
    [SerializeField]
    private AudioSource audioSfx;
    [SerializeField]
    private AudioSource audioSfxLoop;

    private bool gameSoundMuted = false;

    void Awake()
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

    public void PlaySfxLoop(AudioClip sfx)
    {
        audioSfxLoop.clip = sfx;
        audioSfxLoop.Play();
    }

    public void MuteSfxLoop()
    {
        audioSfxLoop.mute = true;
    }

    public void UnmuteSfxLoop()
    {
        audioSfxLoop.mute = false;
    }

    public void StopSfxLoop()
    {
        audioSfxLoop.Stop();
    }

    /// <summary>
    /// Toca música de fundo
    /// </summary>
    /// <param name="background"></param>
    public void PlayBackground(AudioClip background)
    {
        audioBackground.clip = background;
        
        ///Primeiro verifica se o áudio é diferente, caso seja, 
        ///a música será substituida independente de qualquer coisa, caso seja igual, 
        ///ele verifica se a música já não está tocando (para evitar cortes na música atual)
        if (background != audioBackground.clip && !gameSoundMuted)
        {
            audioBackground.Play();
        }

        else if (!audioBackground.isPlaying && !gameSoundMuted) { 
            audioBackground.Play();
        }
    }

    /// <summary>
    /// Para a música de fundo
    /// </summary>
    public void StopBackground()
    {
        if (audioBackground.isPlaying)
        {
            audioBackground.Stop();
        }
    }

    public void SetMusicOff()
    {
        if (audioBackground.isPlaying)
        {
            audioBackground.Stop();
            gameSoundMuted = true;
        }
    }

    public void SetMusicON(AudioClip background)
    {
        gameSoundMuted = false;
        PlayBackground(background);
    }

    public bool IsBackgroundPlaying()
    {
        if (audioBackground.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Toca um efeito
    /// </summary>
    /// <param name="sfx"></param>
    public void PlaySfx(AudioClip sfx)
    {
        audioSfx.clip = sfx;
        audioSfx.Play();
    }

    /// <summary>
    /// Toca a silaba e depois espera pelo tempo que a silaba dura antes de retornar a função.
    /// </summary>
    /// <param name="currentSilaba">clip a ser tocado</param>
    public void PlaySilaba(AudioClip currentSilaba)
    {
        audioFala.clip = currentSilaba;
        audioFala.Play();
    }

    public IEnumerator PlaySilaba(AudioClip currentSilaba, float pitch)
    {
        ChangePitch(pitch);
        audioFala.clip = currentSilaba;
        audioFala.Play();
        yield return new WaitForSeconds(currentSilaba.length - currentSilaba.length * (pitch-1f));
        ChangePitch(1f);
    }

    public void StopSilaba()
    {
        if (audioFala.isPlaying)
        {
            audioFala.Stop();
        }
    }

    public void ChangePitch(float value)
    {
        audioFala.pitch = value;
    }

    public void ChangeVolumeAudioBackground(float volume)
    {
        audioBackground.volume = volume;
    }
}

