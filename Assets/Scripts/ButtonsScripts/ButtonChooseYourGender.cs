using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonChooseYourGender : MonoBehaviour {

    GameObject girl;
    GameObject boy;
    Button buttonBoy;
    Button buttonGirl;
    Button confirmar;

    private void Awake()
    {
        boy = GameObject.Find("Menino");
        girl = GameObject.Find("Menina");
        buttonBoy = boy.GetComponent<Button>();
        buttonGirl = girl.GetComponent<Button>();
        confirmar = GameObject.Find("Button - Confirma").GetComponent<Button>();
    }

    private void Update()
    {
        if ((buttonBoy.interactable && buttonGirl.interactable)||GameObject.Find("NomeText").GetComponent<Text>().text == "" || GameObject.Find("IdadeText").GetComponent<Text>().text == "" || GameObject.Find("SerieText").GetComponent<Text>().text =="")
        {
            confirmar.interactable = false;
        }
        else
        {
            confirmar.interactable = true;
        }
    }

    /// <summary>
    /// Seleciona o sexo do player, muda a imagem do menino, e caso precise, da menina.
    /// </summary>
    public void ClickOnBoy()
    {
        SaveManager.player.genero = true;

        if (buttonBoy.IsInteractable())
        {
            ChangeImage(boy, "Images/botao_selecao_click_menino");
            buttonBoy.interactable = false;

            if (!buttonGirl.IsInteractable())
            {
                ChangeImage(girl, "Images/botao_selecao_base_menina");
                buttonGirl.interactable = true;
            }
        }


    }

    /// <summary>
    /// Seleciona o sexo do player, muda a imagem do menina, e caso precise, da menino.
    /// </summary>
    public void ClickOnGirl()
    {
        SaveManager.player.genero = false;

        if (buttonGirl.IsInteractable())
        {
            ChangeImage(girl, "Images/botao_selecao_click_menina");
            buttonGirl.interactable = false;

            if (!buttonBoy.IsInteractable())
            {
                ChangeImage(boy, "Images/botao_selecao_base_menino");
                buttonBoy.interactable = true;
            }
        }


    }

    /// <summary>
    /// Troca a imagem do objeto recebido
    /// </summary>
    /// <param name="kid">Objeto a ter imagem trocada</param>
    /// <param name="path">Caminho da imagem na pasta Resources</param>
    private void ChangeImage(GameObject kid, string path)
    {
        Sprite newKidImage = Resources.Load<Sprite>(path);
        kid.GetComponent<Image>().sprite = newKidImage;
    }
}
