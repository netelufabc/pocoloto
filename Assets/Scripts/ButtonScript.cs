using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    Button currentButton;
    AudioSource click;

    void Start () {
        currentButton = GetComponent<Button>();
        click = GetComponent<AudioSource>();
	}

	void Update () {
		
	}

    public void buttonPressed2()//função chamada ao apertar o botão nivel 1
    {
        click.Play(0);//toca som de apertando o botao (click_tecla01)
        if (!(LevelController.SilabaDigitada.Length >= LevelController.CharLimitForLevel))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
        {
            LevelController.SilabaDigitada = string.Concat(LevelController.SilabaDigitada, currentButton.name);          
        }
        if (LevelController.SilabaDigitada.Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
        {
            LevelController.BotaoConfirmaResposta = true;
        }

    }

    public void buttonPressed()
    {
        if (1 == LevelController.silabasDigitadas.Length)
        {
            buttonLevel01();
        }
        else if (2 == LevelController.silabasDigitadas.Length)
        {
            buttonLevel02();
        }
        else if (3 == LevelController.silabasDigitadas.Length)
        {
            buttonLevel03();
        }
        else if (4 == LevelController.silabasDigitadas.Length)
        {
            buttonLevel04();
        }
    }

    public void buttonLevel01()
    {
        click.Play(0);//toca som de apertando o botao (click_tecla01)
        if (LevelController.silabasDigitadas[0] == null || !(LevelController.silabasDigitadas[0].Length > 1))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
        {
            LevelController.silabasDigitadas[0] = string.Concat(LevelController.silabasDigitadas[0], currentButton.name);
        }              
        if (LevelController.silabasDigitadas[0].Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
        {
            LevelController.BotaoConfirmaResposta = true;
        }
    }

    public void buttonLevel02()
    {
        click.Play(0);//toca som de apertando o botao (click_tecla01)
        if (LevelController.silabasDigitadas[0] == null || !(LevelController.silabasDigitadas[0].Length > 1))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
        {
            LevelController.silabasDigitadas[0] = string.Concat(LevelController.silabasDigitadas[0], currentButton.name);
        }
        else if (LevelController.silabasDigitadas[0].Length > 1 && (LevelController.silabasDigitadas[1] == null || LevelController.silabasDigitadas[1].Length < 2))
        {
            LevelController.silabasDigitadas[1] = string.Concat(LevelController.silabasDigitadas[1], currentButton.name);
        }
        string temp = string.Concat(LevelController.silabasDigitadas[0], LevelController.silabasDigitadas[1]);
        if (temp.Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
        {
            LevelController.BotaoConfirmaResposta = true;
        }
    }

    public void buttonLevel03()
    {
        click.Play(0);//toca som de apertando o botao (click_tecla01)
        if (LevelController.silabasDigitadas[0] == null || !(LevelController.silabasDigitadas[0].Length > 1))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
        {
            LevelController.silabasDigitadas[0] = string.Concat(LevelController.silabasDigitadas[0], currentButton.name);
        }
        else if (LevelController.silabasDigitadas[0].Length > 1 && (LevelController.silabasDigitadas[1] == null || LevelController.silabasDigitadas[1].Length < 2))
        {
            LevelController.silabasDigitadas[1] = string.Concat(LevelController.silabasDigitadas[1], currentButton.name);
        }
        else if (LevelController.silabasDigitadas[1].Length > 1 && (LevelController.silabasDigitadas[2] == null || LevelController.silabasDigitadas[2].Length < 2))
        {
            LevelController.silabasDigitadas[2] = string.Concat(LevelController.silabasDigitadas[2], currentButton.name);
        }
        string temp = string.Concat(LevelController.silabasDigitadas[0], LevelController.silabasDigitadas[1], LevelController.silabasDigitadas[2]);
        if (temp.Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
        {
            LevelController.BotaoConfirmaResposta = true;
        }
    }

    public void buttonLevel04()
    {
        click.Play(0);//toca som de apertando o botao (click_tecla01)
        if (LevelController.silabasDigitadas[0] == null || !(LevelController.silabasDigitadas[0].Length > 1))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
        {
            LevelController.silabasDigitadas[0] = string.Concat(LevelController.silabasDigitadas[0], currentButton.name);
        }
        else if (LevelController.silabasDigitadas[0].Length > 1 && (LevelController.silabasDigitadas[1] == null || LevelController.silabasDigitadas[1].Length < 2))
        {
            LevelController.silabasDigitadas[1] = string.Concat(LevelController.silabasDigitadas[1], currentButton.name);
        }
        else if (LevelController.silabasDigitadas[1].Length > 1 && (LevelController.silabasDigitadas[2] == null || LevelController.silabasDigitadas[2].Length < 2))
        {
            LevelController.silabasDigitadas[2] = string.Concat(LevelController.silabasDigitadas[2], currentButton.name);
        }
        else if (LevelController.silabasDigitadas[2].Length > 1 && (LevelController.silabasDigitadas[3] == null || LevelController.silabasDigitadas[3].Length < 2))
        {
            LevelController.silabasDigitadas[3] = string.Concat(LevelController.silabasDigitadas[3], currentButton.name);
        }
        string temp = string.Concat(LevelController.silabasDigitadas[0], LevelController.silabasDigitadas[1], LevelController.silabasDigitadas[2], LevelController.silabasDigitadas[3]);
        if (temp.Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
        {
            LevelController.BotaoConfirmaResposta = true;
        }
    }

}
