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
    public static int ptcCoinsCount = 0;

    void Awake()
    {
        PlayerPrefs.DeleteAll(); // DELETE ALL PREFS LINE *********
        if (PlayerPrefs.HasKey("coinsSaved"))
        {
            float aux = PlayerPrefs.GetFloat("coinsSaved");
            coinsCount = (float)aux;
            //
            aux = PlayerPrefs.GetFloat("cpsSaved");
            cps = (float)aux;
        }
        else
        {
            coinsCount = 0;
        }
    }

    void OnApplicationQuit()
    {
        //int aux = (int)coinsCount;
        PlayerPrefs.SetFloat("coinsSaved", (float)coinsCount);
        PlayerPrefs.SetFloat("cpsSaved", (float)cps);
    }

    // Update is called once per frame
    void Update()
    {
        if (coinsCount > 1000) //////////////////////// if high value
        {
            HighValue.CalculatePTC(&coinsCount, &ptcCoinsCount);
            print("entrou no if");
        }
        coinsInternal = System.Math.Round(coinsCount,2);
        coinsDisplay.GetComponent<Text>().text = "" + coinsInternal + " " + HighValue.values[ptcCoinsCount];
        autocoinsStats.GetComponent<Text>().text = "Sellings @: " + cps;
    }
}
