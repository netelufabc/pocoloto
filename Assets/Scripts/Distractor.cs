using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Distractor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Imagem que o cursor terá quando entrar no espaço do distrator")]
    public Texture2D cursor;
    SilabaControl silabaControl;
    //private Vector2 cursorHotSpot;
    private Button botao;
    private Animator animator;
    /// <summary>
    /// Velocidade em que o distrator vai girar
    /// </summary>
    private float SpinningSpeed;

    /// <summary>
    /// Número total de distradores na cena.
    /// </summary>
    public static int numDistractors;

    private void Awake()
    {
        botao = this.GetComponent<UnityEngine.UI.Button>();
        LevelController.bloqueiaBotao = true; //Bloqueia os botões para que a pessoa seja obrigada a destruir todos distradores
        numDistractors++;
        animator = this.GetComponent<Animator>();
        SpinningSpeed = Random.Range(-1f, 1f);
        animator.SetFloat("SpeedMultiplier", SpinningSpeed);
        //cursorHotSpot = new Vector2 (cursor.width / 2, cursor.height / 2);
    }

    private void Start()
    {
        silabaControl = SilabaControl.instance;
    }

    /// <summary>
    /// Destrói o distrator
    /// </summary>
    public void DestroyDistractor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //Muda o curso de volta ao normal, pois a função OnPointerExit não funciona quando o objeto é destruído
        numDistractors--;
        if (numDistractors == 0) //Verifica o número de distradores na cena para ver se pode desbloquear os botões
        {
            LevelController.bloqueiaBotao = false;
            silabaControl.CompleteEmptyTextSlots();

        }
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (botao.interactable)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
