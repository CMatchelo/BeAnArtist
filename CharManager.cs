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
    public int ptcCharCoins = 0;
    public int ptcUpgradeValue = 0;
    public double charCoins; // Quantidade de coins por click no char
    public double upgradeValue;
    public double iniUpgradeValue;
    public double coefficient;
    public double level;
    public Button upgProfitActive;
    public GameObject upgProfitActiveTXT;
    public double qtyCoins;
    public int ptcQtyCoins;
    public double displayUpgradeValue;
    public double displayCharCoins;


    public void SaveCharValues()
    {
        string path = Path.Combine(Application.persistentDataPath, "char.value");
        SaveSystem.SaveChar(this, path);
    }

    public void LoadCharValues()
    {
        string path = Path.Combine(Application.persistentDataPath, "char.value");
        CharData data = SaveSystem.LoadChar(path);

        ptcCharCoins = data.ptcCharCoins;
        ptcUpgradeValue = data.ptcUpgradeValue;
        level = data.level;
        charCoins = data.charCoins;
        upgradeValue = data.upgradeValue;
    }

    void Start()
    {
        LoadCharValues();
        upgProfitActiveTXT.GetComponent<Text>().text = "Make +1 coins by clicking - $" + System.Math.Round(upgradeValue, 2);
    }

    void OnApplicationQuit()
    {
        SaveCharValues();
    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        ptcQtyCoins = GameManager.ptcCoinsCount;
        upgProfitActiveTXT.GetComponent<Text>().text = "Make +1 coins by clicking - $" + System.Math.Round(displayUpgradeValue, 2) + " " + HighValue.values[ptcUpgradeValue];
        // Verifies if enough money to buy upgrade
        if ((ptcUpgradeValue < ptcQtyCoins))
        {
            upgProfitActive.interactable = true;
        }
        else if (qtyCoins >= upgradeValue && ptcUpgradeValue == ptcQtyCoins)
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
        GameManager.coinsCount +=  charCoins;
    }

    public void UpgradeProfit()
    {
        //GameManager.coinsCount -= upgradeValue; ///////// Spent
        GameManager.coinsCount = GameManager.coinsCount - upgradeValue;
        level++;
        double aux = System.Math.Pow(coefficient, level);
        upgradeValue = System.Math.Round(iniUpgradeValue * aux, 2);
        if (upgradeValue > 1000) //////////////////////// if high value
        {
            HighValue.CalculatePTC(upgradeValue, 0, out displayUpgradeValue, out ptcUpgradeValue);
            print("entrou");
        }
        charCoins += 1;
        if (charCoins > 1000) //////////////////////// if high value
        {
            HighValue.CalculatePTC(charCoins, ptcCharCoins, out displayCharCoins, out ptcCharCoins);
            print("entrou2");
        }
    }
}
