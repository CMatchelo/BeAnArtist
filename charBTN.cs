using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controls clicks @ mains char
 * */
public class charBTN : MonoBehaviour
{
    //public GameObject textBox;
    public static int charCoins; // Quantidade de coins por click no char

    void Start()
    {
        charCoins = 1;
    }

    public void ClickButton()
    {
        GameManager.coinsCount += charCoins; // AUmenta coins ao clicar no char
    }
}
