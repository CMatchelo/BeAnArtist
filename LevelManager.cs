using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Image from1;
    public Image from2;
    public Image from3;
    public Image to1;
    public Image to2;
    public Image to3;
    public Image board;
    public static string[] social = new string[] { "feice", "twirer", "instagrao"};
    public static string[] country = new string[]
    {"Brazil", "USA", "Japan", "Germany", "South AFrica"};

    public void UpLevel()
    {
        GameManager.levelGeral++;
    }
}

