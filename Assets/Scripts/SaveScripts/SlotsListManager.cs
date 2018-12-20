using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SlotsListManager : MonoBehaviour {

    #region Save&Load
    public static void saveSlotsList(string path, SlotsList list)
    {
        string json = JsonUtility.ToJson(list);

        StreamWriter sw = File.CreateText(path);
        sw.Close();

        File.WriteAllText(path, json);
    }

    public static SlotsList loadSlotsList(string path)
    {
        string json = File.ReadAllText(path);

        return JsonUtility.FromJson<SlotsList>(json);
    }

    #endregion

    /// <summary>
    /// Checa se o número n está dentro da lista dada, caso esteja, retorna true
    /// </summary>
    /// <param name="n"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool CheckSameNumber(int n, SlotsList list)
    {
        foreach (int p in list.slotsList)
        {
            if (n == p)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Caso o número já esteja na lista, retira esse número da lista e salva a nova lista
    /// </summary>
    /// <param name="n"></param>
    public static void RetiraKey(int n)
    {
        if (CheckSameNumber(n, GameController.list))
        {
            GameController.list.slotsList.Remove(n);
            saveSlotsList(GameController.slotsDataPath, GameController.list);
        }
    }

    /// <summary>
    /// Verifica se existe algum número disponível na lista, caso exista, devolve o primeiro número da lista
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static int SlotGiver(SlotsList list)
    {
        if (list.slotsList.Count == 0)
        {
            return -1;
        }

        else
        {
            list.slotsList.Sort();
            Debug.Log(list.slotsList[0]);
            return list.slotsList[0];
        }
    }
}
