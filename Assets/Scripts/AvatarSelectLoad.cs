using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class AvatarSelectLoad : MonoBehaviour {

    public VideoClip video;

    public void LoadNextScene(string scene)
    {
        gameObject.GetComponent<LoadScene>().LoadSceneWithFade(video, scene);
    }

}
