using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    private ToggleGroup toggleGroup;
    public ItemInfo[] avatarsToSell;
    public ItemInfo[] colorsToSell;
    public ItemInfo[] extrasToSell;

    private ShopToggleWindow shopToggleWindow;
    public GameObject itemPrefab;
    private GameObject itemPrefabInstace;
    private GameObject playerMoney;

    public GameObject confirmMenu;

    private string toggleSelected;
    private int chosenItemPrice;
    public int chosenItem;

	void Start () {
        shopToggleWindow = FindObjectOfType<ShopToggleWindow>();
        toggleSelected = shopToggleWindow.SelectedToggle();

        playerMoney = GameObject.Find("PlayerMoney");
        playerMoney.GetComponent<Text>().text = SaveManager.player.dinheiro.ToString();

        Invoke(string.Concat(toggleSelected, "ShopWindow"), 0);
	}
	
	public void AvatarToggleShopWindow()
    {
        ShowItens(avatarsToSell);
    }

    public void CoresToggleShopWindow()
    {
        ShowItens(colorsToSell);
    }

    public void ExtrasToggleShopWindow()
    {
        ShowItens(extrasToSell);
    }

    public void ShowItens(ItemInfo[] itemsToShow)
    {
        DestroyShopItens();
        toggleSelected = shopToggleWindow.SelectedToggle();
        for (int i = 0; i < itemsToShow.Length; i++)
        {
            itemPrefabInstace = Instantiate(itemPrefab, GameObject.Find("Content").transform);
            ShopItem shopItemPrefab = itemPrefabInstace.GetComponent<ShopItem>();
            shopItemPrefab.itemInfo = itemsToShow[i];

            if (toggleSelected.Contains("Avatar") && SaveManager.player.avatares[i + SaveManager.player.numAvataresLiberadso])
            {
                shopItemPrefab.ItemSold();
            }
            else if (toggleSelected.Contains("Cores") && SaveManager.player.cores[i])
            {
                shopItemPrefab.ItemSold();
            }
            else if (toggleSelected.Contains("Extra") && SaveManager.player.extras[i])
            {
                shopItemPrefab.ItemSold();
            }
        }
    }

    private void DestroyShopItens()
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("ShopItem");

        for (int i = 0; i < objectsToDestroy.Length; i++)
        {
            Destroy(objectsToDestroy[i]);
        }
    }

    public void ConfirmTrade()
    {
        Invoke(string.Concat("GetPrice", toggleSelected), 0);

        if (SaveManager.player.dinheiro > chosenItemPrice)
        {
            SaveManager.player.dinheiro -= chosenItemPrice;

            if (toggleSelected.Contains("Avatar"))
            {
                SaveManager.player.avatares[chosenItem + SaveManager.player.numAvataresLiberadso] = true;
                avatarsToSell[chosenItem].availableToBuy = false;
            }
            else if (toggleSelected.Contains("Cores"))
            {
                SaveManager.player.cores[chosenItem] = true;
                colorsToSell[chosenItem].availableToBuy = false;
            }
            else if (toggleSelected.Contains("Extras"))
            {
                SaveManager.player.extras[chosenItem] = true;
                extrasToSell[chosenItem].availableToBuy = false;
            }
        }
        else
        {
            Debug.LogError("Não tem dinheiro suficiente");
        }
        
        SaveManager.Save();
        //playerMoney.GetComponent<Text>().text = SaveManager.player.dinheiro.ToString();
        Invoke(string.Concat(toggleSelected, "ShopWindow"), 0);
        Destroy(GameObject.Find("ConfirmMenuLoja(Clone)"));
    }

    public void GetPriceAvatarToggle()
    {
        chosenItemPrice = avatarsToSell[chosenItem].itemPrice;
    }

    public void GetPriceCoresToggle()
    {
        chosenItemPrice = colorsToSell[chosenItem].itemPrice;
    }

    public void GetPriceExtrasToggle()
    {
        chosenItemPrice = extrasToSell[chosenItem].itemPrice;
    }

    public void CallConfirmMenu()
    {
        GameObject confirmMenuInstance = Instantiate(confirmMenu, GameObject.Find("Canvas").transform);
    }
}
