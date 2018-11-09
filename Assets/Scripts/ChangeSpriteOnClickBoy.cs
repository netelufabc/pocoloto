using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteOnClickBoy : MonoBehaviour {

    public Sprite sprite1boy, sprite2boy;
    SpriteRenderer SpriteAtualBoy;

    void Start () {
        SpriteAtualBoy = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (VoceEhScreen.BoySelected == false)
        {
            if (VoceEhScreen.GirlSelected == true && VoceEhScreen.BoySelected == false)
            {
                SpriteAtualBoy.sprite = sprite1boy;           
            }  
        }       
    }

    void OnMouseUpAsButton()
    {
        if (SpriteAtualBoy.name == "botao_selecao_base_menino" && VoceEhScreen.BoySelected == false)
        {
            VoceEhScreen.BoySelected = true;
            SpriteAtualBoy.sprite = sprite2boy;
            VoceEhScreen.GirlSelected = false;
        }

        //if (SpriteAtual.sprite.Equals(sprite1))
        //{
        //    SpriteAtual.sprite = sprite2;
        //}
        //else { 
        //    SpriteAtual.sprite = sprite1;
        //    }
    }

    private void OnMouseOver()
    {
        if (VoceEhScreen.BoySelected == false)
            SpriteAtualBoy.color = Color.yellow;
    }

    private void OnMouseExit()
    {
            SpriteAtualBoy.color = Color.white;
    }
}
