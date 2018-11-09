using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteOnClickGirl : MonoBehaviour {

    public Sprite sprite1girl, sprite2girl;
    SpriteRenderer SpriteAtualGirl;

    void Start () {
        SpriteAtualGirl = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(VoceEhScreen.GirlSelected == false)
        {            
            if (VoceEhScreen.BoySelected == true && VoceEhScreen.GirlSelected == false)
            {
                SpriteAtualGirl.sprite = sprite1girl;               
            }       
        }       
    }

    void OnMouseUpAsButton()
    {

        if (SpriteAtualGirl.name == "botao_selecao_base_menina" && VoceEhScreen.GirlSelected == false)
        {
            VoceEhScreen.GirlSelected = true;          
            SpriteAtualGirl.sprite = sprite2girl;
            VoceEhScreen.BoySelected = false;
        }
    }

    
    private void OnMouseOver()
    {
        if (VoceEhScreen.GirlSelected == false)
        SpriteAtualGirl.color = Color.yellow;
    }

    private void OnMouseExit()
    {
            SpriteAtualGirl.color = Color.white;
    }
}
