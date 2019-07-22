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

    public CharData(CharBTN charBtn)
    {
        ptcCharCoins = charBtn.ptcCharCoins;
        ptcUpgradeValue = charBtn.ptcUpgradeValue;
        level = charBtn.level;
        charCoins = charBtn.charCoins;
        upgradeValue = charBtn.upgradeValue;
    }
}
