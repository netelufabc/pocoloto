using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarChoice : MonoBehaviour {

    public GameObject itensInfoList;
    private List<int> indiceLiberados;
    
    private void Awake()
    {
        indiceLiberados = new List<int>();

        for (int i = 0; i<SaveManager.player.avatares.Length; i++)
        {
            if (SaveManager.player.avatares[i])
            {
                indiceLiberados.Add(i);
            }
        }

        foreach (int n in indiceLiberados)
        {
            AvatarButtonChoiceMaker.Create(n);
        }

    }

}
