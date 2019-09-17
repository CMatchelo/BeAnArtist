using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArtsData
{
    public double upgrade_cost;
    public double produc;
    public int ptcUpgrade;
    public int ptcProduc;
    public double level;
    public double cps;
    public double costDecrease;
    public int ptcDecrease;
    public double levelDecrease;
    public double currentTime;

    public ArtsData(ArtsManager artsManager)
    {
        level = artsManager.level;
        levelDecrease = artsManager.levelDecrease;
        currentTime = artsManager.currentTime;
        cps = artsManager.cps;
    }
}
