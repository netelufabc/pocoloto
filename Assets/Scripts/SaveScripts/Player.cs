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
}