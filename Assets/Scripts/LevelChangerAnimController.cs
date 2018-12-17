using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangerAnimController : MonoBehaviour {

    public Animator forwardAnimation;
    public Animator backwardAnimation;
    public static LevelChangerAnimController instance = null;

    private Animator animator;
    private GameObject animForward, animBackward, animation;
    private bool goToNextLevel;

    /// <summary>
    /// Garante a unicidade do manager e inicia as animações
    /// </summary>
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

        animForward = Resources.Load("Prefabs/HyperspaceForward") as GameObject;
        animBackward = Resources.Load("Prefabs/HyperspaceBackwards") as GameObject;

        animForward = Instantiate(animForward);
        animBackward = Instantiate(animBackward);
        animForward.transform.SetParent(GameObject.FindGameObjectWithTag("AnimationManager").transform, false);
        animBackward.transform.SetParent(GameObject.FindGameObjectWithTag("AnimationManager").transform, false);
    }

    /// <summary>
    /// Decide qual animação tocar a partir do parametro levelComplete
    /// True - toca a animação de avanço
    /// False - Toca a animação de retorno
    /// </summary>
    /// <param name="levelComplete"></param>
    public void PlayTransitionSceneAnimation(bool levelComplete)
    {
        goToNextLevel = levelComplete;

        if (levelComplete)
        {
            animation = animForward;
        }
        else
        {
            animation = animBackward;
        }
        animator = animation.GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
    }
}
