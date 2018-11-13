using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public static Timer instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator SetTimeIsRunning(AudioClip silaba)//função para set a variavel de contar tempo e barra após a fala da palavra
    {
        yield return new WaitForSeconds(silaba.length);
        LevelController.TimeIsRunning = true;
    }
}
