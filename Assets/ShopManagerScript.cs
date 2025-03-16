using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{

    public int[,] shopItems = new int[4,4];
    public float coins;
    public int Quantity;
    public TMP_Text CoinsTXT;
    public TMP_Text QuantityTXT;
    public int year = 1;
    
    void Start()
    {
        CoinsTXT.text = "CURRENCY (SOL): S/ " + coins.ToString() + ".00";

        // id
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;

        // Price
        switch (year) {
            case 1: 
                shopItems[2,1] = 4;
                shopItems[2,2] = 5;
                shopItems[2,3] = 6;
                break;
            case 2:
                shopItems[2,1] = 5;
                shopItems[2,2] = 6;
                shopItems[2,3] = 7;
                break;
            case 3:
                shopItems[2,1] = 6;
                shopItems[2,2] = 7;
                shopItems[2,3] = 8;
                break;
            case 4:
                shopItems[2,1] = 7;
                shopItems[2,2] = 8;
                shopItems[2,3] = 9;
                break;
        }

        // Quantity
        shopItems[3,1] = 0;
        shopItems[3,2] = 0;
        shopItems[3,3] = 0;
    

    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID]) {
            coins -= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID]; 
            shopItems[3,ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            CoinsTXT.text = "CURRENCY (SOL): S/ " + coins.ToString() + ".00";
            //ButtonRef.GetComponent<ButtonInfo>().QuantityTXT.text = shopItems[3,ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();

        }
    }
}
