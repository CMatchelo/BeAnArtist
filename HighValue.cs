using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighValue : MonoBehaviour
{
    public static string[] values = new string[]
    { " ", "K", "M", "B", "T", "Q", "AA", "AB", "AC", "AD", "AE", "AF",  "AG" };
    public static void CalculatePTC(double* value, int* ptc)
    {
        value = System.Math.Round(value / 1000, 2);
        ptc += 1;
    }
}
