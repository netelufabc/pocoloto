using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AvatarButtonChoiceMaker : MonoBehaviour {

    public static void Create(int index)
    {
        GameObject itensInfoList = Resources.Load("Prefabs/ItensInfoList") as GameObject;
        GameObject avatar = Resources.Load("Prefabs/CustomizeScreen/AvatarButton") as GameObject;
        GameObject newAvatar = Instantiate(avatar, GameObject.Find("AvatarPanel").transform);
        newAvatar.GetComponent<AvatarButtonChoice>().index = index;
        newAvatar.GetComponent<AvatarButtonChoice>().nome = itensInfoList.GetComponent<ItensInfoList>().avatarsToSell[index].itemInfo;
        //newAvatar.GetComponentInChildren<Image>().sprite = itensInfoList.GetComponent<ItensInfoList>().avatarsToSell[index].itemSprite;
        newAvatar = newAvatar.transform.GetChild(0).gameObject;
        newAvatar.GetComponent<Image>().sprite = itensInfoList.GetComponent<ItensInfoList>().avatarsToSell[index].itemSprite;
    } 
}
