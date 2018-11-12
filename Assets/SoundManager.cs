using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class SoundManager : MonoBehaviour {

    private static SoundManager instance = null;
    [SerializeField]
    private AudioSource audioEfx;
    [SerializeField]
    private AudioSource audioMusic;

    private SoundManager()
    {
    }

    public static SoundManager GetInstance()
    {
        if (instance == null)
        {
            instance = new SoundManager();
        }
        return instance;
    }

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

    public void PlaySilaba(AudioClip currentSilaba)
    {
        audioEfx.clip = currentSilaba;
        audioEfx.Play();
        StartCoroutine(WaitForSound(currentSilaba.length));
    }

    public void PlaySilaba(AudioClip [] currentSilaba)
    {
        for (int i = 0; currentSilaba.Length>i; i++)
        {
            audioEfx.clip = currentSilaba[i];
            audioEfx.Play();
            StartCoroutine(WaitForSound(currentSilaba[i].length));
        }
    }

    private IEnumerator WaitForSound(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

}