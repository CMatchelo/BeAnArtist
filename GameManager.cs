using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls UI 
 * Controls QTY coins
 * */
public class GameManager : MonoBehaviour
{
    public static double coinsCount; // Total of coins the player has
    public GameObject coinsDisplay;
    public GameObject autocoinsStats;
    public double coinsInternal;
    public static double cps;      // Coins per second the player is making

    // Update is called once per frame
    void Update()

    {
        coinsInternal = System.Math.Round(coinsCount,2);
        coinsDisplay.GetComponent<Text>().text = "teste " + coinsInternal;
        autocoinsStats.GetComponent<Text>().text = "Sellings @: " + cps;
    }
}
