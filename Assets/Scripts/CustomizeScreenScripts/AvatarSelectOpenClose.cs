using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelectOpenClose : MonoBehaviour {

	public void CloseFather()
    {
        Destroy(this.transform.parent.gameObject);
    }

    public void OpenAvatarSelect()
    {
        GameObject avatarSelectScreen;
        avatarSelectScreen = Resources.Load("Prefabs/CustomizeScreen/AvatarChoice") as GameObject;
        Instantiate(avatarSelectScreen, GameObject.Find("Canvas").transform);
    }
}
