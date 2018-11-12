using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelController
{
    public static int MaxScoreGlobal = 2;//pontuação objetivo para progredir ou regredir
    public static string PalavraSelecionada = "";//palavra selecionada da lista do nível correspondente (arquivos de áudio)
    public static int CharLimitForLevel = 0;
    public static bool BotaoConfirmaResposta = false;
    public static int scorePositive = 0;
    public static int NegativeScore = 0;
    public static bool AudioHintIsUsed = false;
    public static bool VisualHintIsUsed = false;
    public static bool TimeIsUp = false;
    public static bool TimeIsRunning = false;//flag para indicar que o tempo está correndo para a resposta
    public static bool DicaVisualAtiva = false;//flag para indicar que está sendo mostrada a dica visual

    public static int NumeroDeSilabasDaPalavra;

    public static bool AlgumaSilabaErrada = false;

    public static string[] silabas;
    public static string[] silabasDigitadas;

    public static void InitializeVars()
    {
        silabas = new string[NumeroDeSilabasDaPalavra];
        silabasDigitadas = new string[NumeroDeSilabasDaPalavra];
    }    

    public static void SeparaSilabas()
    {
        int counter = 0;
        for (int i = 0; i < NumeroDeSilabasDaPalavra; i++)
        {
            silabas[i] = string.Concat(PalavraSelecionada[counter + i], PalavraSelecionada[counter + i + 1]);
            counter++;
        }
  
    }

}
