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
    //public GameObject textBox;
    // upgrade_cost  = initialCost * (coefficient)^owned
    // produc = (iniProduc * owned) * multipliers
    public int id;
    public int started;
    public GameObject statusBox;
    public double initialCost;
    public double upgrade_cost;
    public double Coefficient;
    public float time;
    //public double iniRevenue;
    public double iniProduc;
    public double produc;
    
    private double level;
    public GameObject activeBTN;
    public GameObject activeTXT;
    public GameObject lockedBTN;
    public GameObject lockedTXT;
    public double qtyCoins;
    public bool makeProfit = false;
    
    public float timeDecrease;
    public double costDecrease;
    public double iniCostDecrease;
    public GameObject decreaseTimeBTN;
    public GameObject decreaseTimeTXT;
    public GameObject lockeddecreaseTimeBTN;
    public GameObject lockeddecreaseTimeTXT;
    
    void Awake()
    {
        // Load level
        String levelSavedAux = "levelSave" + id;
        String timeSavedAux = "timeSave" + id;
        if (PlayerPrefs.HasKey(levelSavedAux))
        {
            int aux;
            aux = PlayerPrefs.GetInt(levelSavedAux, 0);
            //print("Opened: art" + id + " @ level " + aux);
            level = (double)aux;
        }
        // Load time
        if (PlayerPrefs.HasKey(timeSavedAux))
        {
            time = PlayerPrefs.GetFloat(timeSavedAux, 0);
        }
    }

    void OnApplicationQuit()
    {
        String levelSavedAux = "levelSave" + id;
        String timeSavedAux = "timeSave" + id;
        PlayerPrefs.SetInt(levelSavedAux, (int)level);
        PlayerPrefs.SetFloat(timeSavedAux, time);
        //PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        print("start art " + id + " at level " + level);
        if (level > 0)
        {
            double aux = System.Math.Pow(Coefficient, level);
            upgrade_cost = initialCost * (aux);
            produc = (iniProduc * level);
            double aux2 = System.Math.Round(upgrade_cost, 2);
            activeTXT.GetComponent<Text>().text = "Boost post - $" + aux2;
            lockedTXT.GetComponent<Text>().text = "Boost post - $" + aux2;
            makeProfit = true;
            StartCoroutine(makeMoney());
        }
        else
        {
            activeTXT.GetComponent<Text>().text = "Post art - $" + initialCost;
            lockedTXT.GetComponent<Text>().text = "Post art - $" + initialCost;
        }
    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        if (level > 0)
        {
            double aux = System.Math.Round(upgrade_cost, 2);
            //Mathf.RoundToInt((float)myDouble);
            //int aux = Mathf.CeilToInt((float)upgrade_cost);
            activeTXT.GetComponent<Text>().text = "Boost post - $" + aux;
            lockedTXT.GetComponent<Text>().text = "Boost post - $" + aux;
        }
        //
        /*
        if (time <= 1 && level > 1)
        {
            decreaseTimeTXT.GetComponent<Text>().text = "Minimum time reached";
            lockeddecreaseTimeTXT.GetComponent<Text>().text = "Minimum time reached";
        }
        else
        {*/
            decreaseTimeTXT.GetComponent<Text>().text = "Sell your art 2x faster - $" + costDecrease;
            lockeddecreaseTimeTXT.GetComponent<Text>().text = "Sell your art 2x faster - $" + costDecrease; ;
        //}
        //
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
        //
        if (qtyCoins >= costDecrease && makeProfit == true)
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
        double produc_old = produc;
        GameManager.coinsCount -= upgrade_cost;
        level++;
        double aux = System.Math.Pow(Coefficient, level);
        upgrade_cost = initialCost * (aux);
        produc = (iniProduc * level);
        print(level);
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
        //if (time > 1)
        //{
            print("decreased");
            GameManager.coinsCount -= costDecrease;
            double aux = System.Math.Pow(Coefficient, level);
            costDecrease = iniCostDecrease*aux;
            time = time/2;
       // }
        /*
        if (time < 1)
        {
            time = 1;
        }*/
    }
    //
    //
    // 
}
