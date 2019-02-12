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

    /// <summary>
    /// Se o teclado digita estiver liberado, recebe a letra digitada e concatena com as previamente digitadas (se não atingiu o máximo de letras)
    /// Após concatenar, verifica se pode liberar o botão para confirmar a resposta
    /// </summary>
    public void buttonPressed()
    {
        if (!LevelController.bloqueiaBotao)
        {
            click.Play(0); // toca som de apertando o botao (click_tecla01)

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
}
