/*
All code that has a comment saying it was learned from youtube video is referencing this specific video:
"How to make a simple Shop in Unity" 
Author: Zyger
Date: Oct 13, 2020
*/

using System.Collections; // learned from youtube video
using System.Collections.Generic; // learned from youtube video
using UnityEngine; // learned from youtube video
using UnityEngine.UI; // learned from youtube video
using TMPro; // learned from youtube video

public class ButtonInfo : MonoBehaviour // placed by default
{
    public int ItemID;
    public TMP_Text PriceTXT; 
    public TMP_Text QuantityTXT; 
    public GameObject ShopManager;

    void Start() {
        Debug.Log(transform.name);
    }

    // learned how to do this from youtube video
    void Update()
    {
        PriceTXT.text = "Price: \nS/ " + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString() + ".00"; // learned GetComponent from youtube video
        QuantityTXT.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString(); // learned GetComponent from youtube video
    }
}