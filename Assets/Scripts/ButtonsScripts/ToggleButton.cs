using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToggleButton : MonoBehaviour {

    Button button;
    public static ColorBlock defaultColor;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(BlankAllTheOthers);
        defaultColor = button.colors;
    }

    public void BlankAllTheOthers()
    {
        foreach (Selectable button in Selectable.allSelectables)
        {
            if (button.CompareTag("Button"))
            {
                button.colors = defaultColor;
            }
        }
        ChangeColor();
    }

    void ChangeColor()
    {
        ColorBlock cb = button.colors;
        cb.normalColor = button.colors.highlightedColor; ///Highlight e variaveis
        button.colors = cb;
    }
}
