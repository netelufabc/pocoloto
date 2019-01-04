using System.Collections;
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

    /// <summary>
    /// Toca música de fundo
    /// </summary>
    /// <param name="background"></param>
    public void PlayBackground(AudioClip background)
    {
        if (!audioBackground.isPlaying) { 
            audioBackground.clip = background;
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

}