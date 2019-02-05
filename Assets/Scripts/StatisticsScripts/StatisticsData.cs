using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatisticsData {

    public string sistema;
    public string planeta;
    public string palavraSelecionada;
    public string palavraEscrita;
    public bool respostaCorreta;
    public bool dicaVisual;
    public bool dicaAuditiva;
    public float tempoUtiizado;

    private string lineSeparator = "\n";
    private string fieldSeparator = ",";

    public override string ToString()
    {
        return sistema + fieldSeparator + planeta + fieldSeparator + respostaCorreta + fieldSeparator +
            palavraSelecionada + fieldSeparator + palavraEscrita + fieldSeparator + tempoUtiizado + fieldSeparator +
            dicaVisual + fieldSeparator + dicaAuditiva + lineSeparator;
    }
}
