using System;
using System.Collections;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls clicks @ mains char
 * */
public class CharBTN : MonoBehaviour
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
    
    public void SaveCharValues()
    {
        CharSave.SaveChar(this);
    }

    public void LoadCharValues()
    {
        CharData data = CharSave.LoadChar();

        ptcCharCoins = data.ptcCharCoins;
        ptcUpgradeValue = data.ptcUpgradeValue;
        level = data.level;
        charCoins = data.charCoins;
        upgradeValue = data.upgradeValue;
    }

    void Awake()
    {
        LoadCharValues();
        /*PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("level"))
        {
            float aux = PlayerPrefs.GetFloat("level", 0);
            level = (double)aux;
        }*/
    }
    void Start()
    {
        double aux = System.Math.Pow(coefficient, level);
        upgradeValue = iniUpgradeValue * aux;
        upgProfitActiveTXT.GetComponent<Text>().text = "Make +1 coins by clicking - $" + System.Math.Round(upgradeValue, 2);
    }

    void OnApplicationQuit()
    {
        //PlayerPrefs.SetFloat("level", (float)level);
        SaveCharValues();
    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        ptcQtyCoins = GameManager.ptcCoinsCount;
        //double nextCoins = iniCharCoins * level + 2;
        if (level>0)
        {
            upgProfitActiveTXT.GetComponent<Text>().text = "Make +1 coins by clicking - $" + System.Math.Round(upgradeValue, 2);
        }
        // Verifies if enough money to buy upgrade
        if ((ptcCharCoins < ptcQtyCoins))
        {
            upgProfitActive.interactable = false;
        }
        else if (qtyCoins >= upgradeValue)
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
        //GameManager.coinsCount += charCoins; // Ganha coins ao clicar no char
        HighValue.makeMoney(charCoins, ptcCharCoins);
    }

    public void UpgradeProfit()
    {
        GameManager.coinsCount -= upgradeValue; ///////// Spent
        level++;
        double aux = System.Math.Pow(coefficient, level);
        upgradeValue = System.Math.Round(iniUpgradeValue * aux, 2);
        if (upgradeValue > 1000) //////////////////////// if high value
        {
            HighValue.CalculatePTC(upgradeValue, ptcUpgradeValue, out upgradeValue, out ptcUpgradeValue);
        }
        charCoins += 1;
        if (charCoins > 1000) //////////////////////// if high value
        {
            HighValue.CalculatePTC(charCoins, ptcCharCoins, out charCoins, out ptcCharCoins);
        }
    }
}
