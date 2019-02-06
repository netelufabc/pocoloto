using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatisticsData
{
    public string sistema;
    public string planeta;
    public string ato;
    public string palavraSelecionada;
    public string palavraEscrita;
    public bool respostaCorreta;
    public bool dicaVisual;
    public bool dicaAuditiva;
    public float tempoUtiizado;
    public DateTime dataHora;

    private string lineSeparator = "\n";
    private string fieldSeparator = ",";

    /// <summary>
    /// Coloca todos os dados em uma única string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return sistema + fieldSeparator + planeta + fieldSeparator + ato + fieldSeparator + respostaCorreta + fieldSeparator +
             palavraSelecionada + fieldSeparator + palavraEscrita + fieldSeparator + tempoUtiizado + fieldSeparator +
             dicaVisual + fieldSeparator + dicaAuditiva + fieldSeparator + dataHora + lineSeparator;
    }
}
