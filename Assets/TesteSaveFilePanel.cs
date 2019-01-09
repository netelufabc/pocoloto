using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEditor;

public class TesteSaveFilePanel : MonoBehaviour {

	public void Save()
    {
        Player player = SaveManager.player;
        string path = "";
        if (player != null)
        {
            path = EditorUtility.SaveFilePanel(
                "Save Game",
                "",
                player.nome + ".json",
                "json"
                );
        }
        Debug.Log(path);
    }

    public void Load()
    {
        Player player = SaveManager.player;
        string path = "";
        if (player != null)
        {
            path = EditorUtility.OpenFilePanel(
                "Load Game",
                "",
                "json"
                );
        }
        Debug.Log(path);
    }
}
