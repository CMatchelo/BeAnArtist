using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighValue : MonoBehaviour
{
    public static string[] values = new string[]
    { " ", "K", "M", "B", "T", "Q", "AA", "AB", "AC", "AD", "AE", "AF",  "AG" };

    public static int[] multQty = new int[]
    {1, 10, 50};

    public static void CalculatePTC(double value, out double valueOut, out int ptcOut)
    {
        int ptc = 0;
        while (value > 1000)
        {
            value = value/1000;
            ptc++;
        }
        valueOut = value;
        ptcOut = ptc;
    }
    
    public static void IniStats(double value, out double valueOut)
    {
        value *= (System.Math.Pow(10, GameManager.levelGeral));
        valueOut = value;
    }
}
