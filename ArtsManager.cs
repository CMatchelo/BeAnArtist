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
            decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(costDecrease, 2)) + " " + HighValue.values[ptcDecrease];
            activeTXT.GetComponent<Text>().text = "Boost post - $" + System.Math.Round(upgrade_cost, 2) + " " + HighValue.values[ptcUpgrade];
            makeProfit = true;
            StartCoroutine(makeMoney());
        }
        else
        {
            activeTXT.GetComponent<Text>().text = "Post art - $" + System.Math.Round(initialCost, 2) + " " + HighValue.values[ptcUpgrade];
            decreaseTimeTXT.GetComponent<Text>().text = "Post your art before upgrade it";
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
        // Verifies if art is active
        if (level > 0 && makeProfit == false)
        {
            makeProfit = true;
            StartCoroutine(makeMoney());
        }
        //Verifies if enough money to upgrade art
        if (ptcUpgrade < ptcQtyCoins) 
        {
            activeBTN.interactable = true;
        }
        else if (qtyCoins >= upgrade_cost && ptcUpgrade == ptcQtyCoins)
        {
            activeBTN.interactable = true;
        }
        else
        {
            activeBTN.interactable = false;
        }
       
        // Verifies if enough money to downgrade time to profit
        if (ptcDecrease < ptcQtyCoins && makeProfit == true)
        {
            decreaseTimeBTN.interactable = true;
        }
        else if (qtyCoins >= costDecrease && ptcDecrease == ptcQtyCoins && makeProfit == true)
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
        double producOld = produc;
        int ptcProducOld = ptcProduc;
        HighValue.SubtractMoney(GameManager.coinsCount, upgrade_cost, GameManager.ptcCoinsCount, ptcUpgrade, out GameManager.coinsCount, out GameManager.ptcCoinsCount);
        level++;
        double aux = System.Math.Pow(Coefficient, level);
        upgrade_cost = initialCost * (aux);
        if (upgrade_cost >= 1000)
        {
            HighValue.CalculatePTC(upgrade_cost, 0, out upgrade_cost, out ptcUpgrade);
        }
        produc = (iniProduc * level);
        if (produc >= 1000)
        {
            HighValue.CalculatePTC(produc, 0, out produc, out ptcProduc);
        }
        GameManager.cps += (produc- producOld); //////////////////////////////////
        activeTXT.GetComponent<Text>().text = "Boost post - $" + System.Math.Round(upgrade_cost, 2) + " " + HighValue.values[ptcUpgrade];
        decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(costDecrease, 2)) + " " + HighValue.values[ptcDecrease];
        CalculateCPS(currentTime, produc, ptcProduc, out GameManager.cps, out GameManager.cpsPTC);
    }

    //
    //
    // Decrease time to profit - Faster time unit
    public void DecreaseTimeProfit()
    {
        levelDecrease++;
        HighValue.SubtractMoney(GameManager.coinsCount, costDecrease, GameManager.ptcCoinsCount, ptcDecrease, out GameManager.coinsCount, out GameManager.ptcCoinsCount);
        double aux = System.Math.Pow(Coefficient, levelDecrease);
        costDecrease = iniCostDecrease*aux;
        if (costDecrease > 1000)
        {
            HighValue.CalculatePTC(costDecrease, 0, out costDecrease, out ptcDecrease);
        }
        currentTime = time/levelDecrease;
        decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(costDecrease, 2)) + " " + HighValue.values[ptcDecrease];
        CalculateCPS(currentTime, produc, ptcProduc, out GameManager.cps, out GameManager.cpsPTC);
    }
    //
    //
    //
    public void CalculateCPS(double time, double coins, int ptc, out double cps, out int cpsPTC)
    {
        double cpsAux=0;
        coins = coins * System.Math.Pow(1000, ptc);
        cps = coins / time;
        if (cps > 1000)
        {
            HighValue.CalculatePTC(cps, 0, out cpsAux, out ptc);
        }
        cps = cpsAux;
        cpsPTC = ptc;
        HighValue.MakeMoney(cps, cpsPTC, out double qtyOut, out int ptcOut);
        print(qtyOut);
        GameManager.cps = GameManager.cps + qtyOut;
        if (GameManager.cps > 1000)
        {
            HighValue.CalculatePTC(GameManager.cps, GameManager.cpsPTC, out GameManager.cps, out GameManager.cpsPTC);
        }
        print("calculated cps" + GameManager.cps + " " + GameManager.cpsPTC);
    }
    //
    //
    // Auto_money_maker
    IEnumerator makeMoney()
    {
        yield return new WaitForSeconds((float)currentTime);
        HighValue.MakeMoney(produc, ptcProduc, out double qtyOut, out int ptcOut);
        GameManager.coinsCount += qtyOut;
        print("arte" + id + " made " + produc + " coins");
        makeProfit = false;
    }
    //
    //
    // Save & Load functions
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
