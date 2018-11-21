using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ButtonDicaAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public static ButtonDicaAudio instance = null;
    private Button botaoDicaAudio;
    private SilabaControl silabaControl;
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
    }

    private void Start()
    {
        silabaControl = SilabaControl.instance;
        botaoDicaAudio = GameObject.FindGameObjectWithTag("Button Sound").GetComponent<UnityEngine.UI.Button>();
    }

    public void ActiveButton()
    {
        botaoDicaAudio = GameObject.FindGameObjectWithTag("Button Sound").GetComponent<UnityEngine.UI.Button>(); //Como o canvas é destruído entre as scenes, é necessário reestabelecer a referência.
        botaoDicaAudio.interactable = true;
    }

    public void DeactiveButton()
    {
        botaoDicaAudio.interactable = false;
    }
    
    public void AcionaDicaAudio()//botao dica audio
    {
        silabaControl.TocarSilabaAtual();//toca silaba atual
        DeactiveButton();
    }

    /// Parte para o cursor mudar quando está em cima do ícone, porque

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (botaoDicaAudio.interactable)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
