using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

    SoundManager soundManager;
    AudioClip creditsSong;
    bool isFading;

	// Use this for initialization
	void Start () {
        creditsSong = Resources.Load("Sounds/Music/random silly chip song") as AudioClip;
        soundManager = SoundManager.instance;
        soundManager.PlayBackground(creditsSong);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
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
