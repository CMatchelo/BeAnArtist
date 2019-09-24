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
    public GameObject levelDisplay;
    public GameObject multQtyBTN;
    public GameObject multQtyTXT;
    public double coinsInternal;
    public static double cps;      // Coins per second the player is making
    public static int levelGeral = 0;
    public int levelGerAux;
    public int multQty = 0;
    public static double followers = 0;

    DateTime oldDate;

    public void MultQty()
    {
        multQty++;
        if (multQty > 2)
        {
            multQty = 0;
        }
        print("aqui");
        multQtyTXT.GetComponent<Text>().text = "x" + HighValue.multQty[multQty];
    }

    void Start()
    {
        coinsCount = coinsInternal;
        long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));
        DateTime oldDate = DateTime.FromBinary(temp);
        var diffInSeconds = (System.DateTime.Now - oldDate).TotalSeconds;
        print("Difference: " + diffInSeconds + " " + cps);
        LoadValues();
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
        SumFollowers();
        HighValue.CalculatePTC(coinsCount, out double showCoinsCount, out int ptcOut);
        coinsDisplay.GetComponent<Text>().text = "" + System.Math.Round(showCoinsCount, 2) + " " + HighValue.values[ptcOut];
        HighValue.CalculatePTC(cps, out cps, out int cpsPTC);
        autocoinsStats.GetComponent<Text>().text = "Sellings @: " + cps + " " + cpsPTC + " " + CharManager.displayCoinsClick;
        levelDisplay.GetComponent<Text>().text = "Rede Social: " + LevelManager.social[levelGeral%3] + " Country: " + LevelManager.country[levelGeral/3];
    }

    public void SumFollowers()
    {
        followers += System.Math.Round(150 * System.Math.Sqrt(GameManager.coinsCount / System.Math.Pow(10, 15)), 0);
    }

    public void SaveValues()
    {
        levelGerAux = levelGeral;
        coinsInternal = coinsCount;
        string path = Path.Combine(Application.persistentDataPath, "gameManager.value");
        SaveSystem.SaveGameManager(this, path);
    }

    public void LoadValues()
    {
        string path = Path.Combine(Application.persistentDataPath, "gameManager.value");
        GameManagerData data = SaveSystem.LoadGameManager(path);
        levelGeral = levelGerAux;
        coinsCount = data.coinsInternal;
    }
}
