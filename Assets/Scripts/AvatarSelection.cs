using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelection : MonoBehaviour {

    /// <summary>
    /// Número de avatares disponívies
    /// </summary>
    //public int numAvatar;
    public float numAvatarTotal = 10;
    /// <summary>
    /// Vetor contendo um prefab do avatar
    /// </summary>
    public GameObject[] avatarImage;
    /// <summary>
    /// Vetor com a informação de qual avatar está disponível e qual está bloqueado
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
	
    public void AvatarSelecionado()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3 wcellPosition = transform.position;
        Vector3Int cellPosition = gridLayout.WorldToCell(wcellPosition);
        Debug.Log(cellPosition);
    }
}
