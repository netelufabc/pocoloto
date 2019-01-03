using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAvatarSelect : MonoBehaviour {

    // Para testes e verificação
    //public Player player = SaveManager.player;

	/// <summary>
    /// Informa ao SaveManager qual o avatar selecionado (pelo índice do vetor de avatar)
    /// </summary>
    public void Avatar()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3Int cellPos = gridLayout.WorldToCell(transform.position);
        SaveManager.player.avatarSelecionadoIndex = PositionToIndex(cellPos);
        GameObject.Find("Confirma").GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    /// <summary>
    /// Transforma uma posição de célula no grid de avatares em um índice de vetor equivalente
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private int PositionToIndex(Vector3Int pos)
    {
        int x = (pos.x - 35) / 70;
        int y = -(pos.y + 35) / 70;
        return x + 3 * y;
    }
}