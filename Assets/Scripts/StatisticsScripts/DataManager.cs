using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour {

    public static StatisticsData statisticsData = new StatisticsData();
    private static string statisticsDataPath;
    private static string fileHeaderCSV = "NumSistema,NumPlaneta,AtoPlaneta,RespostaCorreta,PalavraSelecionada,PalavraEscrita,Tempo,UtilizouDicaisual,UtiizouDicaAuditiva,DataHora\n";
    
    /*private void Awake()
    {
        statisticsDataPath = Application.persistentDataPath + "/" + SaveManager.player.nome + ".csv";
        if (!File.Exists(statisticsDataPath))
        {
            StreamWriter sw = File.CreateText(statisticsDataPath);
            sw.Close();
            File.AppendAllText(statisticsDataPath, fileHeaderCSV);
        }
    }*/

    public static void SelectProperFile()
    {
        statisticsDataPath = Application.persistentDataPath + "/" + SaveManager.player.nome + ".csv";
        if (!File.Exists(statisticsDataPath))
        {
            StreamWriter sw = File.CreateText(statisticsDataPath);
            sw.Close();
            File.AppendAllText(statisticsDataPath, fileHeaderCSV);
        }
    }

    public static void SaveStatistics(StatisticsData data)
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

    public static void SaveStatistics()
    {
        if (File.Exists(statisticsDataPath))
        {
            File.AppendAllText(statisticsDataPath, statisticsData.ToString());
        }
        else
        {
            Debug.Log("Arquivo não encontrado");
        }
    }

    public void TestScript()
    {
        Debug.Log(DateTime.Now);
        statisticsData.SetTime();
        SaveStatistics(statisticsData);
    }
}
