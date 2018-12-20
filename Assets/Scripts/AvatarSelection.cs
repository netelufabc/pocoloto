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
    /// Sombra que cobre avatar bloqueado
    /// </summary>
    public GameObject avatarShadow;

	void Start () {
        // Tamanho da área onde os avatares estão - talvez não seja necessário...
        float canvasWidth = 0; // x (-4, 4), y (-2, 1), escala 0.5
        Vector3 avatarPositon = new Vector3 (0, 0, 0);

        // Espaçamento entre os avatares na tela
        float xSpace = 2; // 20/numAvatarTotal, 
        float ySpace = 3; // 30/numAvatarTotal;

        // Contador para verificar se todos os avatares liberados já foram colocados
        int cont = 0;
        GameObject instanceAvatar;

        // Posiciona os avatares em duas linhas
		for (int i = 0; i < numAvatarTotal/2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (cont < avatarImage.Length)
                {
                    instanceAvatar = avatarImage[cont];
                }
                else
                {
                    instanceAvatar = avatarShadow;
                }

                // Instancia e posiciona o avatar no local certo com o tamanho certo
                avatarPositon.x = -4 + xSpace * i;
                avatarPositon.y = -2 + ySpace * j;
                instanceAvatar = Instantiate(instanceAvatar);
                instanceAvatar.transform.SetPositionAndRotation(avatarPositon, Quaternion.identity);
                instanceAvatar.transform.localScale = new Vector3(0.5f, 0.5f, 1);

                // Mudança de cor do avatar para o preto - para uma futura função
                //Renderer avatarRenderer = instanceAvatar.GetComponent<Renderer>();
                //avatarRenderer.material.SetColor("_Color", Color.black);

                cont++;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
