using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoaderManager : MonoBehaviour {

    /// <summary>
    /// Roda o script logo após o load da scene
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeManagers()
    {
        GameObject soundManager, videoManager, animManager, scoreManager, silabaControl;

        soundManager = Resources.Load("Prefabs/Managers/SoundManager") as GameObject;
        videoManager = Resources.Load("Prefabs/Managers/VideoManager") as GameObject;
        animManager = Resources.Load("Prefabs/Managers/AnimationManager") as GameObject;
        scoreManager = Resources.Load("Prefabs/Managers/ScoreManager") as GameObject;
        silabaControl = Resources.Load("Prefabs/Managers/SilabaControl") as GameObject;

        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }

        if (VideoManager.instance == null)
        {
            Instantiate(videoManager);
        }

        if (AnimationManager.instance == null)
        {
            Instantiate(animManager);
        }

        if (Score.instance == null)
        {
            Instantiate(scoreManager);
        }

        if (SilabaControl.instance == null)
        {
            Instantiate(silabaControl);
        }
    }
}
