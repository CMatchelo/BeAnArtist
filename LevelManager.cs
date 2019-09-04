using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static string[] social = new string[] { "feici", "twirer", "instagram"};
    public static string[] country = new string[]
    {"Brazil", "USA", "Japan", "Germany", "South AFrica"};

    public void UpLevel()
    {
        GameManager.levelGeral++;
    }

}

