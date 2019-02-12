using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EraseButton : CursorChange {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            buttonPressed();
        }
    }

    /// <summary>
    /// Verifica se tem alguma silaba escrita e apaga somente a ultima silaba escrita
    /// </summary>
    public void buttonPressed()
    {
        // Verifica se os botões não estão bloqueados
        if (!LevelController.bloqueiaBotao)
        {
            //click.Play(0);
            

            // Encontra o máximo de sílabas existentes menos 1 - vetor inicia em 0
            int i = LevelController.textSlots - 1;
            SilabaControl silabaControl = SilabaControl.instance;

            // Varre o vetor a partir do final procurando a última sílaba digitada
            while (i > -1)
            {
                // Se o vetor é vazio, não faz nada
                if (LevelController.inputText[i] != null)
                {
                    // Se não for vazio, verifica se existe alguma letra na posição i do vetor
                    if (LevelController.inputText[i].Length > 0)
                    {
                        // Se tem letra, ve se pode apagar
                        if (silabaControl.isPlanetLetter[i])
                        {
                            // Apaga e sai
                            LevelController.inputText[i] = "";//.Remove(LevelController.silabasDigitadas[1].Length - 1);
                            break;
                        }
                    }
                }

                i--;
            }

            // Bloqueia o botão de confirmar resposta, pois todas as sílabas não foram digitadas
            LevelController.BotaoConfirmaResposta = false;

        }
        SetaIndicadora.DestroiSeta();
        SetaIndicadora.IndicarPos();
    }
}
