using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    GameObject soundChecker; //Basicamente um loader para a primeira tela

    SoundManager soundManager;
    AnimationManager animManager;

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    public void PlayClick(AudioClip click)
    {
        soundManager = SoundManager.instance;
        soundManager.PlaySfx(click);
    }
    /*
   public void ChooseScene(string sceneName)
   {
       SceneManager.LoadScene(sceneName);
   }
   */

    IEnumerator EsperaPocoloto()
    {
        Animator anim;
        anim = gameObject.GetComponent<Animator>();
        yield return new WaitForSeconds(3);
        anim.SetBool("pocolotoNaArea", true);
    }

    /// <summary>
    /// Função para carregar uma nova fase com fade - em construção
    /// </summary>
    /// <param name="Scene"></param>
    public void LoadSceneWithFade(string scene)
    {
        animManager = GameObject.FindGameObjectWithTag("AnimationManager").GetComponent<AnimationManager>();
        soundManager.StopSilaba();
        StartCoroutine(animManager.Fade(scene));
    }
}
