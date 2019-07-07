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
    //public int started;
    public double initialCost;
    public double upgrade_cost;
    public double Coefficient;
    public float time;
    public double iniProduc;
    public double produc;
    
    private double level;
    public Button activeBTN;
    public GameObject activeTXT;
    public double qtyCoins;
    public bool makeProfit = false;
    
    public float timeDecrease;
    public double costDecrease;
    public double iniCostDecrease;
    public double levelDecrease;
    public Button decreaseTimeBTN;
    public GameObject decreaseTimeTXT;
    //public GameObject lockeddecreaseTimeBTN;
    //public GameObject lockeddecreaseTimeTXT;
    
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
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
        if (level > 0)
        {
            double aux = System.Math.Round(upgrade_cost, 2);
            activeTXT.GetComponent<Text>().text = "Boost post - $" + aux;
        }
        //
        decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(costDecrease, 2));
        //
        if (qtyCoins >= upgrade_cost)
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
        //
        if (qtyCoins >= costDecrease && makeProfit == true)
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
    // Upgrade when click
    // upgrade_cost  = initialCost * (coefficient)^owned
    // produc = (iniProduc * owned) * multipliers
    public void ClickButton()
    {
        double produc_old = produc;
        GameManager.coinsCount -= upgrade_cost;
        level++;
        double aux = System.Math.Pow(Coefficient, level);
        upgrade_cost = initialCost * (aux);
        produc = (iniProduc * level);
        GameManager.cps += (produc-produc_old);
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
        levelDecrease++;
        GameManager.coinsCount -= costDecrease;
        double aux = System.Math.Pow(Coefficient, levelDecrease);
        costDecrease = iniCostDecrease*aux;
        time = time/2;
        decreaseTimeTXT.GetComponent<Text>().text = "Sell your art" + id + " 2x faster - $" + (System.Math.Round(costDecrease, 2));
    }
    //
    //
    // 
}
