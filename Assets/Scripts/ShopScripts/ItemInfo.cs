using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInfo {

    public string itemInfo;
    public int itemPrice = 200;
    public Sprite itemSprite;
    public Color itemColor;
    public bool availableToBuy = true;
}
