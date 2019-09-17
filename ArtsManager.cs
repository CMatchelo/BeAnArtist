using System;
using System.Collections;
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
    public static bool makeProfit = false;
    public double level;
    public double Coefficient;
    public double cps;

    public double upgradeCost;
    public double initialCost;
    public int ptcUpgradeCost;
    public int ptcInitialCost;
    public double showUpgCost;
    public int ptcShowUpgCost;

    public double iniProduc;
    public double produc;
    public int ptcProduc;
    public int ptcIniProduc;

    public Button activeBTN;
    public GameObject activeTXT;
    public GameObject likesTXT;
    public GameObject showProducTXT;

    public double time;
    public double currentTime;
    public double costDecrease;
    public double iniCostDecrease;
    public int ptcDecrease;
    public int iniCostDecreasePtc;
    public double levelDecrease=1;
    
    public Button decreaseTimeBTN;
    public GameObject decreaseTimeTXT;


    void OnApplicationQuit()
    {
        SaveValues();
        print(cps);
    }

    private void Awake()
    {
        LoadValues();
        if (level > 0)
        {
            GameManager.cps += cps;
        }
    }

    void Start()
    {
        //level = 0; //////////////////////// TO DELETE, DELETING SAVED FILES
        qtyCoins = GameManager.coinsCount;
        ptcQtyCoins = GameManager.ptcCoinsCount;
        if (level > 0)
        {
            CalcUpgCost();
            CalcProduc();
            CalcDecreasCost();
            showUpgCost = upgradeCost;
            ptcShowUpgCost = ptcUpgradeCost;
            WriteValues();
            makeProfit = true;
            StartCoroutine(makeMoney());
        }
        else
        {
            makeProfit = false;
            upgradeCost = initialCost;
            ptcUpgradeCost = ptcInitialCost;
            produc = iniProduc;
            ptcProduc = ptcIniProduc;
            activeTXT.GetComponent<Text>().text = "Post - $" + System.Math.Round(initialCost, 2) + " " + HighValue.values[ptcInitialCost];
            decreaseTimeTXT.GetComponent<Text>().text = "Post your art before upgrade it";
            likesTXT.GetComponent<Text>().text = level + "";
            currentTime = time;
        }
        if (levelDecrease == 0 && level > 0)
        {
            decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(iniCostDecrease, 2)) + " " + HighValue.values[iniCostDecreasePtc];
        }
    }


    void MultQty()
    {
        ptcShowUpgCost = ptcUpgradeCost;
        if (GameManager.multQty == 1)
        {
            showUpgCost = upgradeCost;
        }
        if (GameManager.multQty == 10)
        {
            showUpgCost = upgradeCost * 10;
            if(showUpgCost>1000)
            {
                HighValue.CalculatePTC(showUpgCost, ptcShowUpgCost, out showUpgCost, out ptcShowUpgCost);
            }  
        }
        if (GameManager.multQty == 50)
        {
            showUpgCost = upgradeCost * 50;
            if (showUpgCost > 1000)
            {
                HighValue.CalculatePTC(showUpgCost, ptcShowUpgCost, out showUpgCost, out ptcShowUpgCost);
            }
        }
    }

    void CalcMultPrice()
    {
        int i = 0;
        while (i < GameManager.multQty)
        {
            i++;
        }
    }

    void Update()
    {
        makeProfit = false;
        qtyCoins = GameManager.coinsCount;
        ptcQtyCoins = GameManager.ptcCoinsCount;
        // Verifies if art is active
        if (level > 0 && makeProfit == false)
        {
            makeProfit = true;
            StartCoroutine(makeMoney());
        }
        //Verifies if enough money to upgrade art
        if (ptcShowUpgCost < ptcQtyCoins) 
        {
            activeBTN.interactable = true;
        }
        else if (qtyCoins >= showUpgCost && ptcUpgradeCost == ptcShowUpgCost)
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
    public void ClickButton()
    {
        HighValue.SubtractMoney(GameManager.coinsCount, upgradeCost, GameManager.ptcCoinsCount, ptcUpgradeCost, out GameManager.coinsCount, out GameManager.ptcCoinsCount);
        level++;
        CalcUpgCost();
        CalcProduc();
        WriteValues();
        cps = (produc * (System.Math.Pow(1000, ptcProduc))) / currentTime;
        ShowCPS();
    }
    //
    //
    // Decrease time to profit - Faster time unit
    public void DecreaseTimeProfit()
    {
        levelDecrease++;
        HighValue.SubtractMoney(GameManager.coinsCount, costDecrease, GameManager.ptcCoinsCount, ptcDecrease, out GameManager.coinsCount, out GameManager.ptcCoinsCount);
        CalcDecreasCost();
        currentTime = time/levelDecrease;
        WriteValues();
        cps = (produc*(System.Math.Pow(1000,ptcProduc))) / currentTime;
        ShowCPS();
    }
    //
    //
    // Calculate cost to upgrade art
    public void CalcUpgCost()
    {
        double aux = System.Math.Pow(Coefficient, level);
        upgradeCost = initialCost * System.Math.Pow(1000, ptcInitialCost) * (aux);
        if (upgradeCost >= 1000)
        {
            HighValue.CalculatePTC(upgradeCost, ptcUpgradeCost, out upgradeCost, out ptcUpgradeCost);
        }
    }
    //
    //
    // Calculate art produc per cicle
    public void CalcProduc()
    {
        produc = (iniProduc * System.Math.Pow(1000, ptcIniProduc) * level);
        if (produc >= 1000)
        {
            HighValue.CalculatePTC(produc, ptcProduc, out produc, out ptcProduc);
        }
    }
    //
    //
    // Calculate cost to decrease time
    public void CalcDecreasCost()
    {
        double aux = System.Math.Pow(Coefficient, levelDecrease);
        costDecrease = iniCostDecrease * System.Math.Pow(1000, iniCostDecreasePtc) * aux;
        if (costDecrease > 1000)
        {
            HighValue.CalculatePTC(costDecrease, 0, out costDecrease, out ptcDecrease);
        }
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
    // Updating values when level geral increases
    public void UpLevel()
    {
        HighValue.IniStats(initialCost, ptcInitialCost, out initialCost, out ptcInitialCost);
        HighValue.IniStats(iniProduc, ptcIniProduc, out iniProduc, out ptcIniProduc);
        HighValue.IniStats(iniCostDecrease, iniCostDecreasePtc, out iniCostDecrease, out iniCostDecreasePtc);
        upgradeCost = initialCost;
        ptcUpgradeCost = ptcInitialCost;
        produc = iniProduc;
        ptcProduc = ptcIniProduc;
        costDecrease = iniCostDecrease;
        ptcDecrease = iniCostDecreasePtc;
        levelDecrease = 1;
        currentTime = time / levelDecrease; //////////////////////
        makeProfit = false;
        level = 0;
        activeTXT.GetComponent<Text>().text = "Post art - $" + System.Math.Round(initialCost, 2) + " " + HighValue.values[ptcInitialCost];
        decreaseTimeTXT.GetComponent<Text>().text = "Post your art before upgrade it";
    }
    //
    //
    // Write values on text fields
    public void WriteValues()
    {
        decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(costDecrease, 2)) + " " + HighValue.values[ptcDecrease];
        activeTXT.GetComponent<Text>().text = "Boost post - $" + System.Math.Round(upgradeCost, 2) + " " + HighValue.values[ptcShowUpgCost];
        likesTXT.GetComponent<Text>().text = level + " k";
    }
    //
    //
    // Calculates and writes CPS on field
    public void ShowCPS()
    {
        if (currentTime < 1)
        {
            HighValue.CalculatePTC(cps, 0, out double cpsAux, out int ptcOUt);
            showProducTXT.GetComponent<Text>().text = cpsAux + " " + HighValue.values[ptcOUt] + "/ sec";
        }
        else
        {
            showProducTXT.GetComponent<Text>().text = produc + " " + HighValue.values[ptcProduc] + " / " + currentTime + " seg";
        }
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

        level = data.level;
        levelDecrease = data.levelDecrease;
        currentTime = data.currentTime;
        cps = data.cps;
    }
}
