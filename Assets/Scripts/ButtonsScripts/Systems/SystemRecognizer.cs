using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemRecognizer : MonoBehaviour {

    public static int numberOfSystems;

    /// <summary>
    /// Verifica o numero de sistemas que há no início da scene para saber até onde as instâncias SystemSelectButton pode ir;
    /// </summary>
    private void Awake()
    {
        numberOfSystems = 0;
        int i = 0;
        while (GameObject.Find(string.Concat("Sistema ", i.ToString())) != null)
        {
            numberOfSystems++;
            i++;
        }
    }

}
