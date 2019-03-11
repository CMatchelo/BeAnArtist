using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject activeBTN;
    public GameObject activeTXT;
    public GameObject lockedBTN;
    public GameObject lockedTXT;
    public int qtyCoins;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        qtyCoins = GameManager.coinsCount;
        activeTXT.GetComponent<Text>().text = "Boost post - $" + UpgradeBTN.upgrade_cost;

        if (qtyCoins >= UpgradeBTN.upgrade_cost)
        {
            activeBTN.SetActive(true);
            lockedBTN.SetActive(false);
        }
        else
        {
            activeBTN.SetActive(false);
            lockedBTN.SetActive(true);
        }
    }
}
