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

    public Row[] planeta;

    public void CriarPontuacaoInicial()
    {
        planeta = new Row[22];
        planeta[0].initRow(3);
        planeta[1].initRow(3);
        planeta[2].initRow(3);
        planeta[3].initRow(1);
        planeta[4].initRow(3);
        planeta[5].initRow(3);
        planeta[6].initRow(3);
        planeta[7].initRow(3);
        planeta[8].initRow(3);
        planeta[9].initRow(3);
        planeta[10].initRow(3);
        planeta[11].initRow(3);
        planeta[12].initRow(3);
        planeta[13].initRow(3);
        planeta[14].initRow(3);
        planeta[15].initRow(3);
        planeta[16].initRow(3);
        planeta[17].initRow(3);
        planeta[18].initRow(3);
        planeta[19].initRow(3);
        planeta[20].initRow(3);
        planeta[21].initRow(3);
    }
}

[Serializable]
public struct Row
{
    public int[] ato;

    public void initRow(int i)
    {
        ato = new int[i];
    }

    ///Ato 0 indica quantos atos existem no planeta
    ///acessar como planeta[x].ato[y];
}