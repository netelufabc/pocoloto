using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    AudioSource click;

    public void PlayClick()
    {
        click = GetComponent<AudioSource>();
        click.Play();
    }

    public void ChooseScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
