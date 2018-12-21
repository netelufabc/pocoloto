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
    public int avatarSelecionadoIndex;
    public bool[] avatarBloqueado;
}