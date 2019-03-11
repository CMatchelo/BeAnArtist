using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls UI 
 * Controls QTY coins
 * */
public class GameManager : MonoBehaviour
{
    public static int coinsCount;
    public GameObject coinsDisplay;
    public GameObject autocoinsStats;
    public int coinsInternal;
    public static int cps;

    // Update is called once per frame
    void Update()

    {
        coinsInternal = coinsCount;
        coinsDisplay.GetComponent<Text>().text = "teste " + coinsCount;
        autocoinsStats.GetComponent<Text>().text = "Sellings @: " + cps;
    }





}
