using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject score, soundManager, timer, silabaControl;

    private void Awake()
    {
        if (Score.instance == null)
        {
            Instantiate(score);

        }

        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }

        if (Timer.instance == null)
        {
            Instantiate(timer);
        }

        if (SilabaControl.instance == null)
        {
            Instantiate(silabaControl);
        }
    }
}
