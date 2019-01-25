using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNextSystem : MonoBehaviour {

    private bool sistemasLiberados;
    //private int systemNumber;
    //private string systemName;

    // Libera o botão de NEXT no stageSelect
    void Start () {
        sistemasLiberados = SaveManager.player.sistemaLiberado;
        //systemName = SceneManager.GetActiveScene().name;
        //systemNumber = System.Int32.Parse(systemName.Substring(systemName.Length - 1));

        if (sistemasLiberados)
        {
            this.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
    }
}
