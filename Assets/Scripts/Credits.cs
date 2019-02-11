using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

    SoundManager soundManager;
    AudioClip creditsSong;
    bool isFading;

	void Start () {
        creditsSong = Resources.Load("Sounds/Music/random silly chip song") as AudioClip;
        soundManager = SoundManager.instance;
        soundManager.PlayBackground(creditsSong);
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isFading)
            {
                gameObject.AddComponent<LoadScene>();
                gameObject.GetComponent<LoadScene>().LoadSceneWithFade("02_mainMenu");
                isFading = true;
            }
        }
	}
}
