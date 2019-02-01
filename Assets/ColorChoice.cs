using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ColorChoice : MonoBehaviour {

    public GameObject itensInfoList;
    private List<int> indiceLiberados;
    public GameObject button;
    static public ColorBlock defaultColor;
    static public ColorBlock highlightedColor;

    public void Awake()
    {
        indiceLiberados = new List<int>();

        PegarCorDefaultButton();

        for (int i = 0; i<SaveManager.player.cores.Length; i++)
        {
            if (SaveManager.player.cores[i])
            {
                indiceLiberados.Add(i);
                Debug.Log(i);
            }
        }

        foreach (int n in indiceLiberados)
        {
            Debug.Log(n);
            Create(n);
        }
    }

    private void PegarCorDefaultButton()
    {
        defaultColor = button.GetComponent<Button>().colors;
        ColorBlock cb = defaultColor;
        cb.normalColor = defaultColor.highlightedColor;
        highlightedColor = cb;
    }

    public static void Create(int index)
    {
        GameObject itensInfoList = Resources.Load("Prefabs/ItensInfoList") as GameObject;
        GameObject color = Resources.Load("Prefabs/CustomizeScreen/ColorButton") as GameObject;
        GameObject newColor = Instantiate(color, GameObject.Find("ColorPanel").transform);
        newColor.GetComponent<ColorButtonChoice>().index = index;
        newColor.GetComponent<ColorButtonChoice>().nome = itensInfoList.GetComponent<ItensInfoList>().avatarsToSell[index].itemInfo;

        if (index == SaveManager.player.colorSelecionadoIndex)
        {
            newColor.GetComponent<Button>().colors = highlightedColor;
        }

        newColor = newColor.transform.GetChild(0).gameObject;
        newColor.GetComponent<Image>().sprite = itensInfoList.GetComponent<ItensInfoList>().colorsToSell[index].itemSprite;
        newColor.GetComponent<Image>().color = itensInfoList.GetComponent<ItensInfoList>().colorsToSell[index].itemColor;

    }
}
