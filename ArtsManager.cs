using System;
using System.Collections;
//using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls Art`s upgrades
 * */
public class ArtsManager : MonoBehaviour
{
    public int id;
    public double qtyCoins;
    public int ptcQtyCoins;
    public bool makeProfit = false;
    public double level;

    public double initialCost;
    public double upgrade_cost;
    public double Coefficient;
    public double iniProduc;
    public double produc;
    public int ptcUpgrade = 0;
    public int ptcProduc = 0;
    public Button activeBTN;
    public GameObject activeTXT;

    public double time;
    public double currentTime;
    public double costDecrease;
    public double iniCostDecrease;
    public double levelDecrease=1;
    public int ptcDecrease = 0;
    public Button decreaseTimeBTN;
    public GameObject decreaseTimeTXT;
    
    void OnApplicationQuit()
    {
        SaveValues();
    }

    void Start()
    {
        LoadValues();
        qtyCoins = GameManager.coinsCount;
        ptcQtyCoins = GameManager.ptcCoinsCount;
        if (level > 0)
        {
            activeTXT.GetComponent<Text>().text = "Boost post - $" + System.Math.Round(upgrade_cost, 2);
            makeProfit = true;
            StartCoroutine(makeMoney());
        }
        else
        {
            activeTXT.GetComponent<Text>().text = "Post art - $" + initialCost;
        }
        if (levelDecrease > 0)
        {
            double aux = System.Math.Pow(Coefficient, levelDecrease);
            costDecrease = iniCostDecrease * aux;
        }
        if (levelDecrease == 0)
        {
            currentTime = time;
        }
    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        ptcQtyCoins = GameManager.ptcCoinsCount;
        if (level > 0)
        {
            double aux = System.Math.Round(upgrade_cost, 2);
            activeTXT.GetComponent<Text>().text = "Boost post - $" + aux;
        }
        //
        decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(costDecrease, 2));
        //
        // Verifies if art is active
        if (level > 0 && makeProfit == false)
        {
            makeProfit = true;
            StartCoroutine(makeMoney());
        }
        //Verifies if enough money to upgrade art
        if (ptcUpgrade < ptcQtyCoins)
        {
            activeBTN.interactable = false;
        }
        else if (qtyCoins >= upgrade_cost)
        {
            activeBTN.interactable = true;
        }
        else
        {
            activeBTN.interactable = false;
        }
       
        // Verifies if enough money to downgrade time to profit
        if (ptcDecrease < ptcQtyCoins)
        {
            decreaseTimeBTN.interactable = false;
        }
        else if (qtyCoins >= costDecrease && makeProfit == true)
        {
            decreaseTimeBTN.interactable = true;
        }
        else
        {
            decreaseTimeBTN.interactable = false;
        }
    }
    //
    //
    // Upgrade when clicked - More money by time unit
    // upgrade_cost  = initialCost * (coefficient)^owned
    // produc = (iniProduc * owned) * multipliers
    public void ClickButton()
    {
        double produc_old = produc;
        GameManager.coinsCount -= upgrade_cost; ////////////////////////////////// subtract money
        level++;
        double aux = System.Math.Pow(Coefficient, level);
        upgrade_cost = initialCost * (aux);
        if (upgrade_cost >= 1000)
        {
            HighValue.CalculatePTC(upgrade_cost, ptcUpgrade, out upgrade_cost, out ptcUpgrade);
        }
        produc = (iniProduc * level);
        GameManager.cps += (produc-produc_old); //////////////////////////////////
    }
    //
    //
    // Auto_money_maker
    IEnumerator makeMoney()
    {
        GameManager.coinsCount += produc;
        print("arte" + id + " made " + produc + " coins");
        yield return new WaitForSeconds((float)currentTime);
        makeProfit = false;
    }
    //
    //
    // Decrease time to profit - Faster time unit
    public void DecreaseTimeProfit()
    {
        levelDecrease++;
        GameManager.coinsCount -= costDecrease;
        double aux = System.Math.Pow(Coefficient, levelDecrease);
        costDecrease = iniCostDecrease*aux;
        if (costDecrease > 1000) //////////////////////// if high value
        {
           HighValue.CalculatePTC(costDecrease, ptcDecrease, out costDecrease, out ptcDecrease);
        }
        currentTime = time/levelDecrease;
        decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(costDecrease, 2)) + " " + HighValue.values[ptcDecrease];
    }
    //
    //
    //
    public void SaveValues()
    {
        string path = Path.Combine(Application.persistentDataPath, id+"artsManager.value");
        SaveSystem.SaveArts(this, path);
    }

    public void LoadValues()
    {
        string path = Path.Combine(Application.persistentDataPath, id+"artsManager.value");
        ArtsData data = SaveSystem.LoadArts(path);

        upgrade_cost = data.upgrade_cost;
        produc = data.produc;
        ptcUpgrade = data.ptcUpgrade;
        ptcProduc = data.ptcProduc;
        level = data.level;
        costDecrease = data.costDecrease;
        ptcDecrease = data.ptcDecrease;
        levelDecrease = data.levelDecrease;
        currentTime = data.currentTime;
    }
}
