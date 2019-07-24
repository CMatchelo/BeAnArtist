using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

/*
 * Controls UI 
 * Controls QTY coins
 * */
public class GameManager : MonoBehaviour
{
    public static double coinsCount = 0; // Total of coins the player has
    public GameObject coinsDisplay;
    public GameObject autocoinsStats;
    public double coinsInternal;
    public static double cps;      // Coins per second the player is making
    public static int ptcCoinsCount = 0;
    public int ptcCoinsCountInternal = 0;

    void Start()
    {
        LoadValues();
        coinsCount = coinsInternal;
    }

        void OnApplicationQuit()
    {
        print(coinsCount + " " + coinsInternal);
        coinsInternal = coinsCount;
        SaveValues();
    }

    // Update is called once per frame
    void Update()
    {
        if (coinsCount > 1000)
        {
            HighValue.CalculatePTC(coinsCount, ptcCoinsCount, out coinsCount, out ptcCoinsCount);
        }
        coinsInternal = System.Math.Round(coinsCount,2);
        coinsDisplay.GetComponent<Text>().text = "" + coinsInternal + " " + HighValue.values[ptcCoinsCount];
        autocoinsStats.GetComponent<Text>().text = "Sellings @: " + cps;
    }

    public void SaveValues()
    {
        string path = Path.Combine(Application.persistentDataPath, "gameManager.value");
        SaveSystem.SaveGameManager(this, path);
    }

    public void LoadValues()
    {
        string path = Path.Combine(Application.persistentDataPath, "gameManager.value");
        GameManagerData data = SaveSystem.LoadGameManager(path);

        coinsInternal = data.coinsInternal;
        ptcCoinsCountInternal = data.ptcCoinsCountInternal;
    }
}
