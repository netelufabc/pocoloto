using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    private GameObject score, soundManager, timer, silabaControl, blinker, dicaAudio, dicaVisual;

    private void Awake()
    {

        score = Resources.Load("Prefabs/ScoreManager") as GameObject;
        soundManager = Resources.Load("Prefabs/SoundManager") as GameObject;
        timer = Resources.Load("Prefabs/Timer") as GameObject;
        silabaControl = Resources.Load("Prefabs/SilabaControl") as GameObject;
        blinker = Resources.Load("Prefabs/Blinker") as GameObject;
        dicaAudio = Resources.Load("Prefabs/Button Sound") as GameObject;
        dicaVisual = Resources.Load("Prefabs/Button Eye") as GameObject;

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
            GameObject newTimer = Instantiate(timer) as GameObject;
            newTimer.transform.SetParent(GameObject.FindGameObjectWithTag("Timer").transform, false);
        }

        if (SilabaControl.instance == null)
        {
            Instantiate(silabaControl);
        }

        if(Blinker.instance == null)
        {
            Instantiate(blinker);
        }

        if (ButtonDicaAudio.instance == null)
        {
            GameObject newDicaAudio = Instantiate(dicaAudio) as GameObject;
            newDicaAudio.transform.SetParent(GameObject.FindGameObjectWithTag("ButtonTips").transform, false);
        }

        if (ButtonDicaVisual.instance == null)
        {
            GameObject newDicaVisual = Instantiate(dicaVisual) as GameObject;
            newDicaVisual.transform.SetParent(GameObject.FindGameObjectWithTag("ButtonTips").transform, false);
        }

    }
}
