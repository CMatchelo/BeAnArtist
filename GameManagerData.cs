using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManagerData
{
    public double coinsInternal;
    public int ptcCoinsCountInternal;

    public GameManagerData(GameManager gameManager)
    {
        coinsInternal = gameManager.coinsInternal;
        ptcCoinsCountInternal = gameManager.ptcCoinsCountInternal;
    }
}
