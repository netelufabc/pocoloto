using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelection : MonoBehaviour {

    /// <summary>
    /// Número de avatares disponívies
    /// </summary>
    public float numAvatarTotal = 10;
    /// <summary>
    /// Vetor contendo as prefab dos avatares
    /// </summary>
    public GameObject[] avatarImage;
    /// <summary>
    /// Vetor com a informação de qual avatar está disponível e qual está bloqueado
    /// Obtido do save do jogador
    /// </summary>
    private bool[] avatarBloqueado;

	void Start () {
        avatarBloqueado = SaveManager.player.avatarBloqueado;
        
        // Posiciona os avatares
		for (int i = 0; i < numAvatarTotal; i++)
        {
            avatarImage[i] = Instantiate(avatarImage[i], transform);
            
            // Bloqueio dos avatares não liberados
            if (avatarBloqueado[i])
            {
                avatarImage[i].GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
            else
            {
                avatarImage[i].GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
        }
	}

    /// <summary>
    /// Função de teste para verificação da posição escolhida
    /// </summary>
    public void AvatarSelecionado()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3 wcellPosition = transform.position;
        Vector3Int cellPosition = gridLayout.WorldToCell(wcellPosition);
        Debug.Log(cellPosition);
    }
}
