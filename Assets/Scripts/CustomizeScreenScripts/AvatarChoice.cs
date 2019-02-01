using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AvatarChoice : MonoBehaviour {

    public GameObject itensInfoList;
    private List<int> indiceLiberados;
    public GameObject avatarButton;
    static public ColorBlock defaultColor;
    static public ColorBlock highlightedColor;

    private void Awake()
    {
        indiceLiberados = new List<int>();

        PegarCorDefaultButton();

        for (int i = 0; i<SaveManager.player.avatares.Length; i++)
        {
            if (SaveManager.player.avatares[i])
            {
                indiceLiberados.Add(i);
            }
        }

        foreach (int n in indiceLiberados)
        {
            Create(n);
        }

    }

    private void PegarCorDefaultButton()
    {
        defaultColor = avatarButton.GetComponent<Button>().colors;
        ColorBlock cb = defaultColor;
        cb.normalColor = defaultColor.highlightedColor;
        highlightedColor = cb;
    }

    public static void Create(int index)
    {
        GameObject itensInfoList = Resources.Load("Prefabs/ItensInfoList") as GameObject;
        GameObject avatar = Resources.Load("Prefabs/CustomizeScreen/AvatarButton") as GameObject;
        GameObject newAvatar = Instantiate(avatar, GameObject.Find("AvatarPanel").transform);
        newAvatar.GetComponent<AvatarButtonChoice>().index = index;
        newAvatar.GetComponent<AvatarButtonChoice>().nome = itensInfoList.GetComponent<ItensInfoList>().avatarsToSell[index].itemInfo;

        if (index == SaveManager.player.avatarSelecionadoIndex)
        {
            newAvatar.GetComponent<Button>().colors = highlightedColor;
        }

        newAvatar = newAvatar.transform.GetChild(0).gameObject;
        newAvatar.GetComponent<Image>().sprite = itensInfoList.GetComponent<ItensInfoList>().avatarsToSell[index].itemSprite;

    }

}
