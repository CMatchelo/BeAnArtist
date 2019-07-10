using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighValue : MonoBehaviour
{
    public string[] Values = new string[] 
    { " ", "K", "M", "B", "T", "Q", "AA", "AB", "AC", "AD", "AE", "AF",  "AG" };

    public void CalculatePTC (double value, double ptc)
    {
        value = System.Math.Round(value/1000, 2);
        ptc += 1;
    }

}
