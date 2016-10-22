using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class Erreurs : MonoBehaviour
{
    static GameObject Main;
    public Text textZone;
    static Text tz;
    static bool change = false;
        
    void Start()
    {
        Main = gameObject;
        tz = textZone;
        gameObject.SetActive(false);
    }
    void OnGUI()
    {
        if (change)
        {
            change = false;
            new WaitForEndOfFrame();
            GetComponentInChildren<Scrollbar>().value = 0;
        }
    }
    static public void NouvelleErreur(string message)
    {
        tz.text += message + '\n';
        Main.SetActive(true);
        change = true;
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
    }
    public static void Afficher()
    {
        Main.SetActive(true);
    }
    public static void Cacher()
    {
        Main.SetActive(false);
    }
    public static bool Visible()
    {
        return Main.activeSelf;
    }
}
