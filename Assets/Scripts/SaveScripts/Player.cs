using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public int slot;
    public string nome;
    public bool genero;
    public int idade;
    public int serie;
    public int avatarSelecionadoIndex = -1;
    public bool[] avatarBloqueado = new bool[10] { false, false, true, true, true, true, true, true, true, true };
    public bool[] planetaLiberado = new bool[5] { true, false, false, false, false };
}