using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll(); // DELETE ALL PREFS LINE *********
        if (PlayerPrefs.HasKey("TutorialDone"))
        { 
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }

}
