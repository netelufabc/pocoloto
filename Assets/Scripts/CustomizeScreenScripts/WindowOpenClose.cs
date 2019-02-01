using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpenClose : MonoBehaviour {

    public GameObject window;
    public bool save;

	public void CloseFather()
    {
        SaveGame();
        Destroy(this.transform.parent.gameObject);
    }

    public void OpenWindow()
    {
        Instantiate(window, GameObject.Find("Canvas").transform);
    }

    public void SaveGame()
    {
        if (save)
        {
            SaveManager.Save();
        }
    }
}
