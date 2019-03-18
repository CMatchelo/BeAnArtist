using System.Collections;
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
    //public double iniCharCoins;
    public double upgradeValue;
    public double iniUpgradeValue;
    public double coefficient;
    public double level;
    public GameObject upgProfitActive;
    public GameObject upgProfitActiveTXT;
    public GameObject upgProfitLocked;
    public GameObject upgProfitLockedTXT;
    public double qtyCoins;

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
        upgProfitActiveTXT.GetComponent<Text>().text = "Make 2 coins by clicking - $" + upgradeValue;
        upgProfitLockedTXT.GetComponent<Text>().text = "Make 2 coins by clicking - $" + upgradeValue;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("level", (float)level);
    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        //double nextCoins = iniCharCoins * level + 2;
        if(level>0)
        {
            upgProfitActiveTXT.GetComponent<Text>().text = "Make " + (charCoins + 1) + " coins by clicking - $" + upgradeValue;
            upgProfitLockedTXT.GetComponent<Text>().text = "Make " + (charCoins + 1) + " coins by clicking - $" + upgradeValue;
        }
        if (qtyCoins >= upgradeValue)
        {
            upgProfitActive.SetActive(true);
            upgProfitLocked.SetActive(false);
        }
        else
        {
            upgProfitActive.SetActive(false);
            upgProfitLocked.SetActive(true);
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
        //upgradeValue = iniUpgradeValue * aux;
        upgradeValue = System.Math.Round(iniUpgradeValue * aux, 2);
        charCoins = level+1;
    }
}
