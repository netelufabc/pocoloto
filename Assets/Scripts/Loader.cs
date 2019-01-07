using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    private GameObject score, soundManager, timer, silabaControl, buttonConfirmar, animationController, dicaAudio, dicaVisual;

    private void Awake()
    {
        #region Colocados no LoaderManager
        //score = Resources.Load("Prefabs/ScoreManager") as GameObject;
        //soundManager = Resources.Load("Prefabs/SoundManager") as GameObject;
        //silabaControl = Resources.Load("Prefabs/SilabaControl") as GameObject;
        //animationController = Resources.Load("Prefabs/AnimationManager") as GameObject;
        #endregion

        timer = Resources.Load("Prefabs/Timer") as GameObject;
        dicaAudio = Resources.Load("Prefabs/Button Sound") as GameObject;
        dicaVisual = Resources.Load("Prefabs/Button Eye") as GameObject;
        buttonConfirmar = Resources.Load("Prefabs/Button Confirma Resposta") as GameObject;

        #region Colocados no LoaderManager
        /*if (Score.instance == null)
        {
            Instantiate(score);
        }

        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }*/

        /*if (SilabaControl.instance == null)
        {
            Instantiate(silabaControl);
        }*/

        /*if (AnimationManager.instance == null)
        {
            GameObject newAnimationController = Instantiate(animationController) as GameObject;
        }*/
        #endregion

        if (Timer.instance == null)
        {
            GameObject newTimer = Instantiate(timer) as GameObject;
            newTimer.transform.SetParent(GameObject.FindGameObjectWithTag("Timer").transform, false);
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

        GameObject newButtonConfirmar = Instantiate(buttonConfirmar) as GameObject;
        newButtonConfirmar.transform.SetParent(GameObject.FindGameObjectWithTag("ButtonTips").transform, false);
    }
}
