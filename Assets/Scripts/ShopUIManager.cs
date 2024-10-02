using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public ShopManager shopManager;

    public void BuyItem(ItemData item)
    {
        shopManager.SelectItem(item);
        shopManager.BuySelectedItem();
    }
}
