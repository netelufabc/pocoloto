using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    [SerializeField]
    private AudioSource audioFala;
    [SerializeField]
    private AudioSource audioTimer;

    Timer timer;

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

        timer = Timer.instance;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Toca a silaba e depois espera pelo tempo que a silaba dura antes de retornar a função.
    /// </summary>
    /// <param name="currentSilaba">clip a ser tocado</param>
    public void PlaySilaba(AudioClip currentSilaba)
    {
        audioFala.clip = currentSilaba;
        audioFala.Play();
        StartCoroutine(WaitForSound(currentSilaba.length));
        Debug.Log("Tentou entrar no SetTime");
        //timer.SetTimeIsRunning(currentSilaba);
    }

    /// <summary>
    /// Toca todas as silabas que estão no vetor em sequência
    /// </summary>
    /// <param name="currentSilaba">Vetor de AudioClip das silabas</param>
    public void PlaySilaba(AudioClip [] currentSilaba)
    {
        for (int i = 0; currentSilaba.Length>i; i++)
        {
            audioFala.clip = currentSilaba[i];
            audioFala.Play();
            StartCoroutine(WaitForSound(currentSilaba[i].length));
        }
    }

    private IEnumerator WaitForSound(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

}