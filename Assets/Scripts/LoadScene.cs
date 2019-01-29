using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    GameObject menuOptions;
    protected SoundManager soundManager;
    AnimationManager animManager;

    private void Start()
    {
        menuOptions = Resources.Load("Prefabs/Sub Menus/MenuOptions") as GameObject;
        soundManager = SoundManager.instance;
    }

    //public void PlayClick(AudioClip click)
    //{
    //    soundManager.PlaySfx(click);
    //}
    /*
   public void ChooseScene(string sceneName)
   {
       SceneManager.LoadScene(sceneName);
   }
   */

    public void OpenOptionsMenu()//abre menu opções menu principal
    {
        if (GameObject.Find("MenuOptions(Clone)") == null)
        {
            GameObject newOptionsMenu;
            newOptionsMenu = Instantiate(menuOptions);
            newOptionsMenu.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
    }

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
        soundManager = SoundManager.instance;
        soundManager.SoundManagerOnTransition();
        StopAllCoroutines();
        animManager = GameObject.FindGameObjectWithTag("AnimationManager").GetComponent<AnimationManager>();
        StartCoroutine(animManager.Fade(scene));
    }
}
