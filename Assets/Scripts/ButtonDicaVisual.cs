using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonDicaVisual : MonoBehaviour {

    public static ButtonDicaVisual instance = null;
    private Button BotaoDicaVisual;
    private StageManager stageManager;
    private Text [] TelaSilabaDigitada;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        BotaoDicaVisual = GameObject.FindGameObjectWithTag("Button Eye").GetComponent<UnityEngine.UI.Button>();
    }

    private void Start()
    {
        stageManager = StageManager.instance;
        TelaSilabaDigitada = stageManager.GetTelaSilabaDigitada();
    }

    public void AcionaDicaVisual()//botao dica visual
    {
        BotaoDicaVisual.interactable = false;//desabilita dica visual
        StartCoroutine(MostraDica());
    }

    IEnumerator MostraDica()
    {
        LevelController.DicaVisualAtiva = true;
        int randomNumber = Random.Range(0, LevelController.NumeroDeSilabasDaPalavra - 1);
        TelaSilabaDigitada[randomNumber].text = LevelController.silabas[randomNumber];
        yield return new WaitForSeconds(1);
        TelaSilabaDigitada[randomNumber].text = LevelController.silabasDigitadas[randomNumber];
        LevelController.DicaVisualAtiva = false;
        
    }

    public void DesactiveButton()
    {
        BotaoDicaVisual.interactable = false;
    }

    public void ActiveButton()
    {
        BotaoDicaVisual.interactable = true;
    }

}
