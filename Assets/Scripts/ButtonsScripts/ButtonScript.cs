using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : CursorChange {

    Button currentButton;
    AudioSource click;
    StageManager stageManager;
    
    void Start () {
        currentButton = GetComponent<Button>();
        click = GetComponent<AudioSource>();
        stageManager = StageManager.instance;
	}

	void Update () {
		
	}

    //public void buttonPressed2()//função chamada ao apertar o botão nivel 1
    //{
    //    click.Play(0);//toca som de apertando o botao (click_tecla01)
    //    if (!(LevelController.SilabaDigitada.Length >= LevelController.CharLimitForLevel))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
    //    {
    //        LevelController.SilabaDigitada = string.Concat(LevelController.SilabaDigitada, currentButton.name);          
    //    }
    //    if (LevelController.SilabaDigitada.Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
    //    {
    //        LevelController.BotaoConfirmaResposta = true;
    //    }

    //}

    /// <summary>
    /// Se o teclado digita estiver liberado, recebe a letra digitada e concatena com as previamente digitadas (se não atingiu o máximo de letras)
    /// Após concatenar, verifica se pode liberar o botão para confirmar a resposta
    /// </summary>
    public void buttonPressed()
    {
        if (!LevelController.bloqueiaBotao)
        {
            click.Play(0); // toca som de apertando o botao (click_tecla01)

            #region Seleção de botões antigas
            //if (1 == LevelController.currentLevel)
            //{
            //    buttonLevel01();
            //}
            //else if (2 == LevelController.currentLevel)
            //{
            //    buttonLevel02();
            //}
            //else if (3 == LevelController.currentLevel)
            //{
            //    buttonLevel03();
            //}
            //else if (4 == LevelController.currentLevel)
            //{
            //    buttonLevel04();
            //}
            //else if (5 == LevelController.currentLevel)
            //{
            //    buttonLevel05();
            //}
            #endregion

            int i = 0;
        
            // Encontra em qual silaba está (i = silaba em que está - 1)
            while (i < LevelController.textSlots && LevelController.inputText[i] != null && LevelController.originalText[i].Length <= LevelController.inputText[i].Length)
            {
                i++;
            }

            // Verificar se a silaba em questão está completa (todas as letras) e se pode liberar o botão confirma
            if (i < LevelController.textSlots)
            {
                if (LevelController.inputText[i] == null || LevelController.inputText[i].Length < LevelController.originalText[i].Length)
                {
                    LevelController.inputText[i] = string.Concat(LevelController.inputText[i], currentButton.name);
                }
                if (stageManager.textSlots == i + 1 && LevelController.inputText[i].Length == LevelController.originalText[i].Length)
                {
                    LevelController.BotaoConfirmaResposta = true;
                }
            }

            // Verifica se há mais algum espaço a ser preenchido
            while (i < LevelController.textSlots && LevelController.inputText[i] != null && LevelController.originalText[i].Length <= LevelController.inputText[i].Length)
            {
                i++;
            }
            if (i == LevelController.textSlots)
            {
                LevelController.BotaoConfirmaResposta = true;
            }
        }
    }

    // Botões não utilizados
    //public void buttonLevel01()
    //{
    //   // click.Play(0);//toca som de apertando o botao (click_tecla01)
    //    if (LevelController.silabasDigitadas[0] == null || !(LevelController.silabasDigitadas[0].Length > 1))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
    //    {
    //        LevelController.silabasDigitadas[0] = string.Concat(LevelController.silabasDigitadas[0], currentButton.name);
    //    }              
    //    if (LevelController.silabasDigitadas[0].Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
    //    {
    //        LevelController.BotaoConfirmaResposta = true;
    //    }
    //}

    //public void buttonLevel02()
    //{
    //   // click.Play(0);//toca som de apertando o botao (click_tecla01)
    //    if (LevelController.silabasDigitadas[0] == null || !(LevelController.silabasDigitadas[0].Length > 1))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
    //    {
    //        LevelController.silabasDigitadas[0] = string.Concat(LevelController.silabasDigitadas[0], currentButton.name);
    //    }
    //    else if (LevelController.silabasDigitadas[0].Length > 1 && (LevelController.silabasDigitadas[1] == null || LevelController.silabasDigitadas[1].Length < 2))
    //    {
    //        LevelController.silabasDigitadas[1] = string.Concat(LevelController.silabasDigitadas[1], currentButton.name);
    //    }
    //    string temp = string.Concat(LevelController.silabasDigitadas[0], LevelController.silabasDigitadas[1]);
    //    if (temp.Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
    //    {
    //        LevelController.BotaoConfirmaResposta = true;
    //    }
    //}

    //public void buttonLevel03()
    //{
    //   // click.Play(0);//toca som de apertando o botao (click_tecla01)
    //    if (LevelController.silabasDigitadas[0] == null || !(LevelController.silabasDigitadas[0].Length > 1))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
    //    {
    //        LevelController.silabasDigitadas[0] = string.Concat(LevelController.silabasDigitadas[0], currentButton.name);
    //    }
    //    else if (LevelController.silabasDigitadas[0].Length > 1 && (LevelController.silabasDigitadas[1] == null || LevelController.silabasDigitadas[1].Length < 2))
    //    {
    //        LevelController.silabasDigitadas[1] = string.Concat(LevelController.silabasDigitadas[1], currentButton.name);
    //    }
    //    else if (LevelController.silabasDigitadas[1].Length > 1 && (LevelController.silabasDigitadas[2] == null || LevelController.silabasDigitadas[2].Length < 2))
    //    {
    //        LevelController.silabasDigitadas[2] = string.Concat(LevelController.silabasDigitadas[2], currentButton.name);
    //    }
    //    string temp = string.Concat(LevelController.silabasDigitadas[0], LevelController.silabasDigitadas[1], LevelController.silabasDigitadas[2]);
    //    if (temp.Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
    //    {
    //        LevelController.BotaoConfirmaResposta = true;
    //    }
    //}

    //public void buttonLevel04()
    //{
    //   // click.Play(0);//toca som de apertando o botao (click_tecla01)
    //    if (LevelController.silabasDigitadas[0] == null || !(LevelController.silabasDigitadas[0].Length > 1))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
    //    {
    //        LevelController.silabasDigitadas[0] = string.Concat(LevelController.silabasDigitadas[0], currentButton.name);
    //    }
    //    else if (LevelController.silabasDigitadas[0].Length > 1 && (LevelController.silabasDigitadas[1] == null || LevelController.silabasDigitadas[1].Length < 2))
    //    {
    //        LevelController.silabasDigitadas[1] = string.Concat(LevelController.silabasDigitadas[1], currentButton.name);
    //    }
    //    else if (LevelController.silabasDigitadas[1].Length > 1 && (LevelController.silabasDigitadas[2] == null || LevelController.silabasDigitadas[2].Length < 2))
    //    {
    //        LevelController.silabasDigitadas[2] = string.Concat(LevelController.silabasDigitadas[2], currentButton.name);
    //    }
    //    else if (LevelController.silabasDigitadas[2].Length > 1 && (LevelController.silabasDigitadas[3] == null || LevelController.silabasDigitadas[3].Length < 2))
    //    {
    //        LevelController.silabasDigitadas[3] = string.Concat(LevelController.silabasDigitadas[3], currentButton.name);
    //    }
    //    string temp = string.Concat(LevelController.silabasDigitadas[0], LevelController.silabasDigitadas[1], LevelController.silabasDigitadas[2], LevelController.silabasDigitadas[3]);
    //    if (temp.Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
    //    {
    //        LevelController.BotaoConfirmaResposta = true;
    //    }
    //}

    //public void buttonLevel05()
    //{
    //    // click.Play(0);//toca som de apertando o botao (click_tecla01)
    //    if (LevelController.silabasDigitadas[0] == null || !(LevelController.silabasDigitadas[0].Length > 1))//Se não tem limite de caracteres definidos em CHARLIMITFORLEVEL, concatena o caractere digitado com o que já está na string
    //    {
    //        LevelController.silabasDigitadas[0] = string.Concat(LevelController.silabasDigitadas[0], currentButton.name);
    //    }
    //    else if (LevelController.silabasDigitadas[0].Length > 1 && (LevelController.silabasDigitadas[1] == null || LevelController.silabasDigitadas[1].Length < 3))
    //    {
    //        LevelController.silabasDigitadas[1] = string.Concat(LevelController.silabasDigitadas[1], currentButton.name);
    //    }
    //    string temp = string.Concat(LevelController.silabasDigitadas[0], LevelController.silabasDigitadas[1]);
    //    if (temp.Length >= LevelController.CharLimitForLevel)//se já tem limite de caracteres digitados definido em CHARLIMITFORLEVEL, habilita botao confirmar
    //    {
    //        LevelController.BotaoConfirmaResposta = true;
    //    }
    //}

}
