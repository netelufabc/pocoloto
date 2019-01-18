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
    public bool[] avatarBloqueado = new bool[10] { false, false, true, true, true, true, true, true, true, true };
    public bool[] sistemaLiberado = new bool[5] { true, false, false, false, false };
    public int[,] pontuacao = new int[20, 3]; //Ler como planeta x, ato y

    public void CriarPontuacaoInicial()
    {
        for (int i = 0; i<20; i++)
        {
            for (int j = 0; j<3; j++)
            {
                pontuacao[i, j] = 0;
            }
        }
    }

    public Row[] pontuacaoTeste;

    public void TesteCriarPontuacao()
    {
        pontuacaoTeste = new Row[22];
        pontuacaoTeste[0].initRow(4);
        pontuacaoTeste[1].initRow(4);
        pontuacaoTeste[2].initRow(4);
        pontuacaoTeste[3].initRow(2);
        pontuacaoTeste[4].initRow(4);
        pontuacaoTeste[5].initRow(4);
        pontuacaoTeste[6].initRow(4);
        pontuacaoTeste[7].initRow(4);
        pontuacaoTeste[8].initRow(4);
        pontuacaoTeste[9].initRow(4);
        pontuacaoTeste[10].initRow(4);
        pontuacaoTeste[11].initRow(4);
        pontuacaoTeste[12].initRow(4);
        pontuacaoTeste[13].initRow(4);
        pontuacaoTeste[14].initRow(4);
        pontuacaoTeste[15].initRow(4);
        pontuacaoTeste[16].initRow(4);
        pontuacaoTeste[17].initRow(4);
        pontuacaoTeste[18].initRow(4);
        pontuacaoTeste[19].initRow(4);
        pontuacaoTeste[20].initRow(4);
        pontuacaoTeste[21].initRow(4);
    }
}

[Serializable]
public struct Row
{
    public int[] rowData;

    public void initRow(int i)
    {
        rowData = new int[i];
    }
}