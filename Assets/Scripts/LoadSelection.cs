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
        dataPath = SaveManager.dataPath;

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
                // Coloca as informações do save no botão de load
                loadSlotButton.GetComponentInChildren<Text>().text = "  Load Slot " + i + "\n  Player Name: " + SaveManager.player.nome;
                loadSlotButton.GetComponentInChildren<UnityEngine.UI.Button>().interactable = true;
                // Carrega a imagem do avatar para colocar no botão
                Sprite avatar = Resources.Load<Image>("Prefabs/AvatarTeste0" + (SaveManager.player.avatarSelecionadoIndex + 1).ToString()).sprite;
                // Encontra a imagem do botão e muda para o avatar selecionado
                GameObject buttonImage = loadSlotButton.transform.GetChild(1).gameObject;
                buttonImage.GetComponent<Image>().sprite = avatar;
            }
            else
            {
                // Opção de deixar visível todos os slots, mas bloqueando os que não possuem save
                // Podemos colocar a criação do botão dentro do if e só aparece se existir save
                loadSlotButton.GetComponentInChildren<Text>().text = "  Load Slot " + i + "\n  Vazio";
                // Desabilita o botão e remove a imagem do avatar
                loadSlotButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                loadSlotButton.transform.GetChild(1).gameObject.SetActive(false);
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