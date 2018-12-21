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
    public GameObject[] avatarImage;// = new GameObject[numAvatar];
    /// <summary>
    /// Vetor com a informação de qual avatar está disponível e qual está bloqueado
    /// </summary>
    private bool[] avatarBloqueado;

	void Start () {
        avatarBloqueado = new bool[10] {false, false, true, true, true, true, true, true, true, true};
        
        // Posiciona os avatares em duas linhas
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
	
	// Update is called once per frame
	void Update () {
        
	}

    public void AvatarSelecionado()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        //Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        Vector3 wcellPosition = transform.position;
        Vector3Int cellPosition = gridLayout.WorldToCell(wcellPosition);
        Debug.Log(cellPosition);
    }
}
