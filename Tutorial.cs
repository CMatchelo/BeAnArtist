using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tutorialTXT;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(GameManager.coinsCount < 10)
        {
            tutorialTXT.GetComponent<Text>().text = "Click on yourself until you make enough coins to post your first picture";
        }

        if (GameManager.coinsCount >= 10 && GameManager.coinsCount < 11)
        {

        }
    }
}
