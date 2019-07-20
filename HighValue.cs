﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighValue : MonoBehaviour
{
    public static string[] values = new string[]
    { " ", "K", "M", "B", "T", "Q", "AA", "AB", "AC", "AD", "AE", "AF",  "AG" };
    public static void CalculatePTC(double value, int ptc, out double valueOut, out int ptcOut)
    {
        valueOut = System.Math.Round(value / 1000, 2);
        ptcOut = ptc+1;
    }

    public static void makeMoney (double qty, int ptc)
    {
        int aux = ptc - GameManager.ptcCoinsCount;
        qty = qty*System.Math.Pow(1000, aux);
        GameManager.coinsCount += qty;
    }
}
