using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopConfirmMenu : MonoBehaviour {

    private ShopManager shopManager;

    private void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void ConfirmBuy()
    {
        shopManager.ConfirmTrade();
    }
}
