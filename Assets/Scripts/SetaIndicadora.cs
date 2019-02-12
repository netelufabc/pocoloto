using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetaIndicadora : MonoBehaviour {

    private static GameObject seta;
    private static GameObject newSeta;
    private static Text[] caixaDeTexto;

    private static string nomeObjeto = "Prefabs/Seta";
    private static StageManager stageManager;

    /// <summary>
    /// Prepara as variáveis para posicionar a seta no local correto
    /// </summary>
    public static void SetaSetup()
    {
        stageManager = StageManager.instance;
        seta = Resources.Load(nomeObjeto) as GameObject;
        caixaDeTexto = new Text[stageManager.textSlots];

        for (int i = 0; i < stageManager.textSlots; i++)
        {
            caixaDeTexto[i] = GameObject.Find(string.Concat("Silaba Digitada ", i.ToString())).GetComponent<UnityEngine.UI.Text>();
        }
    }

    /// <summary>
    /// Instancia a seta que indica a posição a ser completada
    /// </summary>
    public static void IndicarPos()
    {
        int i = 0;

        // Encontra em qual silaba está (i = silaba em que está - 1)
        while (i < LevelController.textSlots && LevelController.inputText[i] != null && LevelController.originalText[i].Length <= LevelController.inputText[i].Length)
        {
            i++;
        }
        
        if (i < stageManager.textSlots)
        {
            newSeta = Instantiate(seta, GameObject.Find("Canvas").transform);
            Vector3 setaPos = caixaDeTexto[i].transform.position + new Vector3(0, 1.5f, 0);
            newSeta.transform.position = setaPos;
        }
    }

    /// <summary>
    /// Destroi o indicador anterior
    /// </summary>
    public static void DestroiSeta()
    {
        Destroy(newSeta);
    }
}
