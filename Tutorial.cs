using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject txtTutorial;
    public int step = 0;
    public GameObject btn_char;
    public GameObject locked_btn_char;
    public GameObject btn_art1;
    public GameObject locked_btn_art1;
    public GameObject btn_upgChar;
    public GameObject locked_btn_upgChar;
    public GameObject btn_upgArt1;
    public GameObject locked_btn_upgArt1;
    public GameObject goToGame;

    // Start is called before the first frame update
    void Start()
    {
        txtTutorial.GetComponent<Text>().text = "Hi. You're about to start your artist carrer. Click in your char_btn until you make enough money to post your first art and sell it online";
    }
    // .SetActive(true);
    // Update is called once per frame
    void Update()
    {
        // Comprar arte
        if (GameManager.coinsCount >= 4 && step < 2)
        {
            // Desativar botao chat_btn
            // Ativar botao art1
            step=1;
            txtTutorial.GetComponent<Text>().text = "Great, now you have enough money to post your first art. Click in Post art";
            ChangeBTN(locked_btn_char, btn_art1, btn_char, locked_btn_art1);
        }

        if (GameManager.coinsCount <= 10 && step == 2)
        {
            txtTutorial.GetComponent<Text>().text = "Click chat_btn again to make more money";
            ChangeBTN(btn_char, locked_btn_art1, locked_btn_char, btn_art1);
        }

        if (GameManager.coinsCount >= 10 && step == 2)
        {
            txtTutorial.GetComponent<Text>().text = "Now you can buy your first upgrade. Go to store and click to upgrade chat_btn";
            ChangeBTN(locked_btn_char, btn_upgChar, btn_char, locked_btn_upgChar);
        }

        if (GameManager.coinsCount <= 10 && step == 3)
        {
            txtTutorial.GetComponent<Text>().text = "Click chat_btn again to make more money";
            ChangeBTN(btn_char, locked_btn_upgChar, locked_btn_char, btn_upgChar);
        }

        if (GameManager.coinsCount >= 10 && step == 3)
        {
            txtTutorial.GetComponent<Text>().text = "You can upgrade your first art. Go to store and click to upgrade art1";
            ChangeBTN(locked_btn_char, btn_upgArt1, btn_char, locked_btn_upgArt1);
        }

        if (step == 4)
        {
            txtTutorial.GetComponent<Text>().text = "Well, now you know how to progress in your career. Press the button and go ahead";
            btn_upgArt1.SetActive(false);
            locked_btn_upgArt1.SetActive(true);
            goToGame.SetActive(true);
            // Colocar botao pra carregar SampleScene
            PlayerPrefs.SetInt("TutorialDone", 1);
            goToGame.GetComponent<Button>().onClick.AddListener(() => ClickLevel("SampleScene"));
        }
    }

    /*public void NextStep()
    {
        step++;
    }*/

    public void ChangeBTN(GameObject btnTrue1, GameObject btnTrue2, GameObject btnFalse1, GameObject btnFalse2)
    {
        btnTrue1.SetActive(true);
        btnTrue2.SetActive(true);
        btnFalse1.SetActive(false);
        btnFalse2.SetActive(false);
    }

    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
        print("test");
    }

}
