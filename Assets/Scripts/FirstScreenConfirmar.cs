using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScreenConfirmar : LoadScene {

    private void Start()
    {
        StartCoroutine(EsperaPocoloto());
        soundManager = SoundManager.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown("enter"))
        {
            LoadSceneWithFade("02_mainMenu");
        }
    }

    IEnumerator EsperaPocoloto()
    {
        Animator anim;
        anim = gameObject.GetComponent<Animator>();
        yield return new WaitForSeconds(3);
        anim.SetBool("pocolotoNaArea", true);
    }
}
