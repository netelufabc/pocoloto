using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ButtonDicaVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public static ButtonDicaVisual instance = null;
    private Button BotaoDicaVisual;
    private StageManager stageManager;
    private Text [] TelaSilabaDigitada;
    private Texture2D cursor;


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

        cursor = Resources.Load<Texture2D>("Images/cursor-edit-th");
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

    /// Parte para o cursor mudar quando está em cima do ícone, porque

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (BotaoDicaVisual.interactable)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
