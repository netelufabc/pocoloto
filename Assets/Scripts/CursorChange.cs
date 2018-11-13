using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    }
}
