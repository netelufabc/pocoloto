using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToggleButton : MonoBehaviour {

    Button button;
    public static ColorBlock defaulColor;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(BlankAllTheOthers);
        defaulColor = button.colors;
    }

    public void BlankAllTheOthers()
    {
        foreach (Button button in Button.allSelectables)
        {
            if (button.CompareTag("Button"))
            {
                button.colors = defaulColor;
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
