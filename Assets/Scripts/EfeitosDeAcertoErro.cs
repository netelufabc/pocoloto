using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosDeAcertoErro : MonoBehaviour {

    public AudioClip soundEfx;
    private SoundManager soundManager;

    void Start () {
        soundManager = SoundManager.instance;
        StartCoroutine(PlayEfx());
    }

    private IEnumerator PlayEfx()
    {
        soundManager.PlaySfx(soundEfx);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
