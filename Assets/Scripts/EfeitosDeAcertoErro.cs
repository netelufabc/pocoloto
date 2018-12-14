using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Código para colocar nas mensagens de acerto e erro, pois ela destrói o gameObject no final
/// </summary>
public class EfeitosDeAcertoErro : MonoBehaviour {

    public AudioClip soundEfx;
    public bool efeitoRapido;
    private SoundManager soundManager;


    void Start () {
        soundManager = SoundManager.instance;
        StartCoroutine(PlayEfx());
    }

    private IEnumerator PlayEfx()
    {
        soundManager.PlaySfx(soundEfx);
        if (efeitoRapido)
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}
