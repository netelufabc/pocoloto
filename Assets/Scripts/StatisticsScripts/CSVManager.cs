using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVManager : MonoBehaviour {

    public StatisticsData statisticsData;
    private string statisticsDataPath;
    private string fileHeaderCSV = "NumSistema,NumPlaneta,RespostaCorreta,PalavraSelecionada,PalavraEscrita,Tempo,UtilizouDicaisual,UtiizouDicaAuditiva\n";
    private string resumeHeader = "NumSistema,NumPlaneta,Passou,Acertos,Erros,TempoTotal\n";

    private void Awake()
    {
        statisticsDataPath = Application.persistentDataPath + "/" + SaveManager.player.nome + ".csv";
        if (!File.Exists(statisticsDataPath))
        {
            StreamWriter sw = File.CreateText(statisticsDataPath);
            sw.Close();
            File.AppendAllText(statisticsDataPath, fileHeaderCSV);
        }
    }

    public void SaveStatistics(StatisticsData data)
    {
        if (File.Exists(statisticsDataPath))
        {
            File.AppendAllText(statisticsDataPath, data.ToString());
        }
        else
        {
            Debug.Log("Arquivo não encontrado");
        }
    }

    public void AddResume()
    {
        if (!File.Exists(statisticsDataPath))
        {
            File.AppendAllText(statisticsDataPath, resumeHeader);
        }
    }

    public void TestScript()
    {
        SaveStatistics(statisticsData);
    }
}
