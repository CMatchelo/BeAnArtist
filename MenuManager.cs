using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject main;
    public GameObject store;
    public GameObject settings;
    //public GameObject extra;

    public void MainClick()
    {
        main.SetActive(true);
        store.SetActive(false);
        settings.SetActive(false);
        //extra.SetActive(false);
    }

    public void StoreClick()
    {
        main.SetActive(false);
        store.SetActive(true);
        settings.SetActive(false);
        //extra.SetActive(false);
    }

    public void SettingsClick()
    {
        main.SetActive(false);
        store.SetActive(false);
        settings.SetActive(true);
        //extra.SetActive(false);

    }
    /*
    public void ExtraClick()
    {

    }
    */


}
