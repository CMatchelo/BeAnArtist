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
        
        //LoadValues();
        //level = 0; //////////////////////// TO DELETE, DELETING SAVED FILES
        qtyCoins = GameManager.coinsCount;
        ptcQtyCoins = GameManager.ptcCoinsCount;
        if (level > 0)
        {
            WriteValues();
            makeProfit = true;
            StartCoroutine(makeMoney());
        }
        else
        {
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
        if (ptcUpgradeCost < ptcQtyCoins) 
        {
            activeBTN.interactable = true;
        }
        else if (qtyCoins >= upgradeCost && ptcUpgradeCost == ptcQtyCoins)
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
        HighValue.SubtractMoney(GameManager.coinsCount, upgradeCost, GameManager.ptcCoinsCount, ptcUpgradeCost, out GameManager.coinsCount, out GameManager.ptcCoinsCount);
        level++;
        double aux = System.Math.Pow(Coefficient, level);
        upgradeCost = initialCost * System.Math.Pow(1000, ptcInitialCost) * (aux);
        if (upgradeCost >= 1000)
        {
            HighValue.CalculatePTC(upgradeCost, 0, out upgradeCost, out ptcUpgradeCost);
        }
        produc = (iniProduc * System.Math.Pow(1000, ptcIniProduc) * level);
        if (produc >= 1000)
        {
            HighValue.CalculatePTC(produc, 0, out produc, out ptcProduc);
        }
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
        double aux = System.Math.Pow(Coefficient, levelDecrease);
        costDecrease = iniCostDecrease* System.Math.Pow(1000, iniCostDecreasePtc) * aux;
        if (costDecrease > 1000)
        {
            HighValue.CalculatePTC(costDecrease, 0, out costDecrease, out ptcDecrease);
        }
        currentTime = time/levelDecrease;
        WriteValues();
        cps = (produc*(System.Math.Pow(1000,ptcProduc))) / currentTime;
        ShowCPS();
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
        activeTXT.GetComponent<Text>().text = "Boost post - $" + System.Math.Round(upgradeCost, 2) + " " + HighValue.values[ptcUpgradeCost];
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

        upgradeCost = data.upgrade_cost;
        produc = data.produc;
        ptcUpgradeCost = data.ptcUpgrade;
        ptcProduc = data.ptcProduc;
        level = data.level;
        costDecrease = data.costDecrease;
        ptcDecrease = data.ptcDecrease;
        levelDecrease = data.levelDecrease;
        currentTime = data.currentTime;
        cps = data.cps;
    }
}
