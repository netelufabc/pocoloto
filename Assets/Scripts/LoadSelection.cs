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

    void Start () {

        // Caminho padrão do local de save
        dataPath = SaveManager.dataPath;

        // Usado para contar quantos slots de save estão sendo usados
        int slotsUsados = SaveManager.slotsListSize;

        // Percorre todos os possíveis save slots
        for (int i = 0; i < SaveManager.slotsListSize; i++)
        {
            // Verifica se o slot está sendo usado (não está na lista)
            if (System.IO.File.Exists(dataPath + "/listaDeSlots.json") && !SlotsListManager.CheckSameNumber(i, SaveManager.list))
            {
                // Instancia o botão
                loadSlotButton = Instantiate(loadSlotButton, transform);
                // Carrega o player para obter o nome
                SaveManager.Load(i);
                // Coloca as informações do save no botão de load
                loadSlotButton.GetComponentInChildren<Text>().text = "  Load Slot " + i + "\n  Player Name: " + SaveManager.player.nome;
                loadSlotButton.GetComponentInChildren<UnityEngine.UI.Button>().interactable = true;
                // Carrega a imagem do avatar para colocar no botão
                Sprite avatar = Resources.Load<Image>("Prefabs/AvatarTeste0" + (SaveManager.player.avatarSelecionadoIndex + 1).ToString()).sprite;
                // Encontra a imagem do botão e muda para o avatar selecionado
                GameObject buttonImage = loadSlotButton.transform.GetChild(1).gameObject;
                buttonImage.GetComponent<Image>().sprite = avatar;

                // Se o slot está sendo usado, adiciona no contador
                slotsUsados--;
            }
            #region Opção de deixar todos os slots vísiveis, bloqueando os não utilizados
            /*
            else
            {
                // Opção de deixar visível todos os slots, mas bloqueando os que não possuem save
                // Podemos colocar a criação do botão dentro do if e só aparece se existir save
                loadSlotButton.GetComponentInChildren<Text>().text = "  Load Slot " + i + "\n  Vazio";
                // Desabilita o botão e remove a imagem do avatar
                loadSlotButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                loadSlotButton.transform.GetChild(1).gameObject.SetActive(false);
            }*/
            #endregion
        }

        // Mostra quantos espaços de save estão livres do total
        GameObject.Find("Slots Livres").GetComponent<Text>().text = "Espaços Livres: " + slotsUsados.ToString() + "/" + SaveManager.slotsListSize;
	}

	/*
    /// <summary>
    /// Função de teste para verificação da posição escolhida
    /// </summary>
    public void AvatarSelecionado()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3 wcellPosition = transform.position;
        Vector3Int cellPosition = gridLayout.WorldToCell(wcellPosition);
        Debug.Log(cellPosition);
    }*/
}