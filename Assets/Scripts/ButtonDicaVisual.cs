using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ButtonDicaVisual : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{

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


    public void ActiveButton()
    {
        BotaoDicaVisual = GameObject.FindGameObjectWithTag("Button Eye").GetComponent<UnityEngine.UI.Button>(); //Como o canvas é destruído entre as scenes, é necessário reestabelecer a referência.
        BotaoDicaVisual.interactable = true;
    }

    public void DeactiveButton()
    {
        BotaoDicaVisual.interactable = false;
    }

    public void AcionaDicaVisual()//botao dica visual
    {
        if(!LevelController.bloqueiaBotao)
        {
            BotaoDicaVisual.interactable = false;//desabilita dica visual
            StartCoroutine(MostraDica());
        }
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
}
