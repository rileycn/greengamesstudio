using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TMP_Text PriceTXT; 
    public TMP_Text QuantityTXT; 
    public GameObject ShopManager;

    void Start() {
        Debug.Log(transform.name);
    }

    void Update()
    {
        PriceTXT.text = "Price: \nS/ " + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString() + ".00";
        QuantityTXT.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();
    }
}