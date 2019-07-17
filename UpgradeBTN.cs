using System;
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
    public int id;
    public double qtyCoins;
    public int ptcQtyCoins;
    public bool makeProfit = false;
    private double level;

    public double initialCost;
    public double upgrade_cost;
    public double Coefficient;
    public float time;
    public double iniProduc;
    public double produc;
    public int ptcUpgrade = 0;
    public Button activeBTN;
    public GameObject activeTXT;

    public float timeDecrease;
    public double costDecrease;
    public double iniCostDecrease;
    public double levelDecrease;
    public int ptcDecrease = 0;
    public Button decreaseTimeBTN;
    public GameObject decreaseTimeTXT;
    
    void Awake()
    {
        PlayerPrefs.DeleteAll(); // DELETE ALL PREFS LINE *********
        String levelSavedAux = "levelSave" + id;
        String timeSavedAux = "timeSave" + id;
        String levelDecreaseAux = "levelDecrease" + id;
        //
        // Load level
        if (PlayerPrefs.HasKey(levelSavedAux))
        {
            int aux;
            aux = PlayerPrefs.GetInt(levelSavedAux, 0);
            level = (double)aux;
        }
        //
        // Load time
        if (PlayerPrefs.HasKey(timeSavedAux))
        {
            time = PlayerPrefs.GetFloat(timeSavedAux, 0);
            int aux;
            aux = PlayerPrefs.GetInt(levelDecreaseAux, 0);
            levelDecrease = (double)aux;
        }
    }

    void OnApplicationQuit()
    {
        String levelSavedAux = "levelSave" + id;
        String timeSavedAux = "timeSave" + id;
        String levelDecreaseAux = "levelDecrease" + id;
        PlayerPrefs.SetInt(levelSavedAux, (int)level);
        PlayerPrefs.SetFloat(timeSavedAux, time);
        PlayerPrefs.SetInt(levelDecreaseAux, (int)levelDecrease);
    }

    void Start()
    {
        qtyCoins = GameManager.coinsCount;
        ptcQtyCoins = GameManager.ptcCoinsCount;
        if (level > 0)
        {
            double aux = System.Math.Pow(Coefficient, level);
            upgrade_cost = initialCost * (aux);
            produc = (iniProduc * level);
            double aux2 = System.Math.Round(upgrade_cost, 2);
            activeTXT.GetComponent<Text>().text = "Boost post - $" + aux2;
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
        // Verifies if art is active
        if (level > 0 && makeProfit == false)
        {
            makeProfit = true;
            StartCoroutine(makeMoney());
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
    { // prod // upgrade_cost // level // coefficient // initialCost // iniProduc
        double produc_old = produc;
        GameManager.coinsCount -= upgrade_cost; //////////////////////////////////
        level++;
        double aux = System.Math.Pow(Coefficient, level);
        upgrade_cost = initialCost * (aux);
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
        yield return new WaitForSeconds(time);
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
            HighValue.CalculatePTC(&costDecrease, &ptcDecrease);
        }
        time = time/2;
        decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(costDecrease, 2)) + " " + HighValue.values[ptcDecrease];
    }
    //
    //
    // 
}
