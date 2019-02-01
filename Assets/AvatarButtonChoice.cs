using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AvatarButtonChoice : MonoBehaviour {

    public int index;
    public string nome;

    public void OnClick()
    {
        foreach (Button button in Selectable.allSelectables)
        {
            if (button.CompareTag("Shopitem"))
            {
                button.colors = button.colors;
            }
        }

        SaveManager.player.avatarSelecionadoIndex = index;
        GameObject.Find("Main Camera").GetComponent<CustomizeScreen>().UpdateScreen();
    }
}
