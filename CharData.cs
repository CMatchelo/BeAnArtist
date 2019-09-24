using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharData
{
    public int ptcCharCoins;
    public int ptcUpgradeValue;
    public double level;
    public double charCoins; // Quantidade de coins por click no char
    public double upgradeValue;

    public CharData(CharManager charManager)
    {
        level = charManager.level;
        charCoins = charManager.charCoins;
        upgradeValue = charManager.upgradeValue;
    }
}
