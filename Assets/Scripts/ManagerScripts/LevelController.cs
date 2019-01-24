using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelController
{
    public static int MaxScoreGlobal = 2;//pontuação objetivo para progredir ou regredir
    public static int currentLevel;
    public static int currentAct;
    public static string PalavraSelecionada = "";//palavra selecionada da lista do nível correspondente (arquivos de áudio)

    /* Troca das duas variáveis pelo número de quadrados que devem ser preenchidos
    public static int CharLimitForLevel = 0;
    public static int NumeroDeSilabasDaPalavra;
    */
    public static int textSlots;

    public static bool IsLoaderAlreadyLaunched = false;
    public static bool BotaoConfirmaResposta = false;
    public static int scorePositive = 0;
    public static int NegativeScore = 0;
    public static bool AudioHintIsUsed = false;
    public static bool VisualHintIsUsed = false;
    public static bool TimeIsUp = false;
    public static bool TimeIsRunning = false;//flag para indicar que o tempo está correndo para a resposta
    public static bool DicaVisualAtiva = false;//flag para indicar que está sendo mostrada a dica visual
    public static bool TimePause = false;
    
    public static bool bloqueiaBotao = true; // Flag para indicar que está verificando a resposta digitada, utilizada para bloquear os botões do teclado virtual

    public static bool AlgumaSilabaErrada = false;

    // O que deve ser escrita
    public static string[] originalText;
    // O que está sendo escrito
    public static string[] inputText;

    // Para verificar se a palavra deve ser separada em sílabas ou letras
    public static bool eSilaba;

    //Número total de estrelas que podem ser obtidas em cada sistema
    public static int[] estrelaSistemaTotal = new int [5] {30, 30, 30, 30, 30};
    
    public static void InitializeVars()
    {
        originalText = new string[textSlots];
        inputText = new string[textSlots];
    }    

    public static void SeparaSilabas()
    {
        // Separa silabas - como todas as sílabas no arquivo terminam em uma vogal, concatena as letras até encontrar uma vogal
        // CASO COLOQUEM PALAVRAS COM SÍLABAS QUE NÃO TERMINEM EM VOGAIS ESTA PARTE DEVE SER REVISADA
        string vogais = "AEIOU", silabaTemp = "";
        int k = 0;

        for (int i = 0; i < PalavraSelecionada.Length; i++)
        {
            silabaTemp = string.Concat(silabaTemp, PalavraSelecionada[i]);
            if (vogais.IndexOf(PalavraSelecionada[i]) != -1)
            {
                //Debug.Log(k + " " + silabaTemp);
                originalText[k] = silabaTemp;
                silabaTemp = "";
                k++;
            }
        }
    }

    /// <summary>
    /// Separa as letras da PalavraSelecionada
    /// </summary>
    public static void SeparaLetras()
    {
        for (int i = 0; i < textSlots; i++)
        {
            originalText[i] = PalavraSelecionada[i].ToString();
            //Debug.Log(i + " " + originalText[i]);
        }
    }
}
