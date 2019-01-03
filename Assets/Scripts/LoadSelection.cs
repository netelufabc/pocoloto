using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSelection : MonoBehaviour {

    // Utilizado para testes
    public GameObject loadSlotButton;

    // Utilizadas para carregar as informaçõs dos saves nos botões
    public static string dataPath = string.Empty;
    public static string slotsDataPath = string.Empty;
    public static SlotsList list = new SlotsList();
    public static Player player = new Player();
    public static SaveManager saveManager;

    void Start () {

        // Caminho padrão do local de save
        dataPath = Application.persistentDataPath;

        saveManager = GameObject.Find("Canvas").GetComponent<SaveManager>();

        // Percorre todos os possíveis save slots
        for (int i = 0; i < SaveManager.slotsListSize; i++)
        {
            loadSlotButton = Instantiate(loadSlotButton, transform);

            // Verifica se o slot está sendo usado (não está na lista)
            if (System.IO.File.Exists(dataPath + "/listaDeSlots.json") && !SlotsListManager.CheckSameNumber(i, SaveManager.list))
            {
                // Carrega o player para obter o nome
                saveManager.Load(i);
                loadSlotButton.GetComponentInChildren<Text>().text = "  Load Slot " + i + "\n  Player Name: " + SaveManager.player.nome;
                loadSlotButton.GetComponentInChildren<UnityEngine.UI.Button>().interactable = true;
                Sprite avatar = Resources.Load<Image>("Prefabs/AvatarTeste0" + (SaveManager.player.avatarSelecionadoIndex + 1).ToString()).sprite;
                loadSlotButton.GetComponentInChildren<Image>().sprite = avatar;
            }
            else
            {
                // Opção de deixar visível todos os slots, mas bloqueando os que não possuem save
                // Podemos colocar a criação do botão dentro do if e só aparece se existir save
                loadSlotButton.GetComponentInChildren<Text>().text = "  Load Slot " + i + "\n  Vazio";
                loadSlotButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                loadSlotButton.GetComponentInChildren<Image>().overrideSprite = null;
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