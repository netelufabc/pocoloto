using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine;

public class PlayWithManagersLoaded : MonoBehaviour {

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeManagers()
    {
        GameObject soundManager, AnimManager;
        soundManager = Resources.Load("Prefabs/SoundManager") as GameObject;
        AnimManager = Resources.Load("Prefabs/AnimationManager") as GameObject;
        Instantiate(soundManager);
        Instantiate(AnimManager);
    }
}
