using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EstrelaSistema : MonoBehaviour {

    public int numberSystem;

    Text numberSystemText;

    private void Awake()
    {
        numberSystemText = gameObject.GetComponentInChildren<Text>();
        numberSystemText.text = string.Concat(SaveManager.player.estrelaSistema[numberSystem].ToString(),"/",LevelController.estrelaSistemaTotal[numberSystem].ToString());
    }
}

