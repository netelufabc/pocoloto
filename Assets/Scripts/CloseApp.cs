using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : MonoBehaviour {


    public void FecharAplicacao()
    {
        //Application.LoadLevel(sceneName);
        Application.Quit();
    }

    /// <summary>
    /// Utilizado para destruir o pai do botão de fechar
    /// </summary>
    public void CloseMenu()
    {
        GameObject buttonParent;
        buttonParent = this.transform.parent.gameObject;
        Destroy(buttonParent);
    }
}
