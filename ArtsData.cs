using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArtsData
{
    public int level;
    public double cps;
    public double levelDecrease;

    public ArtsData(ArtsManager artsManager)
    {
        level = artsManager.level;
        levelDecrease = artsManager.levelDecrease;
        cps = artsManager.cps;
    }
}
