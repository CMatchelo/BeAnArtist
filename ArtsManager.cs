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
    public static bool makeProfit = false;
    public int level;
    public double Coefficient;
    public double cps;
    public int multQty;
    public double perc;

    public double upgradeCost;
    public double initialCost;

    public double iniProduc;
    public double produc;

    public Button activeBTN;
    public GameObject activeTXT;
    public GameObject likesTXT;
    public GameObject showProducTXT;

    public double time;
    public double currentTime;
    public double filledTime;
    public double costDecrease;
    public double iniCostDecrease;
    public double levelDecrease=1;
    
    public Button decreaseTimeBTN;
    public GameObject decreaseTimeTXT;
    public Image sqrFilledIMG;

    void FillImage()
    {
        if (filledTime >= currentTime)
        {
            filledTime = 0;
        }
        filledTime += Time.deltaTime;
        sqrFilledIMG.fillAmount = (float)filledTime / (float)currentTime;
    }

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
        if (level > 0)
        {
            CalcUpgCost(HighValue.multQty[multQty]);
            CalcProduc();
            CalcDecreasCost();
            WriteValues();
            ShowCPS();
            currentTime = time / levelDecrease;
            makeProfit = true;
            StartCoroutine(makeMoney());
        }
        else
        {
            makeProfit = false;
            upgradeCost = initialCost;
            produc = iniProduc;
            HighValue.CalculatePTC(initialCost, out double showInitialCost, out int ptcInitialCost);
            activeTXT.GetComponent<Text>().text = "Post - $" + System.Math.Round(showInitialCost, 2) + " " + HighValue.values[ptcInitialCost];
            decreaseTimeTXT.GetComponent<Text>().text = "Post your art before upgrade it";
            likesTXT.GetComponent<Text>().text = level + "";
            currentTime = time;
        }
        if (levelDecrease == 0 && level > 0)
        {
            HighValue.CalculatePTC(iniCostDecrease, out iniCostDecrease, out int iniCostDecreasePtc);
            decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(iniCostDecrease, 2)) + " " + HighValue.values[iniCostDecreasePtc];
        }
    }

    void Update()
    {
        // Verifies if art is active
        if (level > 0)
        {
            FillImage();
            if (makeProfit == false)
            {
                makeProfit = true;
                StartCoroutine(makeMoney());
            }
        }
        //Verifies if enough money to upgrade art
        if (GameManager.coinsCount >= upgradeCost)
        {
            activeBTN.interactable = true;
        }
        else
        {
            activeBTN.interactable = false;
        }
       
        // Verifies if enough money to downgrade time to profit
        if (GameManager.coinsCount >= costDecrease && makeProfit == true)
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
        GameManager.coinsCount -= upgradeCost;
        level += HighValue.multQty[multQty];
        CalcUpgCost(HighValue.multQty[multQty]);
        CalcProduc();
        cps = produc / currentTime;
        ShowCPS();
    }
    //
    //
    // Decrease time to profit - Faster time unit
    public void DecreaseTimeProfit()
    {
        levelDecrease++;
        GameManager.coinsCount -= costDecrease;
        CalcDecreasCost();
        currentTime = time/levelDecrease;
        WriteValues();
        cps = produc / currentTime;
        ShowCPS();
        filledTime = currentTime;
    }
    //
    //
    // Calculate cost to upgrade art
    void CalcUpgCost(int total)
    {
        upgradeCost = 0;
        int levelAux = level;
        for (int i = 0; i<total; i++)
        {
            double aux = System.Math.Pow(Coefficient, levelAux);
            double costAux = upgradeCost;
            upgradeCost = costAux + initialCost * aux;
            levelAux++;
            WriteValues();
        }
    }
    //
    //
    //
    public void MultQty()
    {
        multQty++;
        if (multQty > 2)
        {
            multQty = 0;
        }
        CalcUpgCost(HighValue.multQty[multQty]);
    }
    //
    //
    // Calculate art produc per cicle
    public void CalcProduc()
    {
        produc = iniProduc * level * perc;
    }
    //
    //
    // Calculate cost to decrease time
    public void CalcDecreasCost()
    {
        double aux = System.Math.Pow(Coefficient, levelDecrease);
        costDecrease = iniCostDecrease * aux;
    }
    //
    //
    // Auto_money_maker
    IEnumerator makeMoney()
    {
        yield return new WaitForSeconds((float)currentTime);
        GameManager.coinsCount += produc;
        print("arte" + id + " made " + produc + " coins");
        makeProfit = false;
    }
    //
    //
    // Updating values when level geral increases
    public void UpLevel()
    {
        perc = (100 + GameManager.followers) / 100;
        level = 0;
        levelDecrease = 1;
        HighValue.IniStats(iniProduc, out iniProduc);
        upgradeCost = initialCost;
        produc = iniProduc;
        costDecrease = iniCostDecrease;
        currentTime = time;
        makeProfit = false;
        HighValue.CalculatePTC(initialCost, out double showIniCost, out int ptcInitialCost);
        activeTXT.GetComponent<Text>().text = "Post art - $" + System.Math.Round(initialCost, 2) + " " + HighValue.values[ptcInitialCost];
        decreaseTimeTXT.GetComponent<Text>().text = "Post your art before upgrade it";
    }
    //
    //
    // Write values on text fields
    public void WriteValues()
    {
        HighValue.CalculatePTC(costDecrease, out double showCostDecrease, out int ptcDecrease);
        decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(showCostDecrease, 2)) + " " + HighValue.values[ptcDecrease];
        HighValue.CalculatePTC(upgradeCost, out double showUpgradeCost, out int ptcUpgradeCost);
        activeTXT.GetComponent<Text>().text = "Boost post - $" + System.Math.Round(showUpgradeCost, 2) + " " + HighValue.values[ptcUpgradeCost];
        likesTXT.GetComponent<Text>().text = level + " k";
    }
    //
    //
    // Calculates and writes CPS on field
    public void ShowCPS()
    {
        if (currentTime < 1)
        {
            HighValue.CalculatePTC(cps, out double cpsAux, out int ptcOUt);
            showProducTXT.GetComponent<Text>().text = System.Math.Round(cpsAux, 2) + " " + HighValue.values[ptcOUt] + "/ sec";
        }
        else
        {
            HighValue.CalculatePTC(produc, out double showProduc, out int ptcProduc);
            showProducTXT.GetComponent<Text>().text = System.Math.Round(showProduc, 2) + " " + HighValue.values[ptcProduc] + " / " + currentTime + " seg";
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
        cps = data.cps;
    }
}
