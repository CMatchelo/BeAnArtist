using System.Collections;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls Art1`s upgrades
 * */
public class UpgradeBTN : MonoBehaviour
{
    //public GameObject textBox;
    // upgrade_cost  = initialCost * (coefficient)^owned
    // produc = (iniProduc * owned) * multipliers
    public int id;
    public GameObject statusBox;
    public double initialCost;
    public double upgrade_cost;
    public double Coefficient;
    public float time;
    public double iniRevenue;
    public double iniProduc;
    public double produc;
    
    public double level;
    public GameObject activeBTN;
    public GameObject activeTXT;
    public GameObject lockedBTN;
    public GameObject lockedTXT;
    public double qtyCoins;
    public bool makeProfit = false;
    
    public float timeDecrease;
    public double costDecrease;
    public GameObject decreaseTimeBTN;
    public GameObject decreaseTimeTXT;
    public GameObject lockeddecreaseTimeBTN;
    public GameObject lockeddecreaseTimeTXT;

    void Start()
    {
        activeTXT.GetComponent<Text>().text = "Post art - $" + initialCost;
        lockedTXT.GetComponent<Text>().text = "Post art - $" + initialCost;
    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        if (level > 0)
        {
            double aux = System.Math.Round(upgrade_cost, 2);
            activeTXT.GetComponent<Text>().text = "Boost post - $" + aux;
            lockedTXT.GetComponent<Text>().text = "Boost post - $" + aux;
        }

        decreaseTimeTXT.GetComponent<Text>().text       = "Sell your art" + id + " " + timeDecrease + " seconds faster - $" + costDecrease;
        lockeddecreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " " + timeDecrease + " seconds faster - $" + costDecrease;

        if (qtyCoins >= upgrade_cost)
        {
            activeBTN.SetActive(true);
            lockedBTN.SetActive(false);
        }
        else
        {
            activeBTN.SetActive(false);
            lockedBTN.SetActive(true);
        }
        // Verifies if art is active
        if (level > 0 && makeProfit == false)
        {
            makeProfit = true;
            StartCoroutine(makeMoney());
        }

        if (qtyCoins >= costDecrease && time > 1 && makeProfit == true)
        {
            decreaseTimeBTN.SetActive(true);
            lockeddecreaseTimeBTN.SetActive(false);
        }
        else
        {
            decreaseTimeBTN.SetActive(false);
            lockeddecreaseTimeBTN.SetActive(true);
        }
    }
    //
    //
    // Upgrade when click
    // upgrade_cost  = initialCost * (coefficient)^owned
    // produc = (iniProduc * owned) * multipliers
    public void ClickButton()
    {
        if(GameManager.coinsCount >= upgrade_cost)
        {
            GameManager.coinsCount -= upgrade_cost;
            level++;
            double aux = System.Math.Pow(Coefficient, level);
            upgrade_cost = initialCost * (aux);
            produc = (iniProduc * level);
            print(level);
            GameManager.cps += produc;
        }  
    }
    //
    //
    // Auto_money_maker
    IEnumerator makeMoney()
    {
        GameManager.coinsCount += produc;
        print("arte" + id + " made " + produc + " coins");
        yield return new WaitForSeconds(time);
        makeProfit = false;
    }
    //
    //
    // Decrease time to profit
    public void DecreaseTimeProfit()
    {
        if (time > 1)
        {
            GameManager.coinsCount -= costDecrease;
            costDecrease *= 2;
            time -= timeDecrease;
        }
        
        if (time < 1)
        {
            time = 1;
        }
    }
    //
    //
    // 
}