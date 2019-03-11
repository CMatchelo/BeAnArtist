using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls Art1`s upgrades
 * */
public class UpgradeBTN : MonoBehaviour
{
    //public GameObject textBox;
    public GameObject statusBox;
    public static int upgrade_cost;
    public static int level = 0;
    public GameObject activeBTN;
    public GameObject activeTXT;
    public GameObject lockedBTN;
    public GameObject lockedTXT;
    public int qtyCoins;
    public bool makeProfit = false;
    public int time=10;
    public int timeDecrease = 3;

    void Start()
    {
        upgrade_cost = 10;
    }

    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        activeTXT.GetComponent<Text>().text = "Boost post - $" + upgrade_cost;
        lockedTXT.GetComponent<Text>().text = "Boost post - $" + upgrade_cost;

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

        if (level > 0 && makeProfit == false)
        {
            makeProfit = true;
            StartCoroutine(makeMoney());
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
            print("update");
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
        yield return new WaitForSeconds(time);
        makeProfit = false;
    }
    //
    //
    // Time to profit
    /*
    public void DecreaseTimeProfit()
    {
        if (time > 1)
        {
            time -= timeDecrease;
        }
        
        if (time < 1)
        {
            time = 1;
        }
    }*/

}