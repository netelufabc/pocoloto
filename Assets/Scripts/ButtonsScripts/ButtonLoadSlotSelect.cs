using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadSlotSelect : MonoBehaviour {

    /// <summary>
    /// Informa qual o slot selecionado para carregar (load)
    /// </summary>
	public void SlotSelected()
    {
        // Variável criada para obter o indice do slot
        // Deve ser uma variável publica do gamemanager ou esta função deve chamar a função de load do savemanager
        int slot;

        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3Int cellPos = gridLayout.WorldToCell(transform.position);
        slot = PositionToIndex(cellPos);
        GameObject.Find("Confirma").GetComponent<UnityEngine.UI.Button>().interactable = true;

        //Debug.Log("Slot selecionado: " + slot);
    }

    /// <summary>
    /// Retorna o indice do slot a partir de uma posição no grid
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private int PositionToIndex(Vector3Int pos)
    {
        int y = -(pos.y + 30) / 50;
        return y;
    }
}
