﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

// 139 38 14
/*
 * Controls UI 
 * Controls QTY coins
 * */
public class GameManager : MonoBehaviour
{
    public static double coinsCount = 0; // Total of coins the player has
    public GameObject coinsDisplay;
    public GameObject autocoinsStats;
    public GameObject levelDisplay;
    public GameObject multQtyBTN;
    public GameObject multQtyTXT;
    public double coinsInternal;
    public static double cps;      // Coins per second the player is making
    public static int cpsPTC=0;
    public static int ptcCoinsCount = 0;
    public int ptcCoinsCountInternal = 0;
    public static int levelGeral = 0;
    public int levelGerAux;
    public static int multQty=1;

    DateTime oldDate;

    void Start()
    {
        long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));
        DateTime oldDate = DateTime.FromBinary(temp);
        var diffInSeconds = (System.DateTime.Now - oldDate).TotalSeconds;
        print("Difference: " + diffInSeconds + " " + cps);
        LoadValues();
        if (cps > 1000)
        {
            HighValue.CalculatePTC(cps, 0, out cps, out int ptcOut);
        }
        coinsCount += cps; ///////////////////
        HighValue.MakeMoney(cps, 0, out cps, out cpsPTC);
        print(cps);
        coinsCount += cps * diffInSeconds;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
        SaveValues();
    }

    // Update is called once per frame
    void Update()
    {
        if (coinsCount > 1000)
        {
            HighValue.CalculatePTC(coinsCount, ptcCoinsCount, out coinsCount, out ptcCoinsCount);
        }
        coinsDisplay.GetComponent<Text>().text = "" + System.Math.Round(coinsCount, 2) + " " + HighValue.values[ptcCoinsCount];
        autocoinsStats.GetComponent<Text>().text = "Sellings @: " + cps + " " + cpsPTC + " " + CharManager.displayCoinsClick;
        //levelDisplay.GetComponent<Text>().text = "Rede Social: " + LevelManager.social[levelGeral%3] + " Country: " + LevelManager.country[levelGeral/3];
    }

    public void SaveValues()
    {
        levelGerAux = levelGeral;
        coinsInternal = coinsCount;
        ptcCoinsCountInternal = ptcCoinsCount;
        string path = Path.Combine(Application.persistentDataPath, "gameManager.value");
        SaveSystem.SaveGameManager(this, path);
    }

    public void LoadValues()
    {
        string path = Path.Combine(Application.persistentDataPath, "gameManager.value");
        GameManagerData data = SaveSystem.LoadGameManager(path);
        levelGeral = levelGerAux;
        coinsCount = data.coinsInternal;
        ptcCoinsCount = data.ptcCoinsCountInternal;
    }
}
