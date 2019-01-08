using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine;

public class LoaderManager : MonoBehaviour {

    /// <summary>
    /// Roda o script logo após o load da scene
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeManagers()
    {
        GameObject soundManager, videoManager, animManager, scoreManager, silabaControl;

        soundManager = Resources.Load("Prefabs/SoundManager") as GameObject;
        videoManager = Resources.Load("Prefabs/VideoManager") as GameObject;
        animManager = Resources.Load("Prefabs/AnimationManager") as GameObject;
        scoreManager = Resources.Load("Prefabs/ScoreManager") as GameObject;
        silabaControl = Resources.Load("Prefabs/SilabaControl") as GameObject;

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
