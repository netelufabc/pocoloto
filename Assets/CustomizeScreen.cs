using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CustomizeScreen : MonoBehaviour {

    Text nameText;
    Text starsText;
    Text moneyText;
    Image avatarImage; //Ou como é guardado o avatar

    private void Awake()
    {
        nameText = GameObject.Find("Nome").GetComponent<Text>();
        ChangeToName();
        starsText = GameObject.Find("EstrelaText").GetComponent<Text>();
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        avatarImage = GameObject.Find("Avatar").GetComponent<Image>(); //Pode mudar;
        UpdateScreen();
    }

    public void UpdateScreen()
    {
        UpdateMoney();
        UpdateStars();
        UpdateAvatar();
    }

    private void ChangeToName()
    {
        nameText.text = SaveManager.player.nome;
    }

    private void UpdateStars()
    {
        starsText.text = SaveManager.player.totalEstrelas.ToString();
    }

    private void UpdateMoney()
    {
        moneyText.text = SaveManager.player.dinheiro.ToString();
    }

    private void UpdateAvatar()
    {
        Sprite avatar = Resources.Load<Image>("Prefabs/Avatar/Avatar0" + (SaveManager.player.avatarSelecionadoIndex + 1).ToString()).sprite;
        avatarImage.sprite = avatar;
    }

}
