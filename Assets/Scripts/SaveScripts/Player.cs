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

    public void CriatPontuacao()
    {
        planeta = new Row[22];
        planeta[0].initRow(4);
        planeta[1].initRow(4);
        planeta[2].initRow(4);
        planeta[3].initRow(2);
        planeta[4].initRow(4);
        planeta[5].initRow(4);
        planeta[6].initRow(4);
        planeta[7].initRow(4);
        planeta[8].initRow(4);
        planeta[9].initRow(4);
        planeta[10].initRow(4);
        planeta[11].initRow(4);
        planeta[12].initRow(4);
        planeta[13].initRow(4);
        planeta[14].initRow(4);
        planeta[15].initRow(4);
        planeta[16].initRow(4);
        planeta[17].initRow(4);
        planeta[18].initRow(4);
        planeta[19].initRow(4);
        planeta[20].initRow(4);
        planeta[21].initRow(4);
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