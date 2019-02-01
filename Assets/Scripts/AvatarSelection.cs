using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour {

    [Tooltip("Prefab do avatar a ser exibido")]
    public GameObject avatarImagePrefab;
    [Tooltip("Número de avatares exibidos")]
    private float numAvatarExibidos;
    [Tooltip("Lista con as informações dos itens")]
    public ItensInfoList itensInfoList;
    // Lista com as informações de todos os avatares
    private ItemInfo[] avatarList;
    // Vetor com a informação de qual avatar está disponível e qual está bloqueado. Obtido do save do jogador
    private bool[] avatares;


	void Start () {
        avatares = SaveManager.player.avatares;
        avatarList = itensInfoList.avatarsToSell;
        numAvatarExibidos = SaveManager.player.numAvataresLiberadso;

        // Posiciona os avatares
		for (int i = 0; i < numAvatarExibidos; i++)
        {
            if (avatares[i])
            {
                GameObject avatarImage = Instantiate(avatarImagePrefab, transform);
                avatarImage.GetComponent<Image>().sprite = avatarList[i].itemSprite;
            }
        }
	}
    /*
    /// <summary>
    /// Função de teste para verificação da posição escolhida
    /// </summary>
    public void AvatarSelecionado()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3 wcellPosition = transform.position;
        Vector3Int cellPosition = gridLayout.WorldToCell(wcellPosition);
        Debug.Log(cellPosition);
    }*/
}
