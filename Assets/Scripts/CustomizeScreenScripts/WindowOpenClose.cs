using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpenClose : MonoBehaviour {

    public GameObject window;

	public void CloseFather()
    {
        Destroy(this.transform.parent.gameObject);
    }

    public void OpenWindow()
    {
        Instantiate(window, GameObject.Find("Canvas").transform);
    }
}
