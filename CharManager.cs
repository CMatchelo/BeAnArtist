using System;
using System.Collections;
//using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls clicks @ mains char
 * */
public class CharManager : MonoBehaviour
{

    // upgrade_cost  = initialCost * (coefficient)^owned
    // produc = (iniProduc * owned) * multipliers

    public double iniUpgradeValue;
    public double upgradeValue;
    public double charCoins; // Quantidade de coins por click no char
    public double coefficient;
    public double level;

    public Button upgProfitActive;
    public GameObject upgProfitActiveTXT;

    public double qtyCoins;
    public static double displayCoinsClick;


    void Start()
    {
        // adicionar para quando subir de level
        // charCoins *= System.Math.Pow(10, GameManager.levelGeral);
        LoadCharValues();
        double aux = System.Math.Pow(coefficient, level);
        upgradeValue = iniUpgradeValue * aux;
        HighValue.CalculatePTC(upgradeValue, out upgradeValue, out int ptcUpgValue);
        upgProfitActiveTXT.GetComponent<Text>().text = "Make +1 coins by clicking - $" + System.Math.Round(upgradeValue, 2) + " " + HighValue.values[ptcUpgValue];
    }

    void OnApplicationQuit()
    {
        SaveCharValues();
    }

    void Update()
    {
        //Verifies if enough money to buy upgrade
        if (GameManager.coinsCount >= upgradeValue)
        {
            upgProfitActive.interactable = true;
        }
        else
        {
            upgProfitActive.interactable = false;
        }
    }

    public void ClickButton()
    {
        GameManager.coinsCount += charCoins;
    }

    public void UpgradeProfit()
    {
        GameManager.coinsCount -= upgradeValue;
        level++;
        double aux = System.Math.Pow(coefficient, level);
        upgradeValue = iniUpgradeValue * aux;
        charCoins++;
        displayCoinsClick = charCoins;
        HighValue.CalculatePTC(upgradeValue, out double showUpgValue, out int ptcUpgradeValue);
        upgProfitActiveTXT.GetComponent<Text>().text = "Make +1 coins by clicking - $" + System.Math.Round(showUpgValue, 2) + " " + HighValue.values[ptcUpgradeValue];
    }

    public void SaveCharValues()
    {
        string path = Path.Combine(Application.persistentDataPath, "char.value");
        SaveSystem.SaveChar(this, path);
    }

    public void LoadCharValues()
    {
        string path = Path.Combine(Application.persistentDataPath, "char.value");
        CharData data = SaveSystem.LoadChar(path);

        level = data.level;
        charCoins = data.charCoins;
        upgradeValue = data.upgradeValue;
        displayCoinsClick = charCoins;
    }
    /*
    public void UpLevel()
    {
        HighValue.IniStats(iniUpgradeValue, IniPTCUpgradeValue, out iniUpgradeValue, out IniPTCUpgradeValue);
        ptcUpgradeValue = IniPTCUpgradeValue;
        upgradeValue = iniUpgradeValue;
        charCoins = 10* GameManager.levelGeral;
        iniUpgradeValue *= (System.Math.Pow(10, GameManager.levelGeral));
        level = 0;
    }*/
}
