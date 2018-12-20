using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EraseButton : CursorChange {

    //Button currentButton;
    AudioSource click;

    void Start()
    {
       // currentButton = this.GetComponent<Button>();
        click = GetComponent<AudioSource>();
    }

    //public void buttonPressed1()
    //{
    //    click.Play(0);
    //    if (LevelController.SilabaDigitada.Length >= 1)
    //    {
    //        LevelController.SilabaDigitada = LevelController.SilabaDigitada.Remove(LevelController.SilabaDigitada.Length - 1);
    //        LevelController.BotaoConfirmaResposta = false;
    //    }       
    //}

    /// <summary>
    /// Verifica se tem alguma silaba escrita e apaga somente a ultima silaba escrita
    /// </summary>
    public void buttonPressed()
    {
        // Verifica se os botões não estão bloqueados
        if (!LevelController.bloqueiaBotao)
        {
            click.Play(0);

            // Encontra o máximo de sílabas existentes menos 1 - vetor inicia em 0
            int i = LevelController.NumeroDeSilabasDaPalavra - 1;

            // Varre o vetor a partir do final procurando a última sílaba digitada
            while (i > -1)
            {
                // Se o vetor é vazio, não faz nada
                if (LevelController.silabasDigitadas[i] != null)
                {
                    // Se não for vazio, verifica se existe alguma letra na posição i do vetor
                    if (LevelController.silabasDigitadas[i].Length > 0)
                    {
                        // Se tem letra, apaga e sai
                        LevelController.silabasDigitadas[i] = "";//.Remove(LevelController.silabasDigitadas[1].Length - 1);
                        break;
                    }
                }

                i--;
            }

            // Bloqueia o botão de confirmar resposta, pois todas as sílabas não foram digitadas
            LevelController.BotaoConfirmaResposta = false;
        }
    }
}
