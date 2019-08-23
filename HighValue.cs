using System;
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
}
