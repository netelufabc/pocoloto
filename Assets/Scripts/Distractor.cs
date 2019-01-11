using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Distractor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Imagem que o cursor terá quando entrar no espaço do distrator")]
    public Texture2D cursor;
    //private Vector2 cursorHotSpot;
    private Button botao;
    private Animator animator;
    /// <summary>
    /// Velocidade em que o distrator vai girar
    /// </summary>
    private float SpinningSpeed;

    private void Awake()
    {
        botao = this.GetComponent<UnityEngine.UI.Button>();
        animator = this.GetComponent<Animator>();
        SpinningSpeed = Random.Range(-1f, 1f);
        animator.SetFloat("SpeedMultiplier", SpinningSpeed);
        //cursorHotSpot = new Vector2 (cursor.width / 2, cursor.height / 2);
    }

    /// <summary>
    /// Destrói o distrator
    /// </summary>
    public void DestroyDistractor()
    {
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
