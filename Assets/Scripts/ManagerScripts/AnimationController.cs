using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public static AnimationController control = null;
    public AudioClip audio;

    private Animator animator;
    private GameObject animForward, animBackward, fade, animation;

    /// <summary>
    /// Garante a unicidade do manager e inicia as animações
    /// </summary>
    public void Awake()
    {
        if (control == null)
        {
            control = this;
        }
        else if (this != control)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        animForward = Resources.Load("Prefabs/HyperspaceForward") as GameObject;
        animBackward = Resources.Load("Prefabs/HyperspaceBackwards") as GameObject;
        fade = Resources.Load("Prefabs/Fade") as GameObject;

        animForward = Instantiate(animForward);
        animBackward = Instantiate(animBackward);
        fade = Instantiate(fade);
        animForward.transform.SetParent(GameObject.FindGameObjectWithTag("AnimationManager").transform, false);
        animBackward.transform.SetParent(GameObject.FindGameObjectWithTag("AnimationManager").transform, false);
        fade.transform.SetParent(GameObject.FindGameObjectWithTag("AnimationManager").transform, false);
    }

    /// <summary>
    /// Decide qual animação tocar a partir do parametro levelComplete
    /// True - toca a animação de avanço
    /// False - Toca a animação de retorno
    /// </summary>
    /// <param name="levelComplete"></param>
    public void PlayTransitionSceneAnimation(bool levelComplete)
    {
        if (levelComplete)
        {
            animation = animForward;
        }
        else
        {
            animation = animBackward;
        }
        animator = animation.GetComponent<Animator>();
        SoundManager.instance.PlaySfx(audio);
        animator.SetTrigger("FadeOut");
    }

    /// <summary>
    /// Toca animação simples de FadeIn e FadeOut 
    /// </summary>
    public void PlaySimpleTrasitionAnimation()
    {
        fade.GetComponent<Animator>().SetTrigger("FadeStart");
    }
}
