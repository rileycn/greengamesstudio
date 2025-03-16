/*
All code that has a comment saying it was learned from youtube video is referencing this specific video:
"How to make a simple Shop in Unity" 
Author: Zyger
Date: Oct 13, 2020
*/

using System.Collections; // learned from youtube video
using System.Collections.Generic; // learned from youtube video
using UnityEngine; // learned from youtube video
using UnityEngine.EventSystems; // learned from youtube video
using UnityEngine.UI; // learned from youtube video
using TMPro; // learned from youtube video
using UnityEngine.SceneManagement;


public class ShopManagerScript : MonoBehaviour // placed by default
{
    public int[,] shopItems = new int[4,4];
    public float coins;
    public int Quantity;
    public TMP_Text CoinsTXT; // learned from youtube video
    public TMP_Text QuantityTXT; // learned from youtube video
    public int year = 1; // learned from youtube video
    

    
    void Start() // learned from youtube video
    {
        coins = GameInfo.cash;
        year = GameInfo.year;

        CoinsTXT.text = "CURRENCY (SOL): S/ " + coins.ToString() + ".00"; // learned from youtube video

        // id
        shopItems[1,1] = 1; //seed
        shopItems[1,2] = 2; //fert
        shopItems[1,3] = 3; //water

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
        shopItems[3,1] = GameInfo.seed;
        shopItems[3,2] = GameInfo.fert;
        shopItems[3,3] = GameInfo.water;
    

    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject; // learned from youtube video
        if (coins >= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID]) { // learned GetComponent from youtube video
            coins -= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID]; // learned GetComponent from youtube video
            shopItems[3,ButtonRef.GetComponent<ButtonInfo>().ItemID]++; // learned GetComponent from youtube video
            CoinsTXT.text = "CURRENCY (SOL): S/ " + coins.ToString() + ".00";
        }
    }

    public void EndShop()
    {
        GameInfo.year++;
        GameInfo.seed = shopItems[3, 1];
        GameInfo.fert = shopItems[3, 2];
        GameInfo.water = shopItems[3, 3];
        GameInfo.cash = coins;
        SceneManager.LoadScene("GameScene");
    }
}
