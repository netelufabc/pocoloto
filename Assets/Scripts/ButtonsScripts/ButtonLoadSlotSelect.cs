using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonLoadSlotSelect : MonoBehaviour {
        
    /// <summary>
    /// Informa qual o slot selecionado para carregar (load)
    /// </summary>
	public void SlotSelected()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3Int cellPos = gridLayout.WorldToCell(transform.position);
        SaveManager.selectedSlot = PositionToIndex(cellPos);
        GameObject.Find("Confirma").GetComponent<UnityEngine.UI.Button>().interactable = true;
        GameObject.Find("Delete").GetComponent<UnityEngine.UI.Button>().interactable = true;
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

    /// <summary>
    /// Carrega os dados armazenados no slot selecionado
    /// </summary>
    public void LoadSelectedSlot()
    {
        SaveManager.Load(SaveManager.selectedSlot);
    }

    /// <summary>
    /// Deleta o save do slot selecionado
    /// </summary>
    public void DeleteSelecteSlot()
    {
        SaveData.DeletePlayer();
        SceneManager.LoadScene("03.2_loadSelection");
    }
}
