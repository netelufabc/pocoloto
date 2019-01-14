using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Esta classe só pode ser anexada a UI Buttons. 
/// Quando o mouse passa por cima do objeto anexado, o cursor alterna para a imagem aqui inserida.
/// Também pode ser herdada.
/// </summary>

public class CursorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Texture2D cursor;
    private Button botao;

    private void Awake()
    {
        cursor = Resources.Load<Texture2D>("Images/cursor-edit-th");
        botao = this.GetComponent<UnityEngine.UI.Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (botao.interactable) {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
