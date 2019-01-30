using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    public ItemInfo itemInfo;
    private ShopManager shopManager;

    private void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = itemInfo.itemInfo;
        transform.GetChild(1).GetComponent<Image>().sprite = itemInfo.itemSprite;
        transform.GetChild(1).GetComponent<Image>().color = itemInfo.itemColor;
        transform.GetChild(3).GetComponent<Text>().text = itemInfo.itemPrice.ToString();
        shopManager = GameObject.FindObjectOfType<ShopManager>();
    }

    public void BuyButton()
    {
        shopManager.chosenItem = GridToIndex();
        shopManager.CallConfirmMenu();
    }
    
    private int GridToIndex()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3 wcellPosition = transform.position;
        Vector3Int cellPosition = gridLayout.WorldToCell(wcellPosition);
        return ((cellPosition.x - 40) / 70 - 3 * ((cellPosition.y + 55) / 100));
    }

    public void ItemSold()
    {
        this.transform.GetChild(5).GetComponent<Button>().interactable = false;
        this.transform.GetChild(4).GetComponent<Image>().enabled = true;
    }
}
