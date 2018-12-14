using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangerAnimController : MonoBehaviour {

    public Animator anim;
    public static LevelChangerAnimController instance = null;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayTransitionSceneAnimation()
    {
        anim.SetTrigger("FadeOut");
    }

    public void FadeOutEnd()
    {
        anim.SetTrigger("FadeIn");
    }
}
