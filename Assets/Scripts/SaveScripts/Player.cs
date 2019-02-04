using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public int slot = -1;
    public string nome ="";
    public bool genero = false;
    public int idade = 0;
    public int serie = 0;
    public int avatarSelecionadoIndex = -1;
    public int colorSelecionadoIndex = 0;
    public int dinheiro;
    public int totalEstrelas;
    // Lista de itens que podem ser comprados na loja do jogo
    public int numAvataresLiberadso = 2;
    public bool[] avatares = new bool[12] { true, true, false, false, false, false, false, false, false, false, false, false };
    public bool[] cores = new bool[7] { true, false, false, false, false, false, false };
    public bool[] extras = new bool[5] { true, false, false, false, false };
    public bool sistemaLiberado = false;
    public int[] estrelaSistema = new int[5] { 0, 0, 0, 0, 0 };

    public Row[] planeta;

    public void CriarPontuacaoInicial()
    {
        totalEstrelas = 0;
        dinheiro = 0;
        planeta = new Row[23];
        #region Inicialização das variáveis ato de planeta
        planeta[0].initRow(3);
        planeta[1].initRow(3);
        planeta[2].initRow(3);
        planeta[3].initRow(1);
        planeta[4].initRow(3);
        planeta[5].initRow(3);
        planeta[6].initRow(3);
        planeta[7].initRow(3);
        planeta[8].initRow(1);
        planeta[9].initRow(3);
        planeta[10].initRow(3);
        planeta[11].initRow(3);
        planeta[12].initRow(3);
        planeta[13].initRow(1);
        planeta[14].initRow(3);
        planeta[15].initRow(3);
        planeta[16].initRow(3);
        planeta[17].initRow(3);


        planeta[19].initRow(3);
        planeta[20].initRow(3);
        planeta[21].initRow(3);
        planeta[22].initRow(1);
        #endregion

        // Inicializando os planetas liberados (primeiro planeta de cada sistema)
        planeta[0].liberado = true;
        for (int i = 1; i < planeta.Length; i++)
        {
            if((i + 1) % 5 == 0)
            {
                planeta[i].liberado = true;
            }
            else
            {
                planeta[i].liberado = false;
            }
        }
    }

    public bool CompletouPlaneta(int p)
    {
        for (int i = 0; i<planeta[p-1].ato.Length; i++)
        { 
            if (planeta[p-1].ato[i] == 0)
            {
                return false;
            }
        }
        return true;
    }

    public bool ZerouPlaneta(int p)
    {
        for (int i = 0; i < planeta[p-1].ato.Length; i++)
        {
            if (planeta[p-1].ato[i] != 3)
            {
                return false;
            }
        }
        return true;
    }

    /*ARRUMAR: colocar em relação a quantidfade de planetas que o sistema tem*/


    int[] numeroPlanetasPorSistema = new int[5] { 3, 8, 13, 18, 23 };

    public bool CompletouSistema(int s)
    {
        int i;
        if (s == 0)
        {
            i = 0;
        }
        else
        {
            i = numeroPlanetasPorSistema[s - 1] + 1; 

        }
        for (i = i ; i < numeroPlanetasPorSistema[s]; i++)
        {
            if (!CompletouPlaneta(i+1))
            {
                return false;
            }
        }
        return true;
    }

    public bool ZerouSistema(int s)
    {
        int i;
        if (s == 0)
        {
            i = 0;
        }
        else
        {
            i = numeroPlanetasPorSistema[s - 1] + 1;

        }
        for (i = i; i < numeroPlanetasPorSistema[s]; i++)
        {
            if (!ZerouPlaneta(i+1))
            {
                return false;
            }
        }
        return true;
    }
    /*
    public bool CompletouSistema(int s)
    {
        if (s == 0)
        {
            if (CompletouPlaneta(1) && CompletouPlaneta(2) && CompletouPlaneta(3) && CompletouPlaneta(4))
            {
                return true;
            }
            else
            {


                return false;
            }
        }

        else
        {
            if (CompletouPlaneta(5 + (s-1)*5) && CompletouPlaneta(6 + (s - 1) * 5) && CompletouPlaneta(7 + (s - 1) * 5) && CompletouPlaneta(8 + (s - 1) * 5) && CompletouPlaneta(9 + (s - 1) * 5))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }*/
    /*
    public bool ZerouSistema(int s)
    {
        if (s == 0)
        {
            if (ZerouPlaneta(1) && ZerouPlaneta(2) && ZerouPlaneta(3) && ZerouPlaneta(4))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        else
        {
            if (ZerouPlaneta(5 + (s - 1) * 5) && ZerouPlaneta(6 + (s - 1) * 5) && ZerouPlaneta(7 + (s - 1) * 5) && ZerouPlaneta(8 + (s - 1) * 5) && ZerouPlaneta(9 + (s - 1) * 5))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }*/
}

[Serializable]
public struct Row
{
    // Pontuação de cada ato
    public int[] ato;
    // Informa se o planeta está liberado para ser jogado
    public bool liberado;

    public void initRow(int i)
    {
        ato = new int[i];
    }

    ///Ato 0 indica quantos atos existem no planeta
    ///acessar como planeta[x].ato[y];
}