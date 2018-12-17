using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    GameObject soundChecker; //Basicamente um loader para a primeira tela

    SoundManager soundManager;

    private void Awake()
    {
        soundChecker = Resources.Load("Prefabs/SoundManager") as GameObject;
        if (SoundManager.instance == null)
        {
            soundChecker = Instantiate(soundChecker);
        }
    }

    public void PlayClick(AudioClip click)
    {
        soundManager = SoundManager.instance;
        soundManager.PlaySfx(click);
    }

    public void ChooseScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator EsperaPocoloto()
    {
        Animator anim;
        anim = gameObject.GetComponent<Animator>();
        yield return new WaitForSeconds(3);
        anim.SetBool("pocolotoNaArea", true);
    }
}
