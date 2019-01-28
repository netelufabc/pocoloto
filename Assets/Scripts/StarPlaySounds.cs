using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPlaySounds : MonoBehaviour {

    AudioClip firstAndSecondStars;
    AudioClip thirdStar;
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.instance;
        firstAndSecondStars = (AudioClip)Resources.Load("Sounds/sfx/estrela1");
        thirdStar = (AudioClip)Resources.Load("Sounds/sfx/estrela2");
    }

    public void PlayOneAndTwoStarsSound()
    {
        soundManager.PlaySfx(firstAndSecondStars);
    }

    public void PlayThreeStarsSound()
    {
        soundManager.PlaySfx(thirdStar);
    }

}
