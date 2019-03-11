﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls Art1`s upgrades
 * */
public class UpgradeBTN : MonoBehaviour
{
    //public GameObject textBox;
    public int ID;
    public GameObject statusBox;
    public int upgrade_cost;
    public int level;
    public GameObject activeBTN;
    public GameObject activeTXT;
    public GameObject lockedBTN;
    public GameObject lockedTXT;
    public int qtyCoins;
    public bool makeProfit = false;
    public int time;
    public int timeDecrease;
    public int costDecrease;
    public GameObject decreaseTimeBTN;
    public GameObject decreaseTimeTXT;
    public GameObject lockeddecreaseTimeBTN;
    public GameObject lockeddecreaseTimeTXT;

    void Start()
    {

    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        activeTXT.GetComponent<Text>().text             = "Boost post - $" + upgrade_cost;
        lockedTXT.GetComponent<Text>().text             = "Boost post - $" + upgrade_cost;

        decreaseTimeTXT.GetComponent<Text>().text       = "Sell your art " + timeDecrease + " seconds faster - $" + costDecrease;
        lockeddecreaseTimeTXT.GetComponent<Text>().text = "Sell your art " + timeDecrease + " seconds faster - $" + costDecrease;

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
    public void ClickButton()
    {
        if(GameManager.coinsCount >= upgrade_cost)
        {
            GameManager.coinsCount -= upgrade_cost;
            upgrade_cost *= 2;
            level++;
            print(level);
            GameManager.cps += level;
        }
        else
        {
            statusBox.GetComponent<Text>().text = "Not enough coins.";
            statusBox.GetComponent<Animation>().Play("status_anim");
        }
    }
    //
    //
    // Auto_money_maker
    IEnumerator makeMoney()
    {
        GameManager.coinsCount += level;
        print("arte" + ID + " made " + level + " coins");
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