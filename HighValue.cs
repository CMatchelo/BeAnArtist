using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighValue : MonoBehaviour
{
    public static string[] values = new string[]
    { " ", "K", "M", "B", "T", "Q", "AA", "AB", "AC", "AD", "AE", "AF",  "AG" };
    public static void CalculatePTC(double value, out double valueOut, out int ptcOut)
    {
        int ptc = 0;
        while (value > 1000)
        {
            value = value / 1000;
            ptc++;
        }
        valueOut = System.Math.Round(value / 1000, 2);
        ptcOut = ptc;
    }

    public static void MakeMoney (double qty, int ptc)
    {
        int aux = ptc - GameManager.ptcCoinsCount;
        qty = qty*System.Math.Pow(1000, aux);
        GameManager.coinsCount += qty;
        print("Aqui");
    }

    public static void SubtractMoney(double minuendo, double subtraendo, int ptcM, int ptcS, out double result, out int ptcAux)
    {
        ptcAux = 0;
        while ((minuendo - subtraendo) < 1)
        {
            minuendo *= 1000;
            ptcAux++;
        }
        result = minuendo - subtraendo;
    }
}
