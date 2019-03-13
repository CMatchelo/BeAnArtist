using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls clicks @ mains char
 * */
public class charBTN : MonoBehaviour
{
    //public GameObject textBox;
    public double charCoins; // Quantidade de coins por click no char
    public double upgradeValue;
    public GameObject upgProfitActive;
    public GameObject upgProfitActiveTXT;
    public GameObject upgProfitLocked;
    public GameObject upgProfitLockedTXT;
    public double qtyCoins;

    void Start()
    {
    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        upgProfitActiveTXT.GetComponent<Text>().text = "Make " + charCoins*2 + " coins by clicking - $" + upgradeValue;
        upgProfitLockedTXT.GetComponent<Text>().text = "Make " + charCoins*2 + " coins by clicking - $" + upgradeValue;
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
        upgradeValue *= 2;
        charCoins *= 2;
    }
}
