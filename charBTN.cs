using System;
using System.Collections;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls clicks @ mains char
 * */
public class charBTN : MonoBehaviour
{

    // upgrade_cost  = initialCost * (coefficient)^owned
    // produc = (iniProduc * owned) * multipliers
    public double charCoins; // Quantidade de coins por click no char
    public double upgradeValue;
    public double iniUpgradeValue;
    public double coefficient;
    public double level;
    public Button upgProfitActive;
    public GameObject upgProfitActiveTXT;
    public double qtyCoins;
    public double ptcQtyCoins;
    public double ptcCharCoins = 0;

    void Awake()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("level"))
        {
            float aux = PlayerPrefs.GetFloat("level", 0);
            level = (double)aux;
        }
    }
    void Start()
    {
        double aux = System.Math.Pow(coefficient, level);
        upgradeValue = iniUpgradeValue * aux;
        upgProfitActiveTXT.GetComponent<Text>().text = "Make +1 coins by clicking - $" + upgradeValue;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("level", (float)level);
    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        ptcQtyCoins = GameManager.ptcCoinsCount;
        //double nextCoins = iniCharCoins * level + 2;
        if (level>0)
        {
            upgProfitActiveTXT.GetComponent<Text>().text = "Make +1 coins by clicking - $" + upgradeValue;
        }
        if (qtyCoins >= upgradeValue && ptcCharCoins >= ptcQtyCoins)
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
        GameManager.coinsCount += charCoins; // Ganha coins ao clicar no char
    }

    public void UpgradeProfit()
    {
        GameManager.coinsCount -= upgradeValue;
        level++;
        double aux = System.Math.Pow(coefficient, level);
        upgradeValue = System.Math.Round(iniUpgradeValue * aux, 2);/*
        if (upgradeValue > 100000)
        {
            upgradeValue = System.Math.Round(upgradeValue / 100000, 2);
            ptcCharCoins += 5;
        }*/
        charCoins = level+1;
    }
}
