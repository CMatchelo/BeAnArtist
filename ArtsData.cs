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

    public double costDecrease;
    public int ptcDecrease;
    public double levelDecrease;
    public double currentTime;

    public ArtsData(ArtsManager artsManager)
    {
        upgrade_cost = artsManager.upgradeCost;
        produc = artsManager.produc;
        ptcUpgrade = artsManager.ptcUpgradeCost;
        ptcProduc = artsManager.ptcProduc;
        level = artsManager.level;
        costDecrease = artsManager.costDecrease;
        ptcDecrease = artsManager.ptcDecrease;
        levelDecrease = artsManager.levelDecrease;
        currentTime = artsManager.currentTime;
    }
}
