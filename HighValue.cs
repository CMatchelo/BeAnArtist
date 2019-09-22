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

    public static void CalculatePTC(double value, int ptc, out double valueOut, out int ptcOut)
    {
        while (value > 1000)
        {
            value = value/1000;
            ptc++;
        }
        valueOut = value;
        ptcOut = ptc;
    }

    public static void MakeMoney(double qty, int ptc, out double qtyOut, out int ptcOut)
    {
        ptcOut = 0;
        int aux = ptc - GameManager.ptcCoinsCount;
        qtyOut = qty*System.Math.Pow(1000, aux);
    }

    public static void SumMoney(double qty, int ptc, double qty2, int ptc2, out double qtyOut, out int ptcOut)
    {
        ptcOut = 0;
        int aux = ptc - ptc2;
        qtyOut = qty * System.Math.Pow(1000, aux);
    }

    public static void SubtractMoney(double minuendo, double subtraendo, int ptcM, int ptcS, out double result, out int ptc)
    {
        int ptcAux = 0;
        minuendo = minuendo * System.Math.Pow(1000, ptcM);
        subtraendo = subtraendo * System.Math.Pow(1000, ptcS);
        result = minuendo - subtraendo;
        if (result > 1000)
        {
            CalculatePTC(result, 0, out result, out ptcAux);
        }
        ptc = ptcAux;
    }
    
    public static void IniStats(double value, int ptc, out double valueOut, out int ptcOut)
    {
        value *= (System.Math.Pow(10, GameManager.levelGeral));
        HighValue.CalculatePTC(value, ptc, out value, out ptc);
        ptcOut = ptc;
        valueOut = value;
    }
}
